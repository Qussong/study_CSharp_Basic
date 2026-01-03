## 배열 선언 및 초기화
- 배열은 동일한 데이터 타입의 여러 값을 하나의 변수로 관리할 수 있는 자료구조
- 선언
    ```csharp
    // 선언
    int[] numbers;      // 정수형 배열 선언
    string[] fruits;    // 문자열 배열 선언
    ```
- 초기화시 자동으로 기본값이 들어간다.
    ```csharp
    int[] numbers = new int[5];         // [0,0,0,0,0]
    string[] fruits = new string[5];    // [null,null,null,null,null]
    ```
- 배열의 요소에는 인덱스를 통해 접근하며 인덱스는 0부터 시작한다.
- 배열 초기화시 들어갈 값을 지정하는 방법도 존재한다.
    ```csharp
    // case 1
    int[] numbsrs = new int[] { 1, 2, 3, 4, 5 };    // [1,2,3,4,5]
    // case 2
    int[] numbsrs = { 1, 2, 3, 4, 5 };    
    // 선언시 타입이 int 로 정의되었기에 "new int[]" 를 생략해도 된다.
    ```
- 컬렉션 표현식 (C# 9.0 이상)
    ```csharp
    string[] fruits = ["사과", "바나나", "수박"];
    ```

## 배열 요소 접근
- 요소 접근 예시
    ```csharp
    int[] numbers = new int[] { 1, 2, 3, 4, 5 }; 
    Console.WriteLine(numbers[2]);  // 3
    numbers[2] = 30;
    Console.WriteLine(numbers[2]);  // 30
    ```

- `^ (hat)`연산자 (C# 8.0 이상)</br>
    배열의 마지막 요소에 쉽게 접근하기위해 만들어진 기능
    ```csharp
    string[] fruits = ["사과", "바나나", "수박"];
    Console.WriteLine(fruits[^1]);  // 수박
    Console.WriteLine(fruits[^2]);  // 바나나
    ```

## 반복문
- `for`</br>
    고정된 횟수만큼 반복할 때 사용
    ```
    for (초기화; 조건; 증감식)
    {
        // 반복 실행할 코드
    }
    ```
    ```csharp
    for (int i = 0; i < 5; ++i)
    {
        Console.Write(i + " ");
    }
    // 0 1 2 3 4
    ```
- `foreach`</br>
    컬렉션(배열, 리스트 등)의 모든 요소를 순회할 때 사용한다. 인덱스를 직접 관리할 필요 없이 각 요소에 직접 접근할 수 있어 코드가 간결해진다.
    ```
    foreach (타입 변수명 in 컬렉션)
    {
        // 반복 실행할 코드
    }
    ```
    ```csharp
    string[] fruits = { "사과", "바나나", "수박" };
    foreach (string fruit in fruits)
    {
        Console.Write(fruit + " ");
    }
    // 사과 바나나 수박
    ```
- `while`</br>
    조건이 참인 동안 계속 반복할 때 사용한다.
    ```cs
    int count = 0;
    while(count < 3)
    {
        Console.WriteLine($"카운트 : {count}");
        ++count;
    }
    // 카운트 : 0
    // 카운트 : 1
    // 카운트 : 2
    ```
- `do-whie`</br>
    while 문과 유사하지만, 조건을 나중에 검사하여 최소 한 번은 반복 블록을 실행한다.
    ```
    do
    {
        // 반복 실행할 코드
    } while (조건);
    ```
    ```cs
    int count = 0;
    do
    {
        Console.WriteLine($"카운트 : {count}");
        ++count;
    } while (count < 3);
    // 카운트 : 0
    // 카운트 : 1
    // 카운트 : 2
    ```
- 반복 제어문 (break, continue)</br>
    - `break`</br>반복문을 완전히 종료하고 반복문 다음의 코드로 이동한다.
        ```cs
        int count = 0;
        do
        {
            if (count == 1) break;
            Console.WriteLine($"카운트 : {count}");
            ++count;
        } while (count < 3);
        // 카운트 : 0
        ```
    - `continue`</br>현재 반복문을 건너뛰고 다음 반복문으로 넘어간다.
        ```cs
        int count = 0;
        do
        {
            if (count == 1)
            {
                ++count;
                continue;
            }
            Console.WriteLine($"카운트 : {count}");
            ++count;
        } while (count < 3);
        // 카운트 : 0
        // 카운트 : 2
        ```
- 중첩 반복문</br>
    반복문 안에 또 다른 반복문을 포함하여 다차원 구조의 데이터를 처리할 때 사용된다.
    ```cs
    // 구구단
    for (int i = 2; i < 10; ++i)
    {
        for (int k = 1; k < 10; k++)
        {
            Console.WriteLine($"{i} x {k} = {i * k}");
        }
        Console.WriteLine();    // 단마다 줄바꿈
    }
    ```

## 다차원 배열
- 2차원 배열 선언과 초기화
    ```cs
    // 2차원 배열 선언
    int[,] matrix = new int[2, 3];   // 2행 3열 배열

    // 2차원 배열 선언과 동시에 초기화
    int[,] matrix = new int[,] { { 1, 2, 3 }, { 4, 5, 6 } };

    // 더 간단한 형태로 초기화
    int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };

    Console.WriteLine(matrix[1,1]); // 5
    Console.WriteLine(matrix[1,2]); // 6
    matrix[1,1] = 100;
    Console.WriteLine(matrix[1,1]); // 100
    ```
- 차원의 개수 접근(`GetLength(int)`)
    ```cs
    int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };

    Console.WriteLine(matrix.GetLength(0)); // 2
    Console.WriteLine(matrix.GetLength(1)); // 3
    ```
- 다차원 배열 반복
    ```cs
    int[,] matrix = { { 1, 2, 3 }, { 4, 5, 6 } };

    for(int row = 0;  row < matrix.GetLength(0); row++)
    {
        for(int col = 0; col < matrix.GetLength(1); col++)
        {
            Console.Write(matrix[row, col] + " ");
        }
        Console.WriteLine();
    }

    /* 1 2 3
    * 4 5 6 */
    ```
- 가변 배열</br>
    가변 배열은 배열의 배열을 의미하며, 각 행의 길이가 다를 수 있다.
    ```cs
    // 가변 배열 선언 (각 배열의 크기가 다를 수 있음)
    int[][] jaggedArray = new int[3][];

    // 각 행에 다른 크기의 배열을 할당
    jaggedArray[0] = new int[] { 1, 2 };
    jaggedArray[1] = new int[] { 3, 4, 5 };
    jaggedArray[2] = new int[] { 6, 7, 8, 9 };
    ```
- 가변 배열 반복</br>
    ```cs
    for(int row = 0; row < jaggedArray.GetLength(0); row++)
    {
        for(int col = 0; col < jaggedArray[row].Length; ++col)
        {
            Console.Write(jaggedArray[row][col] + " ");
        }
        Console.WriteLine();
    }

    /* 1 2
    * 3 4 5
    * 6 7 8 9 */
    ```

## Array 클래스
- `Array.Sort()`</br>
    배열을 오름차순으로 정렬한다.
    ```cs
    int[] numbers = { 2, 1, 5, 3, 4 };
    foreach (var number in numbers)
        Console.Write(number + " ");
    // 2 1 5 3 4

    Array.Sort(numbers);    // 배열 오름차순 정렬
    foreach (var number in numbers)
        Console.Write(number + " ");
    // 1 2 3 4 5
    ```
- `Array.Reverse()`</br>
    배열의 순서를 반대로 뒤집는다.
    ```cs
    int[] numbers = { 2, 1, 5, 3, 4 };
    foreach (var number in numbers)
        Console.Write(number + " ");
    // 2 1 5 3 4

    Array.Reverse(numbers);    // 배열 뒤집음
    foreach (var number in numbers)
        Console.Write(number + " ");
    // 4 3 5 1 2
    ```
- `Array.IndexOf()`</br>
    배열에서 특정 값의 최초위치의 인덱스를 찾는다. 
    ```cs
    int[] numbers = { 2, 1, 5, 3, 1, 4 };
    int index = Array.IndexOf(numbers, 1);  // 1의 인덱스를 찾음
    Console.WriteLine(index);   // 1
    ```
    만약 존재하지 않는 값의 위치를 찾으려고 한다면 -1 을 반환한다.
    ```cs
    int[] numbers = { 2, 1, 5, 3, 1, 4 };
    int index = Array.IndexOf(numbers, 111);
    Console.WriteLine(index);   // -1
    ```
- `Array.Resize()`</br>
    배열의 크기를 조정한다. 늘어난 공간에는 기본값이 들어간다.
    ```cs
    int[] numbers = { 1, 2, 3 };
    Console.WriteLine(numbers.Length);  // 3
    Array.Resize(ref numbers, 5);   // 배열의 크기를 5로 변경
    Console.WriteLine(numbers.Length);  // 5
    
    foreach (var number in numbers)
        Console.Write(number + " ");
    // 1 2 3 0 0
    ```