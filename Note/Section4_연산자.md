## 비교 연산자
- 두 값을 비교하고 결과로 true, false 를 반환한다.

    |연산자|설명|예시|
    |---|---|---|
    |`==`|값이 같은지 비교|a == b|
    |`!=`|값이 다른지 비교|a != b|
    |`>`|왼쪽 값이 큰지 비교|a > b|
    |`<`|오른쪽 값이 큰지 비교|a < b|
    |`>=`|왼쪽 값이 크거나 같은지 비교|a >= b|
    |`<=`|오른쪽 값이 크거나 같은지 비교|a <= b|
- 비교연산을 숫자에만 사용할 수 있는건 아니다. 문자나 다른 타입에도 활용할 수 있다.
    ```csharp
    string a = "안녕";
    string b = "안녕";
    Console.WriteLine(a == b);  // True
    ```

## 산술 연산자
- 숫자에 대한 기본적인 수학 연산을 수행한다.
    ```csharp
    int a = 10;
    int b = 20;
    Console.WriteLine("a + b = " + (a + b)); // a + b = 30
    Console.WriteLine("a - b = " + (a - b)); // a - b = -10
    Console.WriteLine("a * b = " + (a * b)); // a * b = 200
    Console.WriteLine("a / b = " + (a / b)); // a / b = 0
    Console.WriteLine("a & b = " + (a % b)); // a & b = 10
    ```
- 증감연산자
    ```csharp
    int a = 10;
    Console.WriteLine(a++); // 10
    Console.WriteLine(a);   // 11
    Console.WriteLine(++a); // 12

    int b = 10;
    Console.WriteLine(b--); // 10
    Console.WriteLine(b);   // 9
    Console.WriteLine(--b); // 8
    ```

## 할당 연산자
- 변수에 값을 할당하거나, 변수의 값을 변경한다.
    ```csharp
    int a = 10;
    a = a + 20;
    Console.WriteLine(a);   // 30
    a = a - 10;
    Console.WriteLine(a);   // 20
    a = a * 2;
    Console.WriteLine(a);   // 40
    a = a / 4;
    Console.WriteLine(a);   // 10
    a = a % 3;
    Console.WriteLine(a);   // 1

    // 줄여적은 형태
    int b = 10;
    b += 20;
    Console.WriteLine(b);   // 30
    b -= 10;
    Console.WriteLine(b);   // 20
    b *= 2;
    Console.WriteLine(b);   // 40
    b /= 4;
    Console.WriteLine(b);   // 10
    b %= 3;
    Console.WriteLine(b);   // 1
    ```
## 논리 연산자
- 논리값을 연산하여 true 또는 flase 를 반환하며, 주로 조건문에 사용된다.
    |연산자|설명|예시|
    |---|---|---|
    |`&&`|논리 AND (그리고)</br>둘 모두 참이면 true 반환|a && b|
    |`\|\|`|논리 OR (또는)</br>둘 중 하나라도 참이면 true 반환|a \|\| b|
    |`!`|논리 NOT (부정)</br>참이면 false 반환|!a|
    ```csharp
    Console.WriteLine($"true && true = {true && true}");    // True
    Console.WriteLine($"false && true = {false && true}");  // False
    Console.WriteLine($"false && false = {false && false}");// False

    Console.WriteLine($"true || true = {true || true}");    // True
    Console.WriteLine($"false || true = {false || true}");  // True
    Console.WriteLine($"false || false = {false || false}");// False

    Console.WriteLine($"!false = {!false}");// True
    Console.WriteLine($"!true = {!true}");  // False
    ```

## 비트 연산자
- 정수형 값에 대해 비트 단위의 연산을 수행한다.
    |연산자|설명|예시|
    |---|---|---|
    |`&`|비트 AND</br>두 값이 서로 같으면 1 다르면 0|a & b|
    |`\|`|비트 OR</br>두 갑중 하나라도 1 이면 1|a \| b|
    |`^`|비트 XOR</br>두 값이 서로 다르면 1 같으면 0|a ^ b|
    |`~`|비트 NOT|~a|
    |`<<`|비트 왼쪽 시프트|a << 2|
    |`>>`|비트 오른쪽 시프트|a >> 2|
    ```csharp
    int a = 192;    //  11000000
    int b = 168;    //  10101000

    Console.WriteLine($"a & b = {a & b}");  // 128 (10000000)
    Console.WriteLine($"a | b = {a | b}");  // 232 (11101000)
    Console.WriteLine($"a ^ b = {a ^ b}");  // 104 (01101000)

    // a = 00000000 00000000 00000000 11000000
    // ~a = 11111111 11111111 11111111 00111111
    // ~공식 : ~x = -(x+1)
    Console.WriteLine($"~a = {~a}");    // -193

    // a = 00000000 00000000 00000000 11000000
    // a << 2 = 00000000 00000000 00000011 00000000
    // a >> 2 = 00000000 00000000 000000 00110000
    Console.WriteLine($"a << 2 = {a << 2}");    // 768
    Console.WriteLine($"a >> 2 = {a >> 2}");    // 48
    ```

## Null 병합 연산자
- Null 가능 변수를 다룰 때 사용한다.
    |연산자|설명|예시|
    |---|---|---|
    |`??`|왼쪽 값이 null일 경우 오른쪽 값을반환|a = b ?? defaultVal|
    |`??=`|왼쪽 값이 null일 경우 오른쪽 값을 할당|a ??= b|

- 값 타입은 기본적으로 null 을 허용하지 않는다. `Nullable<T>`를 사용하면 허용된다. (syntax sugar = `T?`)
    ```csharp
    int a = null;           // ❌ 오류발생
    Nullable<int> a = null; // OK
    int? a = null;          // OK (일반적으로 사용되는 방법)
    ```
- Nullable<T> 구조체 사용시 `HasValue`를 통해 값 할당여부를 확인할 수 있다.
    ```csharp
    public struct Nullable<T> where T : struct
    {
        private bool hasValue;
        private T value;
    }
    ```
    ```csharp
    int? a = null;
    int? b = 1;
    Console.WriteLine($"a = {a}");  // a =
    Console.WriteLine($"a 값이 있는가? = {a.HasValue}"); // False
    Console.WriteLine($"b 값이 있는가? = {b.HasValue}"); // True
    ```
- `??` 예제
    ```csharp
    int? b = null;
    int a = b ?? 3;
    Console.WriteLine(a);   // 3

    b = 5;
    a = b ?? 3;
    Console.WriteLine(a);   // 5
    ```
- `??=` 예제
    ```csharp
    int? a = null;
    int? b = 5;
    Console.WriteLine($"a = {a}");  // a =

    a ??= b;
    Console.WriteLine($"a = {a}");  // a = 5
    ```

