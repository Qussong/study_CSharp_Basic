## 비동기 (기초 설명)
- 비동기 프로그래밍(Asynchronous Programming)
    - 작업을 병렬로 처리하여 응용 프로그램의 메인 스레드가 블록되지 않도록 하는 프로그래밍 패러다임
    - 특히 I/O 바운드 작업(파일 읽기/쓰기, 네트워크 요청, 데이터베이스 호출 등)에서 유용하게 사용됨
- 필요성
    - 응답성 향상 : UI 애플리케이션에서 긴 작업이 메인 스레드를 블록하지 않도록 하여 사용자 인터페이스가 계속 반응할 수 있게 한다. 
    - 리소스 최적화 : 스레드 풀을 효율적으로 사용하여 시스템 리소스를 절약한다.
    - 확장성 : 서버 애플리케이션에서 더 많은 동시 요청을 처리할 수 있게 한다.
- 과거에는 Callback 메서드와 같은 비동기 방식을 주로 사용했지만, 현재는 Task 기반의 비동기 방식이 널리 사용되고 있다. (`Task`, `async`, `await`)
- `async`
    - 메서드, 람다, 또는 익명 메서드에 적용되어 비동기 메서드임을 나타낸다.
        ```cs
        // async 키워드는 접근제한자와 반환값 사이에 위치한다.
        public async Task<int> GetDataAsync()
        {
            int result = await FetchDataFromDatabaseAsync();
            return result;
        }
        ```
    - 비동기 메서드는 반환 타입으로 Task, Task<T> 으로 나오는게 일반적이다.
    - 반환값으로 void 도 가능하긴하지만 권장되지 않음 (일반적으로 void는 이벤트 핸들러에만 사용)
    - await 키워드가 Task 를 제어할때 사용되는데 void 키워드를 사용하는경우 await를 사용할 수 없다.
- `await`
    - 반환값이 Task/Task<T>인 경우 사용된다.
    - 비동기 메스드 안에서만 사용가능하다.
- `Task`
    - 비동기 작업을 나타내는 클래스
    - 반환값이 없는 비동기 작업에 사용된다.
        ```cs
        public async Task PerformOperationAsync()
        {
            await Task.Delay(1000); // 1초 지연
        }
        ```
- `Task<T>`
    - 비동기 작업의 결과를 반환하는 클래스
    - T는 반환될 데이터의 타입을 나타낸다.
        ```cs
        public async Task<string> GetMessageAsync()
        {
            await Task.Delay(500);  // 0.5초 지연
            return "Hello, World";  // string 값 반환
        }
        ```

## 비동기 (async, await)
- `await`
    - await는 Task가 완료될 때까지 현재 메서드의 실행 흐름을 일시 중단하는 키워드다.
    - await는 블로킹이 아니며, 스레드를 점유하지 않는다.
    - await는 async 키워드가 선언된 메서드 내부에서만 사용할 수 있다.
- `Task`
    - Task는 미래에 완료될 작업을 표현하는 객체다.
    - 작업의 완료 상태(성공, 실패, 취소)와 예외, 결과를 관리한다.
    - 실제 비동기 작업의 단위이며, 보통 ThreadPool 스레드에서 실행된다.
- `async`
    - async는 메서드 내에서 await를 사용할 수 있게 해주는 문법적 키워드다.
    - 컴파일러가 비동기 상태 머신을 생성하도록 지시한다.
    - async 자체는 스레드를 생성하지 않으며, 비동기 동작의 주체는 Task다.
- Task는 "무엇을 실행할지", await는 "언제 기다릴지", async는 "비동기 문법을 허용하는 표시자"다.
- 예제 코드
    ```cs
    async Task TaskAsync()
    {
        Console.WriteLine("TaskAsync Started");
        await Task.Delay(3000);
        Console.WriteLine("TaskAsync Finished");
    }

    async Task TaskAsync2()
    {
        Console.WriteLine("TaskAsync2 Started");
        await Task.Delay(1000);
        Console.WriteLine("TaskAsync2 Finished");
    }

    await TaskAsync();
    await TaskAsync2();
    /*
        TaskAsync Started
        TaskAsync Finished
        TaskAsync2 Started
        TaskAsync2 Finished
    */
    ```
    - `await` 키워드는 비동기 작업이 완료될 때까지 현재 메서드의 실행을 중단하고, 완료 후 이어서 실행되도록 한다 (블로킹(blocking)은 아님)
    - 이 코드는 비동기 메서드를 순차적으로 await하고 있기 때문에 결과적으로 순차 실행처럼 보인다.
- 예제 코드
    ```cs
    // 위 코드의 실행 구문을 수정한다.
    Task task1 = TaskAsync();
    Task task2 = TaskAsync2();

    await Task.WhenAll(task1, task2);
    /*
        TaskAsync Started
        TaskAsync2 Started
        TaskAsync2 Finished
        TaskAsync Finished
    */
    ```
    - 두 작업은 병렬적으로(동시에) 진행된다
    - `Task.WhenAll()` : Task.WhenAll()은 여러 개의 Task를 받아 모든 Task가 완료되었을 때 완료되는 하나의 Task를 반환한다. await와 함께 사용하면 모든 비동기 작업이 끝날 때까지 기다릴 수 있다.
    - 비동기 메서드를 동시에 실행하려면 await하기 전에 먼저 Task를 생성해야 한다.

## 비동기 (진행 흐름 - UI가 없는 환경)
- 비동기의 흐름은 크게 UI가 있는 경우와 없는 경우로 나뉜다.</br>(정확히는 SynchronizationContext의 존재 여부에 따라 달라진다.)
- UI가 없는 경우에는 Console, ASP.Net Core 이 해당한다.</br>(ASP.NET의 경우 정확히는 UI SynchronizationContext가 없는 환경이라고 부른다.)
- 흐름 예시
    ![UI가 없는 환경에서의 흐름](./_img/20_진행%20흐름_UI가%20없는%20환경.png)

### Synchronization 이란?
- 동기화(Synchronization)의 가장 일반적인 의미는 여러 실행 흐름(스레드/작업)의 실행 순서, 시점, 접근을 맞추는 것을 뜻한다.
- 즉, 동시에 실행되는 것들 사이의 충돌, 순서 꼬임, 경쟁 상태(Race Condition)을 방지하기 위한 개념이다.
- 대표적인 Synchronization의 예
    |대상|목적|
    |---|---|
    |`lock`,`Monitor`|동시에 접근하지 못하게|
    |`Mutex`,`Semaphore`|접근 개수 제어|
    |`Interlocked`|원자적 연산|
    |`volatile`|메모리 가시성|
    ☝️ 이건 전부 데이터 보호 중심이다.
- .NET의 SynchronizationContext에서의 Synchronization은 조금 다른의미를 가진다. → "코드를 어디에서 실행하나?"
- 데이터가 아니라 실행 컨텍스트(스레드/큐/루프)를 맞추는 개념
- 즉, 해당 작업이 반드시 실행되어야하는 특정 환경으로 보내(post)주는 역할을 한다.

### UI Synchronization은 무엇인가?
- UI는 단일 스레드 모델을 사용하기에 모든 UI 접근을 "UI 스레드"라는 하나의 실행 컨텍스트로 동기화해야 한다.
- 이 때문에 생겨난것이 WinForm의 "WindowsFormsSynchronizationContext", WPF의 "DisptcherSynchronizationContext"다.
- UI Synchronization이 필요한 이유 :
    - UI 컨트롤은 Thread-Safe하지 않다.
    - 내부 상태가 매우 복잡함
    - 다중 스레드 접근 시 깨짐
- 이러한 이유로 UI 작업은 항상 UI 스레드에서 실행하라는 규칙을 강제한다.

### 일반 Synchronization vs UI Synchronization 비교
|구분|일반 Synchronization|UI Synchronization|
|---|---|---|
|대상|데이터/자원|실행 위치|
|목적|경쟁 상태 방지|UI 스레드 강제|
|방식|lock,Mutex 등|Context Post|
|스레드 차단|⭕|❌|
|논블로킹|보장 못함|⭕|
- 일반 콘솔/ASP.NET의 동기화와 WinFrom의 동기화는 같은 단어를 사용하지만 의미가 다르다.
- 또한, Synchronization은 UI에만 있는 개념은 아니며, WinForm에서는 UI 전용 구현체(WindowsFormsSynchronizationContext)로 사용된다.
- SynchronizationContext는 비동기 후속 코드를 어디서 실행할지 결정하는 추상 개념이다.
    |환경|SynchronizationContext|
    |---|---|
    |WinForm|WindowsFormsSynchronizationContext|
    |WPF|DispatcherSynchronizationContext|
    |ASP.NET (구버전)|AspNetSynchronizationContext|
    |Console / ASP.NET Core|❌ (없음, null)|
    ☝️ 실제 구현 예

## 비동기 (WinForm UI 생성)
- UI가 있는 환경을 위해 프로젝트를 새로 만들어준다.
    ```
    VS2022 → 새 프로젝트 만들기 → Windows From 앱 → 프로젝트 이름 설정(AsyncUI) → 프레임워크 설정(.NET 8.0) → 프로젝트 생성
    ```
- 프로젝트가 생성되면 디자인 창이 보인다. 
- 솔루션 탐색기에서 Form1.cs 을 우클릭하고 "코드 보기"를 선택해주면 디자인창에 해당하는 코드 페이지가 열린다.
- 화면 구성시 필요한 창들을 "보기"에서 열어준다 (도구상자, 속성)
- 도구상자에서 button 을 검색하여 WinForm 창에 만들어주고 버튼을 더블클릭하면 해당 버튼의 리스너 함수가 코드 페이지에 자동으로 생성된다.
- 버튼의 속성은 속성창에서 수정할 수 있다. (Name : 객체 이름, Text : 버튼의 텍스트 내용)
- WinForm 프로젝트 페이지
    ![WinForm 프로젝트 페이지](./_img/20_WinForm%20UI%20생성_WInForm%20프로젝트.png)

## 비동기 (진행 흐름 - UI가 있는 환경)
- 예제 코드
    ```cs
    using System.Diagnostics;

    namespace AsyncUI
    {
        public partial class Form1 : Form
        {
            public Form1()
            {
                InitializeComponent();
            }

            // 1. 버튼 클릭 이벤트 발생 → UI 스레드에서 btnAsync_Click 실행
            private async void btnAsync_Click(object sender, EventArgs e)
            {
                // 2. TaskAsync1() 호출
                // → await 이전 코드까지는 즉시 UI 스레드에서 실행
                // → 첫 await에서 비동기 작업 등록 후 제어 반환
                Task task1 = TaskAsync1();
                // 4. TaskAsync2() 호출 (동일한 흐름)
                Task task2 = TaskAsync2();

                // 6. 이미 실행 중인 두 Task의 완료를 논블로킹으로 대기
                // → 스레드를 점유하지 않음
                // → await 시 UI SynchronizationContext 캡처
                await Task.WhenAll(task1, task2);

                // 7. 두 Task 완료 후 continuation 실행
                // → 캡처된 WindowsFormsSynchronizationContext를 통해
                //   UI 스레드에서 실행된 뒤 메서드 종료
            }

            async Task TaskAsync1()
            {
                Debug.Print("TaskAsync1 Started");
                // 3. 타이머 기반 비동기 작업 등록
                // → UI 스레드 차단 없이 반환
                // → 완료 시 continuation을 UI SynchronizationContext에 Post
                await Task.Delay(3000);
                Debug.Print("TaskAsync1 Finished");
            }

            async Task TaskAsync2()
            {
                Debug.Print("TaskAsync2 Started");
                // 5. 타이머 등록 후 UI 스레드 반환
                await Task.Delay(1500);
                Debug.Print("TaskAsync1 Finished");
            }
        }
    }
    ```
    - await 를 만나게되면 현재 실행 중이던 메서드의 나머지 실행을 중단하고, UI 스레드를 WinForm 메세지 루프에게 다시 넘긴다.
    - "두 Task 완료 후 continuation 실행" == `await Task.WhenAll(task1, task2)` 이 줄 다음에 있는 코드를 두 Task가 모두 완료된 후에 실행한다
    - "완료 시 continuation을 UI SynchronizationContext에 Post" == 해당 continuation을 이 컨텍스트가 관리하는 실행 환경에서 실행해 달라
    - "Post" 라는 표현을 사용하는 이유는 UI 에서는 절대 UI 스레드를 즉시 점유하지 않기때문이다.
        - Send : 즉시 실행 (동기)
        - Post : 큐에 넣고 나중에 실행 (비동기)
    - WinForm 에서는 async/await가 메인 스레드로 돌아온다. → WinForm의 SynchronizationContext 때문
    - WinForm 앱이 시작되면 UI 스레드 전용 WindowsFormsSynchronizationContext가 설정되며, await를 만나면 현재 SynchronizationContex를 캡쳐한다.
- WinForm에서 비동기 코드가 동일한 메인 스레드에서 동작하는 것처럼 보이는 이유는 await가 현재 SynchronizationContext(UI 스레드)를 캡쳐하고, 비동기 작업 완료 후 그 컨텍스트를 통해 다시 UI 스레드에서 continuation을 실행하기 때문이다.

### continuation 이란?
- continuation은 await 이후에 실행되어야 할 "나머지 코드"를 의미한다.
- await 이후의 코드는 현재 실행되지 않고 "나중에 실행할 코드"로 등록된다.
- "나중에 실행할 코드 묶음"이 바로 continuation 이다.

### "비동기 == 멀티스레드"는 아니다.
```cs
async void Button_Click(...)
{
    await Task.Delay(1000);
    label1.Text = "완료";
}
```
- 타이머 기반 비동기는 스레드를 점유하지 않음
- 완료 후 UI SynchronizationContext로 복귀
- 때문에 모든 코드가 같은 스레드에서 도는 것처럼 보임
- 다만 WinForm UI 프로그램이 항상 같은 스레드만 사용하는 것은 아니며, `Task.Delay` 같은 타이머 기반 비동기는 스레드를 사용하지 않기 때문에 위의 모든 코드가 같은 UI 스레드에서 실행되는 것처럼 보이는 것이다.

## 비동기 (Context Switching)
- UI에 보여지는 작업은 반드시 주 스레드에서 실행이 되어야한다.
- 만약 주 스레드가 아니라 다른 스레드에서 실행이 되면 Cross Thread Error가 발생한다.
- 예제 코드
    ```cs
    // TaskAsync1 을 수정
    async Task TaskAsync1()
    {
        lbLog.Items.Add("TaskAsync1 Started");
        await Task.Delay(3000).ConfigureAwait(false);   // ContextSwitching 사용x
        lbLog.Items.Add("TaskAsync1 Finished");
    }
    ```
    - `ConfigureAwait()` : 기본값은 true로 현재 Synchronization을 캡쳐하여 비동기 작업 완료 후 같은 컨텍스트에서 continuation을 실행하도록한다.
    - false 로 설정하면 SynchronizationContext를 캡처하지 않고 UI 스레드로 복귀하지 않게된다.
    - false의 의미 : "이 await 이후의 continuation은 현재 SynchronizationContextUI로 돌아오지 말고, 아무 ThreadPool 스레드에서나 실행해도 된다."
    - 에러 메세지
        ```
        System.InvalidOperationException: '크로스 스레드 작업이 잘못되었습니다. 'lbLog' 컨트롤이 자신이 만들어진 스레드가 아닌 스레드에서 액세스되었습니다.'
        ```
- UI는 단일 스레드 모델이기 때문에 UI 접근은 반드시 UI 스레드에서만 가능하다. 때문에 SynchronizationContext가 continuation을 UI 스레드의 메세지 큐에 Post 한다.
- UI 스레드에서 실행하도록 예약된 continuation이 처리되는 과정에서, OS 스케줄링에 따라 스레드 전환(Context Switching)이 발생할 수 있다.
- Context Switching은 continuation을 UI 스레드에서 실행하기 위한 ‘목적’이 아니라, 그 실행을 OS가 스케줄링하는 과정에서 ‘부수적으로 발생할 수 있는 결과’다.

### SynchronizationContext 캡처의 의미
- SynchronizationContext를 캡처한다는 것은 await 시점의 이후 코드를 어디서 실행할지를 저장해둔다는 의미이다.
- 따라서 캡처하지 않는다는 것은 이후 코드를 굳이 원래 환경으로 돌려보내지 않겠다는 의미가된다.

## 비동기와 동기의 차이 : Non-Blocking
- 간단메모 :
    - 동기와 비동기의 차이를 보여주기위해 WinForm에서 동기 테스트 버튼을 만들어 시각적인 예시를 만들어봄

## 비동기 (반환 타입 Task, WhenAll)
- 간단메모 :
    - Task.WhenAny(List<Task>)
    - While 문과 WhenAny() 함수로 먼저 완료된 Task 를 꺼내어 출력하는 예제코드 작성

## 비동기 (deadlock 원인 및 해결방안)
- 간단메모 :
    - 데드락 발생원인과 해결방법
    - Task.Wait() : 비동기를 동기적으로 기다리게하는 기능
    - ConfigureAwait(false)
    - Task.Result

## 비동기 (스트림-IAsyncEnumerable)
- 간단메모 :
    - 비동기 열거 타입 : IAsyncEnumerable
    - await foreach
    - 비동기 스트림 구현 코드 작성

## 비동기 (CancelationTokenSource - 1)
- 간단메모 : 
    - 비동기 스트림 중지 기능
    - CancelationTokenSource
    - CancelationToken
    - 비동기 캔슬시 try-catch 로 예외처리 필요함 (TaskCanceledException 발생)
    - \[EnumeratorCancellation\] 속성
    - CancelationTokenSource 는 사용시 메모리 해제를 직접해줘야한다. (finally 에서 해줄것)
    - 비동기 작업이 여러번 동작하지않도록 방지코드 작성해줄것

## 비동기 (CancelationTokenSource - 2)
- 간단메모 : 
    - CancelationToken.ThromIfCancellationRequested() : 해당 코드 실행전에 CancelationTokenSource.Cancel() 함수 호출시 해당 코드 실행시 OperationCanceledException 발생시킴, 예외를 바로 발생시키지않고 해당 코드가 실행되는경우 발생시킴
    - Delay() 의 인자로 token 을 넣어주는 코드와 함께 사용하여 비동기 작업을 중지시킨다.
    - CancelationToken.IsCancellationRequested : 해당 코드 실행전에 CancelationTokenSource.Cancel() 호출시 해당 코드 실행시 true를 반환함

## 비동기 (Task.Run - 무거운 연산 처리)
- 간단메모 : 
    - 비동기에서 큰 연산을 수행하는경우 어떻게하면 좋은가?
    - Task.Run() : 가벼운 ThreadPool 사용시 활용

## 비동기 (Task.Factory.StartNew)
- 간단메모 : 
    - Task.Factory : Task.Run()에서 다양한 기능을 추가해서 사용하는 기능
    - Task.Factory.StartNew()
    - TaskCreationOptions.AttachedToParent

## 비동기 (파일 생성, 복사 실습)
- 간단메모 : 
    - 비동기 활용 예제
