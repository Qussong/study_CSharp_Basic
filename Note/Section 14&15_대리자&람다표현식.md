# Section 14

## 대리자
- delegate 는 특정 메서드의 참조를 캡슐화하는 객체다.
- 메서드를 변수처럼 사용하겠다는 의미
- 만드는 법 : delegate 키워드를 사용한다.
    ```cs
    
    void MyMethod()                     // 2. 함수 정의
    {
        Console.WriteLine("안녕하세요.");
    }

    MyDelegate myDelegate = MyMethod;   // 3. 대리자 선언 및 할당

    myDelegate();                       // 4. 대리자 호출

    delegate void MyDelegate();         // 1. 대리자 정의
    ```
- 대리자는 메서드를 참조하여 대리자를 메서드를 대신하여 사용된다.

## 매개변수가 있는 대리자
```cs
void Plus(int a, int b)
{
    Console.WriteLine($"a + b = {a + b}");
}

Operation operation = Plus;

operation(10, 5);   // a + b = 15

delegate void Operation(int a, int b);
```

## 반환 값이 있는 대리자
```cs
int Plus(int a, int b)
{
    return a + b;
}

Operation operation = Plus;

int result = operation(10, 5);
Console.WriteLine(result);  // 15

delegate int Operation(int a, int b);
```

## 대리자 - 멀티 캐스팅
- 대리자는 하나의 메서드만이 아닌 여러개의 메서드를 참조할 수 있다.
    ```cs
    int Plus(int a, int b)
    {
        Console.WriteLine($"a + b = {a + b}");
        return a + b;
    }

    int Minus(int a, int b)
    {
        Console.WriteLine($"a - b = {a - b}");
        return a - b;
    }

    Operation operation = Plus;
    operation += Minus;

    int result = operation(10, 5);
    Console.WriteLine(result);
    /*
    * a + b = 15
    * a - b = 5
    * 5    <- 제일 마지막에 참조한 메서드의 결과를 반환
    */

    operation -= Plus;  // 참조 메서드 제거
    result = operation(20, 5);
    Console.WriteLine(result);
    /* 
    * a - b = 15
    * 15
    */

    // 대리자 선언
    delegate int Operation(int a, int b);
    ```

## 대리자 - 이벤트
- 대리자는 주로 이벤트를 발생시킬 때 많이 사용된다.
    ```cs
    Calculate calculate = new Calculate();
    calculate.OnValueChanged += Calculate_OnValueChanged;   // 이벤트 등록

    void Calculate_OnValueChanged(int result, string message)
    {
        Console.WriteLine($"{message} - 현재 값 : {result}");
    }
        
    calculate.Plus(5);      // 5을 더했습니다. - 현재 값 : 5
    calculate.Plus(3);      // 3을 더했습니다. - 현재 값 : 8
    calculate.Minus(2);     // 2을 뻈습니다. - 현재 값 : 6
    calculate.Minus(10);    // 10을 뻈습니다. - 현재 값 : -4

    // 대리자 선언
    delegate void ValueChangeHandler(int result, string message);

    // 클래스 정의
    class Calculate()
    {
        // 이벤트 선언
        public event ValueChangeHandler? OnValueChanged;
        private int _value;

        public void Plus(int value)
        {
            _value += value;
            // case 1. null 검사
            if(null != OnValueChanged)
                OnValueChanged(_value, $"{value}을 더했습니다.");   // 대리자 호출
        }

        public void Minus(int value)
        {
            _value -= value;
            // case 2. Invoke() 를 통한 호출
            OnValueChanged.?Invoke(_value, $"{value}을 뻈습니다.");     // 대리자 호출
        }
    }
    ```
- 이벤트를 제거하는 방법은 멀티 캐스트와 동일하게 `-=` 연산자를 사용해주면된다.

### Note_event 사용 이유
- C#의 event는 델리게이트를 안전하게 공개하기 위한 언어 차원의 접근 제어 장치다.
- 일반 델리게이트는 "함수 포인터 필드" 다.
    ```cs
    public MyDelegate myDelegate;
    ```
    외부에서 모든 조작이 가능하다.
    ```cs
    obj.myDelegate = Foo;  // 교체
    obj.myDelegate = null; // 제거
    obj.myDelegate();      // 직접 호출
    ```
    소유권이 완전히 외부에 있다.
- event 는 "델리게이트에 대한 제한된 인터페이스" 다.
    ```cs
    public event MyDelegate OnMyDelegate
    ```
    외부에서 가능한 것은 구독, 구독해제 뿐이다.
    ```cs
    obj.OnMyDelegate += Foo;    // 구독
    obj.OnMyDelegate -= Foo;    // 구독 해제
    ```
    외부에서 절대 못하는 것들
    ```cs
    obj.OnMyDelegate = Foo; // ❌
    obj.OnMyDelegate = null;// ❌
    obj.OnMyDelegate();     // ❌
    ```
    ⭐ **호출 권한은 오직 선언한 클래스 내부에만 있다.**
- 델리게이트만 사용하면 생기는 문제들 :
    ```cs
    class Button
    {
        public Action Clicked;
    }

    button.Clicked += OnClick;
    button.Clicked = null;      // 💥기존 구독자 모두 삭제
    button.Clicked();           // 💥외부에서 강제 실행
    ```
    위의 상황은 완전히 캡슐화가 깨진 상태다.
- event 가 해결하는 것 : 
    ```cs
    class Button
    {
        public event Action Clicked;
    }

    button.Clicked += OnClick;  // 구독 가능
    button.Clicked();           // ❌ 컴파일 에러 - 외부호출 불가능

    // 클래스 내부에서만 호출 할 수 있다.
    Clicked?.Invoke();          // ⭕
    ```
- 즉, delegate는 "함수 자체"이고, event는 "함수를 등록할 수 있는 권한만 공개한 계약"이다.

### Note_Invoke()
- `Invoke`는 델리게이트 인스턴스가 참조하고 있는 메서드를 호출하는 메서드
    ```cs
    delegate void MyDelegate();

    MyDelegate d = Foo;
    d.Invoke(); // Foo 호출
    d();        // 완전히 동일
    ```
    `d()` 는 `d.Invoke()` 의 문법 축약(Syntax Sugar) 이다.
- 델리게이트는 객체이고 "객체라면 메서드를 가진다"는 개념이 필요했다. 이에 생겨난 메서드가 Invoke() 다.
- 이벤트의 경우 외부에서 Invoke() 를 할 수 없다.
- 이벤트는 Invoke() 권한을 제한한 델리게이트다.

## 대리자 - 함수 매개변수
- 매개변수로 함수를 넘겨받을 수 있다.
    ```cs
    void ApplyOperation(int a, int b, Operation operation)
    {
        int result = operation(a, b);
        Console.WriteLine(result);
    }

    int Plus(int a, int b) => a + b;
    int Minu(int a, int b) => a - b;

    ApplyOperation(5, 10, Plus);    // 15;
    ApplyOperation(5, 10, Minu);    // -5;

    delegate int Operation(int a, int b);
    ```

## 대리자 - Func
- 매번 대리자를 선언하는것은 비효율적이다.
- `Func` : 사전에 정의되어있는 대리자 중 하나로 반환값이 있는 메서드의 캡슐화
- Func 대리자의 경우 Generic 으로 매개변수과 반환값을 넣어준다.
    ```cs
    void ApplyOperation(int a, int b, Func<int, int, int> operation)
    {
        int result = operation(a, b);
        Console.WriteLine(result);
    }

    int Plus(int a, int b) => a + b;
    int Minu(int a, int b) => a - b;

    ApplyOperation(5, 10, Plus);    // 15;
    ApplyOperation(5, 10, Minu);    // -5;
    ```

## 대리자 - Action
- `Action` : 반환값이 없는 메서드의 캡슐화
    ```cs
    void ActionMethod(string s, int i)
    {
        // 함수 기능
    }

    Action<string, int> action = ActionMethod;
    ```
- 매개변수로 16개 까지 지원을 하지만, 이러한 코드 작성은 추천되지 않는다.

## 대리자 - Predicate
- `Predicate` : 조건에 따라 bool 값을 반환하는 메서드의 캐슐화
    ```cs
    bool IsGreaterThanZero(int value)
    {
        return value > 0;
    }

    Predicate<int> predicate = IsGreaterThanZero;
    Console.WriteLine(predicate.Invoke(2));  // True
    ```

## 대리자  - Comparison
- `Comparison` : 조건에 따라 bool 값을 반환하는 메서드의 캐슐화
    ```cs
    bool IsGreaterThanZero(int value)
    {
        return value > 0;
    }

    Predicate<int> predicate = IsGreaterThanZero;
    Console.WriteLine(predicate.Invoke(2));  // True
    ```

# Section 15

## 람다(Lambda) 표현식
- 매개변수를 받아 특정 작업을 수행하는 "익명 함수"를 정의하는 방법
- 기존의 메서드를 정의하지 않고도 간단한 함수를 작성할 수 있어 코드의 가독성과 간결성을 높여준다.
    ```cs
    (매개변수) => { 표현식 }    // 형식
    ```
- Func 예제
    ```cs
    // base case
    Func<int, int, int> operation = Plus;
    Console.WriteLine(operation(5, 10));    // 15
    int Plus(int a, int b)
    {
        return a + b;
    }

    // case 1
    Func<int, int, int> operation2 = (int a, int b) => { return a + b; };
    // case 2
    Func<int, int, int> operation3 = (a, b) => { return a + b; };   // Func 에서 int 타입이 명시되어있기에 매개변수 타입 생략가능
    // case 3
    Func<int, int, int> operation4 = (a, b) => a + b;   // 표현식이 한줄이라면 본문 표현식 활용가능

    Console.WriteLine(operation2(5, 10));   // 15
    Console.WriteLine(operation3(5, 10));   // 15
    Console.WriteLine(operation4(5, 10));   // 15
    ```
- Action 예제
    ```cs
    // base case
    Action<int, int> operation = (int a, int b) => { Console.WriteLine(a + b); };
    operation(5, 10);   // 15

    // case 1
    Action<int, int> operation2 = (a, b) => { Console.WriteLine(a + b); };  // 매개변수 타입 생략
    // case 2
    Action<int, int> operation3 = (a, b) => Console.WriteLine(a + b);       // 본문 표현식 활용

    operation2(5, 10);   // 15
    operation3(5, 10);   // 15
    ```
