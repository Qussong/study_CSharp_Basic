## 예외 처리 (정의 및 try-catch)
- 프로그램 실행 중 발생할 수 있는 예기치 않은 오류를 효과적으로 관리하여 프로그램의 안정성과 신뢰성을 높이는 기능
- `try-catch` : try 에는 예외처리할 본문이 위치한다. catch 에는 예외처리시 실행될 구문이 위치한다.
    ```cs
    try
    {
        // 본문
    }
    catch
    {
        Console.WriteLine("예외가 발생했습니다.");
    }
    ```

## 예외 처리 (예외 객체)
- 예외 객체란 예외 핸들러의 매개변수를 의미한다.
- `catch` 다음에 소괄호를 만들고 그 안에 위치시킨다.
    ```cs
    try { }
    catch (Exception ex) { }
    ```
- `Exception` 은 모든 예외의 부모에 해당한다.
- `IndexOutOfRangeException` -> `SystemException` -> `Exception` 상속구조
- 컴파일러는 언박싱 전까지 타입을 확인할 수 없기에 오류를 발생시키지 않는다.
    ```cs
    object obj = "abc";
    try
    {
        // UnBoxing 전까지 오류발생하지 않음
        // System.InvalidCastException 발생
        double d = (double)obj;
    }
    //catch (Exception ex)
    catch (IndexOutOfRangeException ex) // 타입이 다르기에 예외검출 안됨
    {
        // 오류 처리 구문
    }
    ```

## 예외 처리 (finally)
- 예외 발생여부와 관계없이 항상 실행되는 블록으로, 주로 자원 정리에 사용된다.
- try 블록 내 코드 실행 중 예외가 발생하든 안 하든, catch 블록이 실행되든 안 되든 상관 없이 항상 실행이 보장되는 코드 블록인다.
- 예외처리가 정상적으로 이루어진다면 try-catch 문 이후의 코드는 어차피 실행되는데 finally 는 왜 필요한가?
- finally는 흐름제어를 하더라도 반드시 실행된다.
    ```cs
    // case 1)
    object obj = "abc";
    try
    {
        double d = (double)obj;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Exception 예외가 발생했습니다.");
        goto toEnd;
    }

    Console.WriteLine("Hello, World");
    toEnd:

    // Exception 예외가 발생했습니다
    ```
    ```cs
    // case 2
    object obj = "abc";
    try
    {
        double d = (double)obj;
    }
    catch (Exception ex)
    {
        Console.WriteLine("Exception 예외가 발생했습니다.");
        goto toEnd;
    }
    finally
    {
        Console.WriteLine("Hello, World");
    }

    Console.WriteLine("Hello, World 2");
    toEnd:

    //Exception 예외가 발생했습니다.
    //Hello, World
    ```
- 활용 예제
    ```cs
    object obj = "abc";

    string Error()
    {
        try
        {
            double d = (double)obj;
        }
        catch (Exception ex)
        {
            return "Exception 예외가 발생했습니다.";
        }
        finally 
        {
            obj = null;
        }

        return null;
    }

    string errorMsg = Error();
    Console.WriteLine($"errorMsg : {errorMsg}, obj : {obj}");
    // errorMsg : Exception 예외가 발생했습니다., obj :
    ```
    예외가 발생하여 catch 문의 return 이 호출되었음에도 finally 문이 동작하여 obj 를 null 로 초기화 한것을 확인할 수 있다.

## 사용자 정의 예외 처리
- `throw` : 예외를 발생시키거나 던질 때 사용한다.
- 커스텀 예외 생성 실습
    ```cs
    try
    {
        throw new CustomException("커스텀 에러 발생");
    }
    catch (CustomException ex)
    {
        Console.WriteLine(ex.Message);
    }
    // 커스텀 에러 발생

    Console.ReadKey();

    class CustomException : Exception 
    {
        public CustomException(string msg) : base(msg)
        {
            
        }
    }
    ```
- 응용 실습
    ```cs
    try
    {
        throw new CustomException("커스텀 에러 발생", 500);
    }
    catch (CustomException ex)
    {
        Console.WriteLine($"Message : {ex.Message}, ErrorCode : {ex.ErrorCode}");
    }
    // Message : 커스텀 에러 발생, ErrorCode : 500

    class CustomException : Exception 
    {
        public CustomException(string msg, int errorCode) : base(msg)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; }
    }
    ```

## 사용자 정의 예외 처리의 장점
- 예제 상황 : 잔액부족 클래스의 값을 전달하기위해 다중 메서드가 호출된다. 너무 많은 코드가 호출되었다.
    ```cs
    void 출금(out 잔액부족 부족)
    {
        잔액확인1(out 부족);
    }

    void 잔액확인1(out 잔액부족 부족)
    {
        잔액확인2(out 부족);
    }

    void 잔액확인2(out 잔액부족 부족)
    {
        잔액확인3(out 부족);
    }

    void 잔액확인3(out 잔액부족 부족)
    {
        부족 = new 잔액부족 { 잔액 = 5000, 출금액 = 10000 };
        // 잔액 부족 예외 발생
        // 잔액 : 5,000원 , 출금 10,000원
    }

    출금(out 잔액부족 부족);
    Console.WriteLine($"잔액 : {부족.잔액}, 출금 : {부족.출금액}");
    // 잔액 : 5000, 출금 : 10000

    class 잔액부족
    {
        public Decimal 잔액 { get; set; }
        public Decimal 출금액 { get; set; }
    }
    ```
- 위의 예제를 커스텀 예외처리를 통해 가독성 높게 수정
    ```cs
    void 출금()
    {
        잔액확인1();
    }

    void 잔액확인1()
    {
        잔액확인2();
    }

    void 잔액확인2()
    {
        잔액확인3();
    }

    void 잔액확인3()
    {
        throw new 잔액부족Exception("잔액이 부족합니다.", 5000, 10000);
        // 잔액 부족 예외 발생
        // 잔액 : 5,000원 , 출금 10,000원
    }

    try
    {
        출금();
    }
    catch (잔액부족Exception ex)
    {
        Console.WriteLine($"Message : {ex.Message}, 잔액 : {ex.잔액}, 출금 : {ex.출금액}");
    }
    // Message : 잔액이 부족합니다., 잔액 : 5000, 출금 : 10000

    Console.ReadKey();

    class 잔액부족Exception : Exception
    {
        public 잔액부족Exception(string message, decimal 잔액, decimal 출금액) : base(message)
        {
            this.잔액 = 잔액;
            this.출금액 = 출금액;
        }

        public decimal 잔액 { get; }
        public decimal 출금액 { get; }
    }
    ```
- 커스텀 예외처리를 활용하여 예외 발생시 필요한 데이터를 쉽게 전달 받을 수 있다.