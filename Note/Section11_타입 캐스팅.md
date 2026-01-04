## 암시적 변환
- 본인보다 큰 데이터 타입으로 변환되는 경우 사용하며, 데이터 손실이 발생하지 않는다.
    ```cs
    int intNum = 100;
    double doubleNum = intNum;
    Console.WriteLine($"int : {intNum}, double : {doubleNum}");
    // int : 100, double : 100
    ```

## 명시적 변환
- 큰 타입에서 작은 타입으로 변환될 때 사용하며, 데이터의 손실이 발생할 수 있다.
    ```cs
    double anotherDouble = 123.456;
    int anotherInt = (int)anotherDouble;
    Console.WriteLine($"double : {anotherDouble}, int : {anotherInt}");
    // double : 123.456, int : 123
    ```
- 큰 타입에서 작은 타입으로 변환시 명시적 변환을 하지 않는다면 오류가 발생한다.
    ```cs
    double anotherDouble = 123.456;
    int anotherInt = anotherDouble; // ❌ 오류발생 (double -> int)
    ```

## object (boxing, unboxing)
- object 타입은 모든 타입의 최상위 부모 타입이다.
- 모든 타입을 대입할 수 있다.
    ```cs
    object stringObject = "C# Programming";
    object intObject = 123;
    object doubleObject = 3.14;
    object boolObject = true;
    object classObject = new TestClass();

    class TestClass() { }
    ```
- `boxing` : object 타입으로 변환하는 과정
- `unboxing` : object 타입에서 다른 타입으로 변화하는 과정
    ```
    type --(boxing)--> object --(unboxing)--> type
    ```
- boxing 과 unboxing은 object 타입과 값 타입간의 변화에 관련있다.
- 값 타입을 힙 메모리에 할당하고 접근하는 과정을 포함한다. 이는 스택 기반의 값 타입 직접 접근보다 오버헤드가 커 성능저하의 원인이 될 수 있기에 가능하면 피하는 것이 좋다.
- unboxing 시 잘못된 타입으로 변환을해도 실행하기전까진 오류가 발생하지 않기에 조심해야한다.

### Note_boxing/unboxing 의 성능저하
- 값 타입을 object 로 변환하면 boxing 은 값 타입을 힙에 올리는 과정이 된다. (참조 타입을 object 로 변환하는경우 이는 boxing이 아니다.)
    ```cs
    int x = 10;
    object obj = x; // boxing
    ```
    위 과정에서 벌어지는 일
    1. 힙에 새로운 객체 생성
    2. 그 안에 10 복사
    3. obj 는 힙 주소를 참조
- unboxing 을 하면..
    ```cs
    int y = (int)obj;
    ```
    위 과정에서 벌어지는 일 (왕복 작업 발생)
    1. 힙 주소 따라감
    2. 타입 검사
    3. 값 복사
    4. 스택에 다시 저장
- 만약 참조 타입을 object로 변환한 경우
    ```cs
    class Person
    {
        public string Name;
    }

    Person person = new Person { Name = "Tom" };
    object obj = person;    // boxing ❌
    ```
    - 힙에 객체는 이미 존재하고 있다.
    - object 는 그 객체를 그대로 가리키게된다.
    - 새 객체 생성 ❌, 값 복사 ❌
    - 업 캐스팅일뿐 (비용 거의 없음)
- 즉, Boxing을 주의해야할 상황은 **"값 타입을 object 또는 interface 타입으로 다룰 때"**이다.
    ```cs
    strcut Point : IDisposable
    {
        public void Dispose() {}
    }
    Point p = new Point();
    IDisposable d = p;  // boxing 발생
    ```

## as 연산자
- 타입 변환을 위한 연산자
    ```cs
    object obj = "C# Programming";
    string? str = obj as string;
    if (str != null)
        Console.WriteLine($"as 연산자 변환 : {str}");
    else
        Console.WriteLine("null");
    // as 연산자 변환 : C# Programming
    ```
    ```cs
    object obj = 1;
    int? num = obj as int?;
    if (num != null)
        Console.WriteLine($"as 연산자 변환 : {num}");
    else
        Console.WriteLine("null");
    // as 연산자 변환 : 1
    ```
- 타입을 안전하게 변환하기위해 사용한다.
    ```cs
    object obj = "C# Programming";
    int num = (int)obj; // ❌ 명시적 변환 후 실행시 오류가 발생한다.
    ```
    
- 만약 올바르지 않은 타입으로 변환한다면 null을 반환한다.
    ```cs
    object obj = 1;
    string? str = obj as string;
    if (str != null)
        Console.WriteLine($"as 연산자 변환 : {str}");
    else
        Console.WriteLine("null");
    // null
    ```
    변환이 실패하면 예외를 발생시키는 대신 null을 반환한다. 이는 특히 참조 타입이나 nullable 값 타입을 다룰 때 유용하다.

## Convert Class 을 이요한 타입 변환
- Convert 클래스는 정적 클래스다.
    ```cs
    string strNumber = "123";
    int convertedInt = Convert.ToInt32(strNumber);
    Console.WriteLine($"String : {strNumber}, Converted int : {convertedInt}");
    // String : 123, Converted int : 123
    ```
    문자열과 숫자는 서로 타입자체가 다르기에 명시적 변환과 as 변환이 불가능하다.
    ```cs
    int num1 = (int)strNumber;      // ❌ 오류발생
    int? num2 = strNumber as int?;  // ❌ 오류발생
    ```
- 주로 base64String 변환에 사용된다.
    ```cs
    byte[] bytes = { 1, 2, 3 };
    string base64String = Convert.ToBase64String(bytes);

    Console.WriteLine(base64String);    // AQID
    /* 
     * “값이 1, 2, 3인 byte 데이터(이진 데이터)를
     * 텍스트 환경에서도 깨지지 않게 전달하려고
     * 문자열 ‘AQID’로 표현했다”
    */
    ```
- [MSDN_Convert 클래스](https://learn.microsoft.com/ko-kr/dotnet/api/system.convert?view=net-8.0) ← 링크 참고

### Note_정적 클래스
- "일반 클래스 + static 메서드" 조합을 사용해도 되는데 굳이 static 클래스를 사용하는 이유 -> "의도를 강제하고, 잘못된 사용을 원천 차단하기 위함"
    ```cs
    class MathUtil
    {
        public static int Add(int a, int b) => a + b;
    }
    ```
    위 코드는 기능적으로 아무런 문제가 없다. 
    ```cs
    var util = new MathUtil();  // 의미 없는 인스턴스 생성
    ```
    하지만 위와 같은 사용이 가능해지면서 설계 의도와 어긋난 사용이 가능하게된다.
- 정적 클래스의 핵심 목적 :<br>
    정적 클래스를 사용하면 컴파일러가 아래를 강제로 막아준다.
    - 인스턴스 생성 불가
        ```cs
        new MathUtil(); // ❌ 컴파일 에러
        ```
    - 상속 불가
        ```cs
        class MyUtil : MathUtil // ❌ 컴파일 에러
        ```
    - 인터페이스 구현 불가
        ```cs
        static class MathUtil : IUtil   // ❌ 컴파일 에러
        ```
-  정적 클래스가 주는 설계적 의미
    1. 상태가 없음을 보장
        - 필드가 있다면 그것도 static
        - 인스턴스 상태 개념이 아예 없음
        - 순수 함수 모음임을 보장 (static 필드를 가질 수 있지만 관례적으로 "순수 함수 모음"이라고 부른다.)
    2. SRP(단일 책임 원칙)에 잘 맞음
        - 객체가 아니라 기능 묶음이라는 것을 명확히 표현
    3. 유지보수 시 오용 방지

### Note_Base64
- Base64는 이진 데이터(byte 배열)를 텍스트 문자열로 안전하게 표현하는 인코딩 방식이다.
- 컴퓨터 내부 데이터는 대부분 byte(0~255)로 이루어져 있지만, 모든 환경이 이진 데이터를 그대로 다룰 수는 없다.
- 문자열 기반에 바이너리를 그대로 넣으면 깨지거나 잘린다. (문자열 기반 : JSON, XML, HTTP Header, SMTP, 로그 파일, etc...)
- Base64는 이진 데이터를 ASCII 문자 64개만 사용해서 문자열로 변환한다.
- 사용되는 문자 집합 (64개)
    ```
    A-Z (26)
    a-z (26)
    0-9 (10)
    + /
    ```
- 내부적으로 일어나는 일
    ```
    1. byte 3개(24비트)를 묶음
    00000001 00000010 00000011
    
    2. 6비트씩 나눔 -> 4개
    000000 010000 001000 000011
    
    3. 각 6비트를 Base64 문자 하나로 매핑
    A       Q       I       D
    ```
- 장점 :
    - 텍스트 환경에서 안전
    - 표준화됨 (RFC 4648)
    - 언어/플랫폼 공통
- 단점 : 
    - 크기 증가 (약 33% 커짐)
- Base64는 암호화가 아니다. "옮기기 쉽게 만드는 것"이다.
- 즉, Base64는 이진 데이터를 어떤 텍스트 환경에서도 깨지지 않게 전달하기 위한 문자열 인코딩 방식이다.

## is 연산자
- 변환 가능 여부를 boolean 값으로 반환해준다.
    ```cs
    object obj = "C# Programing";

    if(obj is string)
    {
        string str = (string)obj;
        Console.WriteLine($"is 연산자 변환 성공 : {str}");
    }
    // is 연산자 변환 성공 : C# Programing
    ```
- is 연산자의 기능을 활용하면 타입 변환 코드 한 줄을 줄일 수 있다.
    ```cs
    if(obj is string str)
    {
        Console.WriteLine($"is 연산자 변환 성공 : {str}");
    }
    // is 연산자 변환 성공 : C# Programing
    ```
    (단, str 은 if 코드블록 내에서만 유효하다.)

