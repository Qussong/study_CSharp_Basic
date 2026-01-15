## 스레드 (정의, 현재 스레드 확인)
- 스레드란 프로세스 내에서 실행되는 흐름의 단위를 의미한다.
- 프로그램이 동시에 여러 작업을 수행할 수 있도록 하는 도구
- 아래 코드는 현재 스레드의 ID 를 출력한다.
    ```cs
    Console.WriteLine(Thread.CurrentThread.ManagedThreadId);    // 1
    ```
    - 지금까지 1번 메인 스레드에서 작업을 진행해왔음을 알 수 있다.
- 디버깅 상태에서 "디버그->창->스레드 (ctrl + shift + h)" 를 통해 스레드 창을 열 수 있다.
- 스레드 창을 통해 현재 동작중인 스레드를 알 수 있다.

## 스레드 (생성 및 시작)
```cs
void DoWork()
{
    Console.WriteLine("DoWork");
}

//Thread thread = new Thread(new ThreadStart(DoWork));
Thread thread = new Thread(DoWork); // 축약가능
thread.Start(); // 스레드 실행

Console.WriteLine("Main Thread");
```
- 스레드 창을 통해 확인해보면 `thread.Start()` 를 통해 스레드가 실행되면 새로운 작업자 스레드가 생성되는 걸 확인할 수 있다.

## 스레드 (실습)
```cs
void DoWork()
{
    try
    {
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(1000);
            Console.WriteLine($"DoWork : {i}");
        }
    }
    catch (ThreadInterruptedException ex)
    {
        Console.WriteLine(ex.Message);
    }
}

Thread thread = new Thread(DoWork);
thread.IsBackground = true;
thread.Start();

while (true)
{
    char c = Console.ReadKey().KeyChar; // 키 입력시까지 대기
    Console.WriteLine("↓↓↓↓↓↓↓");

    if (c == 'q') break;    // 메인스레드 종료
    if (c == 'a') Console.WriteLine("IsAlive : " + thread.IsAlive);
    if (c == 'i') thread.Interrupt();
    if (c == 'j') thread.Join();
}

Console.WriteLine("메인 스레드 완료");
```
- `Thread.Start()` : 스레드 실행
- `Thread.IsAlive` : 해당 스레드가 현재 실행 중이거나, 실행을 시작했지만 아직 완전히 종료되지 않은 상태인지를 나타내는 bool 속성이다.
    - true : 실행 중(Running), 실행 대기 중(WaitSleepJoin), ~~일시 중시 상태(Suspended,)~~
    - false : 아직 시작되지 않았을 때(Unstarted), 완전히 종료되었을 때(Stopped)
    - `ThreadState` 를 통해 구체적인 상태를 확인할 수 있다.
        ```cs
        public enum ThreadState
        {
            Running = 0,
            StopRequested = 1,
            SuspendRequested = 2,
            Background = 4,
            Unstarted = 8,
            Stopped = 16,
            WaitSleepJoin = 32,
            Suspended = 64,
            AbortRequested = 128,
            Aborted = 256
        }
        ```
- `Thread.Interrupt()` : Sleep / Wait / Join 상태의 스레드에 ThreadInterruptedException을 발생시킨다. 실행 중인 스레드에는 아무 일도 안 생김. 예외 처리를 하지 않으면 스레드가 예외로 종료되고, finally / 자원 해제 코드가 실행되지 않아 리소스 누수 가능성이 있다.
    ```cs
    try
    {
        Thread.Sleep(Timeout.Infinite);
    }
    catch (ThreadInterruptedException)
    {
        // 종료 신호 처리
    }
    finally
    {
        // 자원 정리
    }
    ```
- `Thread.Join()` : 해당 스레드가 종료될 때까지 호출한 스레드를 block 시킨다. (위 코드에선 메인 스레드가 호출했기에 메인 스레드가 block 됨)
- `Thread.IsBackground` : 모든 Foreground 스레드가 종료되면 CLR에 의해 남아있는 스레드가 강제 종료된다.
    - Foreground 스레드가 하나라도 남아 있으면 프로세스 유지
    - Background 스레드는 정리 코드 실행 보장 없음

## 스레드 (경쟁 상태)
- 경쟁 상태 (Race Condition)
    - 여러 스레드가 동시에 공유 자원에 접근하여 값을 읽거나 수정할 때, 접근 순서나 실행 타이밍에 따라 프로그램의 동작이 달라지는 상황을 말한다.
    - 예측하지 못한 결과나 "데이터 불일치 (Data Corruption)"를 초래할 수 있다.
- 예제 코드
    ```cs
    // 공유 자원
    int data = 0;   

    void DoWork()
    {
        for (int i = 0; i < 10; i++)
        {
            Thread.Sleep(1);
            data++;
        }
    }

    List<Thread> threads = new List<Thread>();

    for (int i = 0; i < 10; i++)
    {
        Thread thread = new Thread(DoWork);
        threads.Add(thread);
        thread.Start();
    }

    // 리스트에 들어가있는 스레드들이 모두 종료될때까지 메인스레드 대기
    threads.ForEach(t => t.Join());

    // 결과 값 출력 (100 출력 예상)
    Console.Write(data);
    ```
    - DoWork() 함수는 공유 자원인 data를 10번 증가시킨다.
    - DoWork()를 실행하는 스레드는 총 10개이므로, 이론적으로는 data 값이 100이 될 것으로 예상할 수 있다.
    - 하지만 실제 실행 결과는 100이 나올 수도 있으나, 대부분의 경우 100보다 작은 값이 출력된다.

## 스레드 (임계영역 - lock)
- 임계영역 (Critical Section)
    - 다중 스레드 환경에서 여러 스레드가 동시에 접근할 경우 문제가 발생할 수 있는 공유 자원 또는 코드 블록을 의미한다.
    - 임계 영역을 올바르게 보호하지 않으면 데이터 불일치, 경쟁 상태(Race Condition) 등의 문제가 발생할 수 있다.
- 임계 영역의 특징
    - 임계 영역으로 보호된 코드 블록에는 한 시점에 하나의 스레드만 진입할 수 있다.
    - 이를 통해 공유 자원에 대한 동시 접근을 제어하여 스레드 간 충돌을 방지할 수 있다.
    - 단, 이는 해당 임계 영역에 한해서만 안전성을 보장하며, 잘못 설계할 경우 데드락 등의 문제가 발생할 수 있다.
- 임계 영역 생성 예제 코드
    ```cs
    // 공유 자원
    int data = 0;
    // 임계영역의 잠금객체
    object lockObject = new object();

    void DoWork()
    {
        for (int i = 0; i < 10; i++)
        {
            // 임계영역
            lock (lockObject)
            {
                Thread.Sleep(1);
                data++;
            }
        }
    }
    ```
    - lock 키워드를 사용하면 해당 블록은 임계 영역이 되며, 동시에 하나의 스레드만 실행할 수 있다.
    - 이전의 경쟁 상태 예제에서 DoWork()를 위와 같이 수정하면, 모든 스레드가 안전하게 공유 자원에 접근하므로 결과값으로 정상적으로 100이 출력됨을 확인할 수 있다.
    - 잠금 객체(lockObject)는 공유 자원과 동일한 범위(같은 클래스 또는 동일한 생명주기) 에 두는 것이 안전하다.
    - 외부에서 접근 가능한 객체를 잠금 객체로 사용하면 의도치 않은 lock 충돌이나 데드락이 발생할 수 있으므로 주의해야 한다.
- 예제 코드
    ```cs
    class Program
    {
        // 공유 자원
        static int data = 0;
        // 임계영역의 잠금객체
        static readonly object lockObject = new object();

        static void Main()
        {
            void DoWork()
            {
                for (int i = 0; i < 10; i++)
                {
                    // 임계영역
                    lock (lockObject)
                    {
                        Thread.Sleep(1);
                        data++;
                    }
                }
            }

            List<Thread> threads = new List<Thread>();

            for (int i = 0; i < 10; i++)
            {
                Thread thread = new Thread(DoWork);
                threads.Add(thread);
                thread.Start();
            }

            // 리스트에 들어가있는 스레드들이 모두 종료될때까지 메인스레드 대기
            threads.ForEach(t => t.Join());

            // 결과 값 출력 (100 출력 예상)
            Console.Write(data);

            Console.ReadKey();
        }
    }
    ```
    - 잠금객체의 경우 외부에서 수정할 수 없도록 readonly 읽기 전용 한정자를 추가해주면 좋다.

## 스레드 (임계영역 - Monitor)
- Monitor는 임계 영역(Critical Section)을 생성하고 관리하기 위한 클래스로 이를 직접 제어할 수 있도록 확장된 기능을 제공한다. 
- lock 키워드는 내부적으로 Monitor.Enter() / Monitor.Exit()를 사용한다. 즉, lock은 기능이 더 적은 게 아니라 안전하게 제한된 버전이다.
- 예제 코드 (`Monitor` 객체 기본 사용법)
    ```cs
    void DoWork()
    {
        for (int i = 0; i < 10; i++)
        {
            Monitor.Enter(lockObject); // 임계 영역 진입
            try
            {
                Thread.Sleep(1);
                data++;
            }
            finally
            {
                Monitor.Exit(lockObject); // 임계 영역 종료 (반드시 실행)
            }
        }
    }
    ```
    - `Monitor.Enter()`와 `Monitor.Exit()`는 반드시 `try-finally`로 감싸야 한다.
    - 예외가 발생하더라도 잠금이 해제되지 않으면 데드락이 발생할 수 있다.
- 예제 코드 (`Monitor.TryEnter()`)
    ```cs
    class Program
    {
        private static object _lock = new object();

        static void Main()
        {
            Thread thread1 = new Thread(DoWork);
            Thread thread2 = new Thread(DoWork);

            thread1.Start();
            thread2.Start();

            thread1.Join();
            thread2.Join();

            Console.WriteLine("모든 쓰래드 완료");
            Console.ReadKey();
        }

        static void DoWork()
        {
            // 최대 0.5초 동안 잠금 획득 시도
            if (Monitor.TryEnter(_lock, TimeSpan.FromMilliseconds(500)))
            {
                try
                {
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} 번 쓰래드 잠금 획득");
                    Thread.Sleep(1000); // Simulate work
                }
                finally
                {
                    Monitor.Exit(_lock);
                    Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} 번 쓰래드 잠금 해제");
                }
            }
            else
            {
                Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} 번 쓰래드가 잠금을 획득하지 못했습니다.");
            }
        }
    }
    /*
        13 번 쓰래드 잠금 획득
        12 번 쓰래드가 잠금을 획득하지 못했습니다.
        13 번 쓰래드 잠금 해제
        모든 쓰래드 완료
    */
    ```
    - 위 예제는 13번 스레드가 먼저 임계 영역에 진입하여 잠금을 획득한다. 해당 스레드는 약 1초 동안 잠금을 휴지하고 12번 스레드는 최대 0.5초 동안 잠금 획득을 시도하지만, 제한 시간 내에 잠금을 획득하지 못해 false 를 반환한다.
    - `Monitor.TryEnter()` : 잠금 획득을 시도하되, 지정된 시간 동안만 대기하고 그 시간 내에 획득하지 못하면 실패(false)를 반환한다. 예외를 발생시키지 않고, 성공 여부를 반환값으로 알려준다.
- 예제 코드
    ```cs
    class Program
    {
        private static object _lock = new object();
        private static bool _isReady = false;

        static void Main()
        {
            Thread producer = new Thread(Producer);
            Thread consumer = new Thread(Consumer);

            consumer.Start();
            Thread.Sleep(500); // 소비자가 먼저 실행되게 잠시 대기
            producer.Start();

            producer.Join();
            consumer.Join();

            Console.WriteLine("모든 스레드가 완료되었습니다.");
            Console.ReadKey();
        }

        static void Producer()
        {
            lock (_lock)
            {
                Console.WriteLine("생산자가 데이터를 준비 중입니다...");
                Thread.Sleep(2000); // 작업 시뮬레이션
                _isReady = true;
                Console.WriteLine("생산자가 데이터 준비 완료를 알립니다.");
                Monitor.Pulse(_lock); // 대기 중인 스레드에 알림
            }
        }

        static void Consumer()
        {
            lock (_lock)
            {
                while (!_isReady)
                {
                    Console.WriteLine("소비자가 데이터를 기다리고 있습니다...");
                    Monitor.Wait(_lock); // 신호 대기 -> Pulse 신호 올때까지 대기
                }
                Console.WriteLine("소비자가 데이터를 받았습니다.");
            }
        }
    }
    /*
        소비자가 데이터를 기다리고 있습니다...
        생산자가 데이터를 준비 중입니다...
        생산자가 데이터 준비 완료를 알립니다.
        소비자가 데이터를 받았습니다.
        모든 스레드가 완료되었습니다.
    */

    소비자가 데이터를 기다리고 있습니다...
    생산자가 데이터를 준비 중입니다...
    생산자가 데이터 준비 완료를 알립니다.
    ```
    - `Monitor.Wait()` : lock을 풀고, 다시 lock을 얻을 때까지 기다린다. (lock 반납하고 대기)
    - `Monitor.Pulse()` : 대기 중인 스레드에게 "다시 시도해봐" 신호를 보낸다.
    - Producer와 Consumer가 서로 다른 함수에 있더라도 같은 잠금객체(_lock)를 사용한다면 동일한 임계 영역으로 취급하여 둘 중 하나는 임계영역을 사용하지 못한다.
    - 즉, Consumer의 `Monitor.Wait()` 함수가 호출되기전까지 Producer는 임계영역에 진입하지 못한다.
- 언제 lock 대신 Monitor 를 사용하나?
    - 단순한 임계 영역 → lock
    - 아래와 같은 경우 → Monitor
        1. 잠금 대기 시간 제한이 필요한 경우
        2. 잠금 시도 성공/실패를 분기 처리해야 할 경우
        3. Wait/Pulse 를 사용한 조건 동기화가 필요한 경우

## 스레드 (임계영역 - Mutex)
- Mutex 는 주로 임계영역을 관리할 때 사용된다.
- 여러 프로세스에서 리소스 제어접근이 가능하다는 특징이 있다.
- Mutex 생성자
    ```cs
    Mutex(bool initiallyOwned, string name, out bool createdNew)
    ```
    - `bool initiallyOwned` : true 는 생성과 동시에 현재 스레드가 뮤텍스를 점유하며, false 는 생성만 하고 아직 점유하지 않음
    - `string name` : 뮤텍스의 이름으로 같은 이름을 사용하는 모든 프로세스는 동일한 Mutex를 공유한다.
    - `bool createNew` : 단일 인스턴스 체크에 사용되며 true 는 이번에 새로 생성됨을 의미하고 false 는 이미 존재하던 Mutex를 열어서 사용한다.
- 예제 코드
    ```cs
    namespace ThreadMutex1
    {
        class Program
        {
            static void Main(string[] args)
            {
                // 동일한 뮤텍스 이름
                const string mutexName = "Global\\MyUniqueMutex";

                // 뮤텍스 점유 시도
                using (var mutex = new Mutex(false, mutexName, out bool isCreatedNew))
                {
                    if (isCreatedNew)
                        Console.WriteLine("뮤텍스를 새로 생성했습니다. 프로그램이 MutexCreator를 대신 점유합니다.");
                    else
                        Console.WriteLine("MutexCreator가 실행 중입니다. 뮤텍스를 기다립니다...");

                    // 뮤텍스 점유를 기다림
                    try
                    {
                        mutex.WaitOne();
                        Console.WriteLine("뮤텍스를 점유했습니다!");
                    }
                    catch (AbandonedMutexException)
                    { Console.WriteLine("뮤텍스가 포기된 상태에서 점유되었습니다."); }

                    // 프로그램 실행 대기 (예: 10초 동안 점유 유지)
                    Console.WriteLine("뮤텍스를 유지하며 실행 중입니다. 10초 후 종료합니다.");
                    Thread.Sleep(10000);

                    // 뮤텍스 점유 해제
                    mutex.ReleaseMutex();
                    Console.WriteLine("뮤텍스를 해제했습니다.");
                }
            }
        }
    }
    ```
    - `Mutex.WaitOne()` : 
        - 뮤텍스를 획득할 때까지 대기한다. 이미 다른 프로세스 또는 스레드가 뮤텍스를 점유 중이라면, 해제될 때까지 블록 상태를 유지한다.
        - 반환값:
            - true : 뮤텍스 점유 성공
            - false : 지정된 대기 시간 내에 점유하지 못함 (타임아웃 발생 시)
        - 오버로드 : 
            ```cs
            WaitOne()                  // 무한 대기
            WaitOne(int milliseconds)  // 시간 제한
            WaitOne(TimeSpan timeout)
            ```
    - `AbandonedMutexException` : 
        - 어떤 프로세스 또는 스레드가 뮤텍스를 점유한 상태에서 ReleaseMutex()를 호출하지 않고 종료되었을 경우 발생한다.
        - 의미 : 현재 스레드는 뮤텍스를 획득한 상태이지만 이전 소유자가 정상적으로 종료되지 않았으므로, 공유 데이터가 손상되었을 가능성이 있다.
        - 예외를 무시하고 계속 실행할 수는 있지만, 필요한 경우 상태 복구 또는 초기화 로직을 고려해야 한다.
    - `Mutex.ReleaseMutex()` : 
        - 현재 스레드가 점유한 뮤텍스를 해제한다. 뮤텍스를 해제하지 않으면 다른 프로세스/스레드가 대기 상태에 머물게 된다.
        - 뮤텍스를 점유한 상태에서 `ReleaseMutex()`를 호출하지 않고 종료하면, 다음 점유 시 `AbandonedMutexException`이 발생한다.
        - 뮤텍스를 획득한 스레드만 해제할 수 있으며, 다른 스레드가 호출할 경우 `ApplicationException`이 발생한다.
- 즉, `WaitOne()`은 점유를 기다리고, `ReleaseMutex()`는 반드시 소유한 스레드가 해제해야 하며, 정상 해제가 되지 않으면 `AbandonedMutexException`이 발생한다.

## 스레드 (임계영역 - semaphore)
- 여러 스레드(또는 프로세스)가 동시에 접근할 수 있는 최대 개수를 제한하는 동기화 객체
    - lock/Mutex : 동시에 1개만 접근
    - Semaphore/SemaphoreSlim : 동시에 N개까지 접근 가능
- 생성자
    ```cs
    new SemaphoreSlim(int initialCount, int maxCount)
    ```
    - `int initialCount` : 생성 직후 동시에 진입 가능한 스레드 수
    - `int maxCount` : 최대 입장 가능 개수, `Release()`로 증가할 수 있는 최대 한계
    - initialCount ≤ maxCount 여야 함
- 예제 코드
    ```cs
    class Program
    {
        // SemaphoreSlim 생성: 2개의 스레드만 동시에 접근 가능(최대 6개 까지 가능)
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(2, 6);

        static void Main(string[] args)
        {
            Console.WriteLine("SemaphoreSlim 동기 예제 시작!");
            // 입장권 추가
            semaphoreSlim.Release(2);

            // 여러 스레드 생성
            Thread[] threads = new Thread[10];
            for (int i = 1; i <= 10; i++)
            {
                int threadId = i; // 고유 ID 캡처
                threads[i - 1] = new Thread(AccessSharedResource);
                threads[i - 1].Start(threadId);
            }

            // 모든 스레드가 완료될 때까지 대기
            foreach (var thread in threads)
            {
                thread?.Join();
            }

            Console.WriteLine("모든 스레드 작업 완료!");
            Console.ReadKey();
        }

        static void AccessSharedResource(object? id)
        {
            Console.WriteLine($"스레드 {id} - 공유 자원 접근 시도 중...");

            // 임계영역 진입 요청
            semaphoreSlim.Wait(); // 동기 방식으로 잠금 요청
            try
            {
                Console.WriteLine($"스레드 {id} - 공유 자원에 접근!");

                // 공유 자원 작업(예: 파일 쓰기, DB 작업 등)
                Thread.Sleep(2000); // 작업 중인 상태를 시뮬레이션
            }
            finally
            {
                // 반드시 Release를 호출하여 잠금 해제
                Console.WriteLine($"스레드 {id} - 공유 자원 작업 완료.");
                semaphoreSlim.Release();
            }
        }
    }
    ```
    - `Semaphore.Wait()` : 
        - 입장권을 1개 획득할 때까지 대기
        - 입장권이 없으면 다른 스레드가 Release()할 때까지 블록
        - 반환값 :
            - true : 반환값 획득 성공
            - false : 타임아웃 발생 (시간 제한 사용 시)
        - 오버로드
            ```cs
            Wait()
            Wait(int millisecondsTimeout)
            Wait(TimeSpan timeout)
            ```
    - `Semaphore.Release()` : 
        - 입장권을 1개 반환
        - 내부 카운트를 1 증가시킴
        - 오버로드
            ```cs
            Release()
            Release(int releaseCount)
            ```
        - 현재 카운트가 maxCount를 초과하면 `SemaphoreFullException` 발생
- Semaphore는 동시에 실행되는 작업 수를 제한하여 시스템 부하를 제어하는 데 사용할 수 있다. 예를 들어 파일을 100개 생성해야 하는 상황에서 100개의 작업을 동시에 실행하면 디스크 I/O나 시스템 자원에 과도한 부하가 발생할 수 있다. 이때 Semaphore를 사용해 동시에 실행 가능한 스레드 수를 제한하면 작업을 순차적으로 분산 실행하여 시스템 안정성을 높일 수 있다.
- Semaphore는 성능을 높이기 위한 도구라기보다는, 시스템 자원의 사용량을 “조절”하기 위한 도구다.

## 쓰레드풀

|특징|스레드|스레드풀|
|---|---|---|
|**생성 방식**|필요할 때 직접 생성 (`new Thread()`)|시스템에서 관리하는 풀에서 스레드 재사용|
|**비용**|스레드 생성/소멸 비용이 큼|스레드 재사용으로 비용이 낮음|
|**제한**|관리자가 직접 생성한 만큼 동작|풀에 설정된 최대 스레드 개수 제한 있음|
|**유지 관리**|개발자가 수동으로 관리|.NET 런타임이 자동으로 관리|
|**적합한 경우**|소규모 작업이나 특정한 스레드 제어가 필요한 경우|다수의 짧고 반복적인 작업|

- 스레드 풀 (ThreadPool)
    - .Net 런타임이 미리 생성해 두거나 재사용하는 작업용 스레드들의 집합이다.
    - 스레드를 매번 new Thread() 로 만들지 않고 이미 존재하는 스레드를 재사용
    - 필요하면 점진적으로 늘리고, 한가하면 줄임
    - 목적 : 성능 향상 + 자원 낭비 방지
- 예제 코드
    ```cs
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("쓰레드 예제 시작!");

            for (int i = 1; i <= 5; i++)
            {
                //Thread thread = new Thread(() => DoWork());
                //thread.Start();
                ThreadPool.QueueUserWorkItem(state => DoWork());
            }

            Console.ReadKey();
        }



        static void DoWork()
        {
            Console.WriteLine($"[ThreadPool {Thread.CurrentThread.ManagedThreadId}] 작업 시작");
            Thread.Sleep(2000); // 작업 시뮬레이션
            Console.WriteLine($"[ThreadPool {Thread.CurrentThread.ManagedThreadId}] 작업 완료");
        }
    }
    ```
    - `ThreadPool.QueueUserWorkItem(WaitCallback callBac)` : 
        - 스레드 풀에 작업을 등록하고, 풀에 있는 스레드가 하나씩 꺼내서 실행하도록 한다.
        - `WaitCallback callBac` : 
            - 스레드 풀에서 실행할 작업 함수
            - 반드시 void 반환형 (ThreadPool은 작업 결과를 관리하지 않음)
            - 인자값은 형식상 반드시 하나 존재해야한다. 하지만 사용은 선택사항이다.
                ```cs
                public delegate void WaitCallback(object? state);
                ```
            - 주로 클로저(_) 사용한다 (컴파일러가 내부적으로 상태를 캡처한다.)
                ```cs
                int value = 10;
                ThreadPool.QueueUserWorkItem(_ =>
                {
                    Console.WriteLine(value);
                });

                ```
    - 스레드 창을 통해 확인해보면 new Thread 를 사용하는 경우 항상 스레드가 새롭게 생성된다. 하지만, ThreadPool 을 사용하는 경우 .Net에서 관리하는 기존 스레드를 사용하다가 필요시에 새로운 스레드를 생성하여 사용한다.
    - 규모가 작거나, 단순한 작업인 경우 또는 스레드를 직접 컨트롤할 필요가 없는경우 스레드 풀을 활용하는게 훨씬 효율적이다.
    - 비동기를 배우게된다면 스레드 풀을 사용해도 스레드를 직접 제어하는 방법을 배울 수 있다.