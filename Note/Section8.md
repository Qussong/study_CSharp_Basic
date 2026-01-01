# Section8

## 문자열
- 텍스트 데이터를 표현하는 데 사용되는 기본 데이터 타입
- 선언과 초기화 :</br>
    ```cs
    string greeting = "Hello, World!";
    string emptyString = "";    // 빈 문자열
    string nullString = null;   // null 문자열 -> string 이 참존타입임을 알 수 있다.
    ```
- 문자열의 불변성 :</br>
    C#에서 문자열은 불변이기에, 문자열을 수정하는 것처럼 보여도 실제로는 새로운 문자열이 생성된다.
    ```cs
    string original = "Hello";
    original = original + ", World";    // 새로운 문자열이 생성됨
    ```
    문자열을 이어 붙이거나 수정할 때마다 새 문자열 객체가 생성되므로, 반복적인 문자열 변경이 많을 경우 메모리 사용량과 GC(가비지 컬렉션) 부담이 증가할 수 있다. 하지만, 불변성의 특징 덕분에 참조타입임에도 불구하고 값 타입처럼 안전하게 다룰 수 있다. 비교 연산 시 예측 가능한 결과를 얻을 수 있음
    ```cs
    string a = "Hello";
    string b = "Hello";
    Console.WriteLine(a == b);   // True
    ```
- 문자열 연결 :</br>
    문자열을 연결하려면 `+` 연산자 또는 `string.Concat()` 메서드를 사용할 수 있다.
    ```cs
    string firstName = "Jane";
    string lastName = "Doe";
    string fullName = firstName + " " + lastName;
    Console.WriteLine(fullName);    // Jane Doe
    ```
- 문자열 보간 :</br>
    C#에서 문자열 보간을 사용하여 문자열 내에 변수 값을 포함할 수 있다. 보간된 문자열은 `$`기호로 시작한다.
    ```cs
    string firstName = "Jane";
    string lastName = "Doe";
    string fullName = $"{firstName} {lastName}";
    Console.WriteLine(fullName);    // Jane Doe
    ```
    가독성면에서 뛰어나기에 연결법 보단 보간법을 주로 사용한다.
- 문자열 형식화 :</br>
    `string.Format()` 메서드나 `ToStrin()` 메서드를 사용하여 형식화된 문자열을 생성할 수 있다.
    ```cs
    string name = "Jane";
    int age = 20;
    string formatted = string.Format("Name: {0}, Age: {1}", name, age);
    Console.WriteLine(formatted);    // Name: Jane, Age: 20
    ```
- 문자열 속성 메서드 : </br>
    - `Length` : 문자열 길이 확인
        ```cs
        string greeting = "Hello";
        int length = greeting.Length;
        Console.WriteLine(length);    // 5
        ```
    - `Substring()` : 문자열의 일부를 추출
        ```cs
        string greeting = "Hello, World";
        string part = greeting.Substring(0, 5);
        Console.WriteLine(part);    // Hello
        ```
    - `Contains()` : 문자열 포함여부 확인, 대소문자를 구분한다.
        ```cs
        string greeting = "Hello, World";
        bool isContain1 = greeting.Contains("Hello");
        Console.WriteLine(isContain1);    // True
        bool isContain2 = greeting.Contains("hello");
        Console.WriteLine(isContain2);    // False
        ```
        "StringComparison.OrdinalIgnoreCase" 속성을 추가해주면 대소문자에 영향을 받지 않는다.
        ```cs
        bool isContain2 = greeting.Contains("hello", StringComparison.OrdinalIgnoreCase);
        Console.WriteLine(isContain2);    // True
        ```
    - `Equal` : 문자열 비교, "StringComparison.OrdinalIgnoreCase" 속성 추가시 대소문자 비교 안함
        ```cs
        string greeting = "Hello, World";
        bool isContain1 = greeting.Equals("Hello");
        Console.WriteLine(isContain1);    // True
        bool isContain2 = greeting.Equals("Hello");
        Console.WriteLine(isContain2);    // False
        bool isContain3 = greeting.Equals("hello", StringComparison.OrdinalIgnoreCase);
        Console.WriteLine(isContain3);    // True
        ```
    - `ToUpper()`, `ToLower()` : 대소문자 변환
        ```CS
        string greeting = "Hello, World";
        string upper = greeting.ToUpper();
        Console.WriteLine(upper);    // HELLO, WORLD
        string lower = greeting.ToLower();
        Console.WriteLine(lower);    // hello, world
        ```
    - `Trim()` : 문자열의 공백을 제거할 수 있다.
        ```cs
        string withSpace = "    Hello   ";
        string trimmed = withSpace.Trim();              // "Hello"
        string trimmedStart = withSpace.TrimStart();    // "Hello   "
        string trimmedEnd = withSpace.TrimEnd();        // "    Hello"

        string withChars = "****Hello****";
        string trimmedChars = withChars.Trim('*');          // Hello
        string trimmedStartChars = withChars.TrimStart('*');// Hello****
        string trimmedEndChars = withChars.TrimEnd('*');    // ****Hello

        string mixedChars = "-*_Hello_*--";
        string trimmedMixed = mixedChars.Trim('-', '*', '_');           // Hello
        string trimmedStartMixed = mixedChars.TrimStart('-', '*', '_'); // Hello_*--
        string trimmedEndMixed = mixedChars.TrimEnd('-', '*', '_');     // -*_Hello
        ```
