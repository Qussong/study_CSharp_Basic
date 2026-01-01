# Section 5

## 디버깅
- 코드가 길어지면 전체 흐름을 한 줄씩 따라가기 어려운 상황이 발생한다. 이럴 때 "디버깅(Debugging)" 기능을 활용하면 코드의 실행 과정을 단계별로 확인할 수 있다.
- 기존에 `F5` 키를 눌러 코드를 실행하던 방식은 메뉴에서 "디버그 → 디버깅 시작"을 선택하는 것과 동일한 기능이다.
- 디버깅 컨트롤</br>
    - F5 : 디버깅 시작/계속
    - F9 : 중단점 설정
    - F10 : 한 줄씩 실행
    - F11 : 한 줄씩 들어가기
    - Shift + F11 : 함수에서 나오기
    - Ctrl + Shift + F5 : 디버깅 다시 시작
- 디버깅 창 활용</br>
    - 지역(Locals) 창</br>
        - 현재 스코프 내의 모든 변수와 그 값을 보여준다/
    - 자동(Autos) 창</br>
        - 현재 라인과 관련된 변수들을 자동으로 표시한다.
    - 호출스택(Call Stack)</br>
        - 현재 실행되고 있는 함수 호출의 순서를 트리 형태로 보여준다.
    - 조사식</br>
        - 특정 변수나 표현식을 직접 추가하여 값을 모니터링할 수 있다.
        - Watch1, Watch2 등 여러 감시 창을 활용할 수 있다.
    - 직접실행 창</br>
        - 디버깅 중 조회, 할당, 함수 실행을 할 수 있다.

# Section 6

조건문은 특정 조건을 평가하고 그 조건에 따라 다른 코듭 블록을 실행한다.

## if, 삼항 연산자
- `if`</br>
    조건이 참일 때만 코드 블록을 실행한다.
    ```csharp
    if (조건)
    {
        // 조건이 참일 때 실행
    }
    ```
- `if-else`</br>
    조건이 참이면 if 블록을, 거짓이면 else 블록을 실행한다.
    ```csharp
    if (조건)
    {
        // 조건이 참일 때 실행
    }
    else
    {
        // 조건이 거짓일 때 실행
    }
    ```
- `else if`</br>
    여러 조건을 순차적으로 평가하고, 첫 번째로 참인 조건의 코드 블록을 실행한다.
    ```csharp
    if (조건1)
    {
        // 조건1이 참일 때 실행
    }
    else if (조건2)
    {
        // 조건2가 참일 때 실행
    }
    else
    {
        // 모든 조건이 거짓일 떄 실행
    }
    ```
- `삼항연산자`</br>
    조건문을 간단히 표현할 때 사용된다. (if-else문의 축약형이다.)
    |연산자|설명|예시|
    |--|--|--|
    |?:|조건식이 참이면 참(a)일때의 값을 거짓이면 거짓(b)일때의 값을 반환한다.|int result = (a > b) ? a : b|
    ```csharp
    Console.WriteLine("숫자를 입력하세요.");
    string? input = Console.ReadLine();

    int number = int.Parse(input ?? "0");
    string text = number % 2 == 0
        ? "입력한 숫자는 짝수입니다."
        : "입력한 숫자는 홀수입니다.";

    Console.WriteLine(text);
    ```
- `Console.ReadLine()`</br>
    사용자의 입력을 받을 수 있다.
    ```csharp
    Console.WriteLine("숫자를 입력하세요.");
    string? input = Console.ReadLine();
    Console.WriteLine("입력한 숫자는" + input + "입니다.");
    ```

## switch
- switch문은 하나의 변수를 여러 값과 비교하여, 일치하는 경우 해당 코드 블록을 실행한다.
    ```csharp
    switch (표현식)
    {
        case 값1:
            // 값1일 때 실행될 코드
            break;
        case 값2:
            // 값2일 때 실행될 코드ㄴ
            break;
        default:
            // 모든 case에 해당하지 않을 때 실행될 코드
            break;
    }
    ```
    ```csharp
    string grade = "B+";
    switch(grade)
    {
        case "A":
            Console.WriteLine("우수한 성적입니다.");
            break;
        //case "B":
        case string g when g == "B" || g == "B+":
            Console.WriteLine("좋은 성적입니다.");
            break;
        case "C":
            Console.WriteLine("보통 성적입니다.");
            break;
        default:
            Console.WriteLine("잘 모르겠습니다.");
            break;
    }
    ```
- switch 표현식 (C# 8.0 이상) :</br>
    switch 문을 더 간결하게 사용할 수 있는 표현식이다.
    ```csharp
    var 결과 = 표현식 switch
    {
        값1 => 결과1,
        값2 => 결과2,
        _ => 기본결과
    };
    ```
    ```csharp
    string grade = "B+";
    string message = grade switch
    {
        "A" => "우수한 성적입니다.",
        string g when g == "B" || g == "B+" => "좋은 성적입니다.",
        "C" => "보통 성적입니다.",
        _ => "잘 모르겠습니다."
    };
    Console.WriteLine(message);
    ```