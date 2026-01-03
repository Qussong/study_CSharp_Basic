# Section2

`타입(Type)`은 크게 **값 타입**과 **참조 타입**으로 나뉜다.</br>
그 중 값 타입은 데이터를 직접 저장하며 메모리의 스택(Stack) 영역에 저장한다. 값 타입의 변수를 다른 변수에 복사하면, 값 자체가 복사되기에 독립적인 데이터를 가지게된다.</br>
참조 타입은 데이터가 메모리의 힙(Heap) 영역에 저장되고, 변수는 그 데이터를 참조하는 "참조(포인터)"를 저장한다. 때문에 참조 타입을 다른 변수에 할당하면 값이 아닌 메모리 주소가 복사되며, 두 변수는 동일한 데이터를 참조하게 된다.

## 정수형 타입
- 소수가 없는 값
- byte, short, int, long
- sbyte, ushort, uint, ulong
    ```csharp
    byte age = 255; // byte 값의 범위 : 0 ~ 255
    // ㅁㅁㅁㅁㅁㅁㅁㅁ 2^8 = 256
    sbyte temperature = -128;   // sbyte 값의 범위 : -128 ~ 127
    // ■ㅁㅁㅁㅁㅁㅁㅁ 2^7 = ±128
    ```

## 소수형 타입
- 소수가 있는 수
- 부동 소수 : float, double
    ```csharp
    float f = 1.5f;
    // f 접미사를 붙이지 않으면 double 타입을 가리킨다.
    double d = 1.5;
    ```
- 고정 소수 : decimal
    ```csharp
    decimal d = 1.5m;
    ```
- 부동 소수는 연산시 작은 오차가 발생할 수 있다. 대신 연산속도가 매우 빠르다는 장점이 있다.
- float 보단 double 의 정밀도가 높다.
- 금융과 같이 정확한 연산이 필요한 경우 decimal 을 활용한다.

## bool, char 타입
- `bool`:</br>
논리형, true or false
    ```csharp
    bool isTrue = false;
    Console.WriteLine("isTrue : " + isTrue);    // isTrue : False
    ```
- `char`:</br>
character 의 준말로 문자 하나를 의미, 16비트 유니코드 문자, char 여러개가 모여서 문자열(string)을 이룬다.
    ```csharp
    char character = 'A';
    char[] chars = { 'i', 's', 'T', 'r', 'u', 'e' };
    string str = new string(chars);

    Console.WriteLine(str + " : " + isTrue);    // isTrue : False
    ```

## Enum 타입
- 열거형, 상수들의 집합
- 상수은 `const` 키워드로 나타내며, 값이 변하지 않는다는 특징이있다.
    ```csharp
    const int SUNDAY = 0;
    const int MONDAY = 1;
    const int TUESDAY = 2;
    const int WEDNESDAY = 3;
    const int THURSDAY = 4;
    const int FRIDAY = 5;
    const int SATURDAY = 6;

    SUNDAY = 1; // ❌ 오류 발생
    ```
- 상수를 활용하여 휴면 에러를 사전에 예방할 수 있다.
- enum 은 가독성을 위해 많이 사용된다.
    ```csharp
    enum Days {
        Sunday,     // =0 (자동으로 0 할당됨)
        Monday,     // =1
        Tuesday,    // =2
        Wednesday = 10,  // 임의로 값 할당 가능
        Thursday,   // =11
        Friday,     // =12
        Saturday    // =13
    };
    
    // 시작점
    class Program
    { 
        static void Main()
        {
            Days day = Days.Sunday;
            Console.WriteLine(day); // Sunday

            Console.ReadKey();
        }
    }
    ```

## 명시적, 암시적 선언
- 명시적 선언 (Explicit Declaration)이랑 타입을 명시적으로 알려주는 선언방식을 의미한다.
    ```csharp
    int number;
    ```
- 암시적 선언 (Implicit Declaration)이란 타입을 자동으로 추론할 수 있도록 작성하는것을 의미한다. `var` 키워드를 사용하며, 반드시 값을 선언과 함께 초기화해줘야한다.
    ```csharp
    var number = 10;
    ```
- 명시적 선언은 변수의 타입이 명확하게 드러나기에 코드의 가독성이 높아진다는 장점이 있다. 하지만, 모든 변수에 대해 타입을 명시해야 하므로 코드가 길어질 수 있다.
- 암시적 선언은 변수의 타입을 명시하지 않아도 되므로 코드가 간결해진다는 장점이 있다. 하지만, 타입을 명확히 알기 어려워 타입 관련 오류를 발견하기 힘들 수 있다.

# Section3

## 최상위문
