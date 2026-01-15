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
## 비동기 (Context Switching)
## 비동기와 동기의 차이 : Non-Blocking
## 비동기 (반환 타입 Task, WhenAll)
## 비동기 (deadlock 원인 및 해결방안)
## 비동기 (스트림-IAsyncEnumerable)
## 비동기 (CancelationTokenSource - 1)
## 비동기 (CancelationTokenSource - 2)
## 비동기 (Task.Run - 무거운 연산 처리)
## 비동기 (Task.Factory.StartNew)
## 비동기 (파일 생성, 복사 실습)