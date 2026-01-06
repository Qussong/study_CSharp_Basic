## 열거자 (Enumerator)
- 열거자는 컬렉션을 순회(iterate)할 수 있게 해주는 객체이다.
- 주로 IEnumerable 과 IEnumerator 인터페이스를 통해 구현된다.
- IEnumerator 의 구현부
    ```cs
    public interface IEnumerator
    {
        object Current { get; }
        bool MoveNext();
        void Reset();   // 열거에서 불안정성을 초래하기에 사용하지 않는다.
    }
    ```
- Enumerator 활용 예제
    ```cs
    IEnumerator<int> GetEnumerator()
    {
        yield return 1;
        yield return 10;
        yield return 20;
        yield return 100;
        yield return -50;
    }

    // GetEnumerator() 메서드는 호출되었지만, 내부 코드가 실행되지 않는다.
    // -> 이유 : yield 키워드와 지연 실행(Deferred Execution) 특성 
    var enumerator = GetEnumerator();

    // enumerator.MoveNext() : 다음 값 존재여부 확인
    while (enumerator.MoveNext())
    {
        Console.WriteLine(enumerator.Current);
    }

    /*
    * 1
    * 10
    * 20
    * 100
    * - 50
    */
    ```
- 셀프 커스텀 
    ```cs
    IEnumerator<Action> GetMessage()
    {
        yield return () => Console.WriteLine("Hello");
        yield return () => Console.WriteLine("World");
    }

    var enumerator = GetMessage();
    while(enumerator.MoveNext())
    {
        enumerator.Current();
    }

    /*
    * Hello
    * World
    */
    ```

## 열거형 (Enumerable)
- IEnumerator 예제
    ```cs
    Collection collection = new Collection();

    foreach(var value in collection) // <- 컬렉션은 클래스인데...?
    {
        Console.WriteLine(value);
    }
    /*
    * 1
    * 10
    * 20
    * 100
    * -50
    */

    class Collection()
    {
        // 
        public IEnumerator<int> GetEnumerator()
        {
            yield return 1;
            yield return 10;
            yield return 20;
            yield return 100;
            yield return -50;
        }

    }
    ```
    - 위의 코드를 보면 collection 객체는 클래스 타입의 객체인데 foreach 로 순회가 됨을 확인할 수 있다.
    - GetEnumerator() 메서드는 클래스내에서 열거형을 사용하기위한 약속된 메서드임을 알 수 있다. (철자 하나라도 틀리면 열거형으로 사용할 수 없다.)
    - `IEnumerable` 인터페이스의 정의를 확인해보면 GetEnumerator() 메서드가 선언되어있음을 확인할 수 있다.
        ```cs
        public interface IEnumerable<out T> : IEnumerable
        {
            IEnumerator<T> GetEnumerator();
        }
        ```
    - .Net 에서 모든 배열과 컬렉션들은 IEnumerable을 구현하고 있다.
- IEnumerable 예제
    ```cs
    IEnumerable<int> GetEnumerable()
    {
        yield return 1;
        yield return 10;
        yield return 20;
        yield return 100;
        yield return -50;
    }

    foreach (int value in GetEnumerable())
    {
        Console.WriteLine(value);
    }

    var enumerator = GetEnumerable().GetEnumerator();
    bool isExists = enumerator.MoveNext();
    int value2 = enumerator.Current;
    ```
    - foreach는 반복기(Enumerator)가 필요하지만, 제공한 건 열거형(Enumerable) 인터페이스다.
    - IEnumerable<T> 인터페이스의 정의를 보면, 해당 인터페이스는 딱 하나의 메서드만 가지고 있다. 즉, IEnumerable은 IEnumerator를 만들어내는 공장이다.
    - foreach 문은 IEnumerable을 받으면, 그 안에 있는 `.GetEnumerator()`를 자동으로 호출해서 IEnumerator를 꺼내 쓴다.
    - 즉, foreach는 IEnumerator가 필요하지만, IEnumerable을 주면 
    "안에 GetEnumerator()가 있겠구나" 하고 알아서 꺼내 쓰기에 동작한다.

### Note_덕 타이핑(Duck Typing)
- 위 예제의 경우 Collection 클래스가 IEnumerable 인터페이스를 상속하고 있지 않음에도 순회가 동작하는 이유는 foreach문의 경우 인터페이스(IEnumerable) 구현 여부를 확인하기 전에, "특정 조건을 만족하는 메서드가 있는가?"를 먼저 확인하기 때문이다. -by Gemini
- 이러한 접근법을 프로그래밍 용어로 덕 타이핑 또는 패턴 기반 접근이라고 한다.

## 컬렉션 - List 정의, 생성, 요소 접근
- 배열의 크기가 가변적으로 늘어나거나 줄어들때 주로 사용되는 컬렉션으로 "동적 배열" 이라 부른다.
- 네임스페이스 : `System.Collections.Generic`
- 생성법
    ```cs
    List<Type> 변수명 = new List<Type>();
    ```
- List 의 구현부를 확인해보면 IList<T> 안에 IEnumerable<T> 도 구현하고 있음을 알 수 있다. 즉, 요소를 순회할 수 있음을 알 수 있다.
    ```cs
    public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable
    ```
    ```cs
    List<string> stringList = new List<string>();
    stringList.Add("a");
    stringList.Add("b");
    stringList.Add("c");
    foreach (string str in stringList)
        Console.Write(str + " ");
    // a b c
    ```

## 컬렉션 - List 초기화
- 컬렉션 단순화 : new 키워드에 커서를 올리고 ctrl + .(dot) 을 통해 컬렉션 단순화 기능을 사용할 수 있다.
    ```cs
    // before
    List<string> stringList = new List<string>();
    stringList.Add("a");
    stringList.Add("b");
    stringList.Add("c");

    // after
    List<string> stringList = ["a", "b", "c"];  // 컬렉션 표현식 (C# 12)
    ```

## 컬렉션 - List 요소 삽입
- `AddRange()` : 뒤에 다른 리스트의 원소들을 추가한다.
    ```cs
    public void AddRange(IEnumerable<T> collection);
    ```
    ```cs
    List<string> stringList = ["a", "b", "c"];
    List<string> anotherStringList = ["가", "나", "다"];
    stringList.AddRange(anotherStringList);

    foreach (string str in stringList)
        Console.Write(str + " ");
    // a b c 가 나 다
    ```
- `Insert()` : 
    ```cs
    public void Insert(int index, T item);
    // index : 어느 위치에 넣을것인가? 
    // item : 들어갈 값
    ```
    ```cs
    List<string> stringList = ["a", "b", "c"]; 
    stringList.Insert(1, "가");

    foreach (string str in stringList)
        Console.Write(str + " ");
    // a 가 b c
    ```
- `InsertRange()` : 특정 위치에 다른 리스트의 원소들을 추가한다.
    ```cs
    public void InsertRange(int index, IEnumerable<T> collection);
    ```
    ```cs
    List<string> stringList = ["a", "b", "c"];
    List<string> anotherStringList = ["가", "나", "다"];
    stringList.InsertRange(2, anotherStringList);

    foreach (string str in stringList)
        Console.Write(str + " ");
    // a b 가 나 다 c
    ```

## 컬렉션 - List 요소 삭제
- `Remove()` : 특정 값을 찾아 지울 수 있다.
    ```cs
    public bool Remove(T item);
    ```
    ```cs
    List<string> stringList = ["a", "b", "c", "a"];
    stringList.Remove("a");

    foreach (string str in stringList)
        Console.Write(str + " ");
    // b c a
    ```
- `RemoveAt()` : 인덱스를 찾아 지울 수 있다.
    ```cs
    public void RemoveAt(int index);
    ```
    ```cs
    List<string> stringList = ["a", "b", "c", "a"];
    stringList.RemoveAt(2);

    foreach (string str in stringList)
        Console.Write(str + " ");
    // a b a
    ```
- `RemoveAll()` : 
    ```cs
    public int RemoveAll(Predicate<T> match);
    ```
    ```cs
    List<string> stringList = ["a", "b", "c", "a"];
    stringList.RemoveAll((str) =>
    {
        return str == "a" || str == "c";
    });

    foreach (string str in stringList)
        Console.Write(str + " ");
    // b
    ```
- `Clear()` : List 내 모든 요소를 제거한다.
    ```cs
    public void Clear();
    ```

## 컬렉션 - List 요소 검색
- `Contains()` : List 내에서 특정 값이 있는지 확인한다. 기본적으로 대소문자 구분을하며, `StringComparer.OrdinalIgnoreCase` 추가시 대소문자를 무시한다.
    ```cs
    public bool Contains(T item);
    ```
    ```cs
    List<string> fruitList = ["apple", "banana", "lemon", "lime"];
    Console.WriteLine(fruitList.Contains("lemon")); // True
    Console.WriteLine(fruitList.Contains("Lemon")); // False
    Console.WriteLine(fruitList.Contains("lemon", StringComparer.OrdinalIgnoreCase));   // True
    ```
- `IndexOf` : 특정 값의 인덱스를 반환한다.
    ```cs
    public int IndexOf(T item);
    ```
    ```cs
    List<string> fruitList = ["apple", "banana", "lemon", "lime"];
    Console.WriteLine(fruitList.IndexOf("lemon")); // 2
    Console.WriteLine(fruitList.IndexOf("Lemon")); // -1
    ```
- `Find()` : 조건에 맞는 첫 번째 요소를 반환한다.
    ```cs
    public T? Find(Predicate<T> match);
    ```
    ```cs
    List<string> fruitList = ["apple", "banana", "lemon", "lime"];
    Console.WriteLine(
        fruitList.Find(fruit => {
            return fruit.StartsWith("l");
        })
    );  // lemon
    ```
- `FindAll()` : 조건에 맞는 모든 요소를 반환한다.
    ```cs
    public List<T> FindAll(Predicate<T> match);
    ```
    ```cs
    List<string> fruitList = ["apple", "banana", "lemon", "lime"];
    List<string> selectedFruits = fruitList.FindAll(fruit =>
    {
        return fruit.StartsWith("l");
    });
    foreach (string fruit in selectedFruits)
        Console.Write(fruit + " ");
    // lemon lime
    ```

## 컬렉션 - List 역순 및 정렬
- `Reverse()` : 요소의 순서를 뒤집음
    ```cs
    public void Reverse();
    ```
    ```cs
    List<int> intList = [3, 100, 5, -1, 20];
    intList.Reverse();

    foreach (int i in intList)
        Console.Write(i + " ");
    // 20 -1 5 100 3
    ```
- `Sort()` : 기본 정렬 (정순정렬, 오름차순) 또는 커스텀 정렬
    - case 1
        ```cs
        public void Sort();
        ```
        ```cs
        List<int> intList = [3, 100, 5, -1, 20];
        intList.Sort();

        foreach (int i in intList)
            Console.Write(i + " ");
        // -1 3 5 20 100
        ```
    - case 2
        ```cs
        public void Sort(Comparison<T> comparison);
        ```
        ```cs
        List<int> intList = [3, 100, 5, -1, 20];
        /*intList.Sort((a, b) => {
            if (a < b) return -1;
            else if (a == b) return 0;
            else return 1;
        });*/
        intList.Sort((a, b) => a.CompareTo(b));

        foreach (int i in intList)
            Console.Write(i + " ");
        // -1 3 5 20 100
        ```

### Note_int32.CompareTo()


## 컬렉션 - List 기타 메서드
- `Count()` : 요소의 수
    ```cs
    public int Count { get; }
    ```
    ```cs
    List<int> intList = [3, 100, 5, -1, 20];
    Console.WriteLine(intList.Count);   // 5
    ```
- `ToArray()` : List<T>를 배열로 변환
    ```cs
    public T[] ToArray();
    ```
    ```cs
    List<int> intList = [3, 100, 5, -1, 20];
    int[] ints = intList.ToArray();
    ```

## 컬렉션 - Dictionary
- Key 와 Value 를 쌍으로 데이터를 저장하는 제네릭 컬렉션
    ```cs
    var fruitPrices = new Dictionary<string, int>();
    fruitPrices.Add("apple", 500);
    fruitPrices.Add("banana", 400);
    fruitPrices.Add("cherry", 800);
    ```
- 아래와 같이 컬레션 초기화를 단순화 할 수 있다.
    ```cs
    var fruitPrices = new Dictionary<string, int>
    {
        { "apple", 500 },
        { "banana", 400 },
        { "cherry", 800 }
    };
    ```
- 이미 존재하는 키를 추가하려고하면 에러가 발생한다.
    ```cs
    fruitPrices.Add("apple", 300);  // ❌ 오류발생 - Value 가 다르더라도 이미 존재하는 Key 가 있기에 추가할 수 없다.
    ```
    아래의 방법으로 값을 추가할 수 있는데 이미 Dictionary 에 해당 값이 존재한다면 값을 덮어쓰고 없다면 값을 추가한다.
    ```cs
    fruitPrices["apple"] = 300; // ⭕
    ```
- 데이터 접근 :
    ```cs
    fruitPrices["apple"] = 300;
    Console.WriteLine(fruitPrices["apple"]);    // 300
    ```
    데이터 접근을 통해 값이 덮어씌워진것을 확인할 수 있다. 만약 존재하지 않는 Key 값으로 접근하려고 한다면 오류가 발생한다.
    ```cs
    int price = fruitPrices["grape"];   // ❌ 오류발생
    ```
- `TryGetValue()` : 데이터에 안전하게 접근하는 방법이다.
    ```cs
    bool hasKey = fruitPrices.TryGetValue("grape", out int grapePrice);
    if (hasKey)
        Console.WriteLine(grapePrice);
    else
        Console.WriteLine("과일이 등록되어있지 않습니다.");
    // 과일이 등록되어있지 않습니다.
    ```
- 순회를 할 수 있다.
    ```cs
    foreach (var key in fruitPrices.Keys)
        Console.Write(key + " ");
    // apple banana cherry

    foreach (var value in fruitPrices.Values)
        Console.Write(value + " ");
    // 500 400 800
    ```
- `Remove()` : 특정 요소를 제거할 수 있다.
    ```cs
    fruitPrices.Remove("apple");

    foreach (var key in fruitPrices.Keys)
        Console.Write(key + " ");
    // banana cherry
    ```
- `Clear()` : 모든 요소를 제거한다.

## 컬렉션 - Queue
- FIFO (선입선출)
- 생성 및 값 추가(`Enqueue()`) 방법
    ```cs
    var fruits = new Queue<string>();
    fruits.Enqueue("사과");
    fruits.Enqueue("바나나");
    fruits.Enqueue("체리");
    ```
- `Dequeue()` / `Peek()` : 값 꺼내기/값 확인, 만약 값이 없는데 Dequeue를 하게되면 오류가 발생한다.
    ```cs
    Console.Write(fruits.Dequeue() + " ");
    Console.Write(fruits.Dequeue() + " ");
    Console.Write(fruits.Peek() + " ");
    Console.Write(fruits.Dequeue());
    // 사과 바나나 체리 체리
    ```
- `TryDequeue()` : 안전하게 값을 꺼내는 방법
    ```cs
    //Console.Write(fruits.Dequeue());    //  ❌ 오류발생
    fruits.TryDequeue(out string? newFruit);
    Console.Write(newFruit);
    ```

## 컬렉션 - Stack
- LIFO (후입선출)
- 생성 및 값 추가(`Push()`) 방법
    ```cs
    var fruits = new Stack<string>();
    fruits.Push("사과");
    fruits.Push("바나나");
    fruits.Push("체리");
    ```
- `Pop()` / `Peek()` : 값 꺼내기 / 값 확인, 만약 값이 없는데 Pop을 하려고하면 오류가 발생한다.
    ```cs
    Console.Write(fruits.Pop() + " ");
    Console.Write(fruits.Pop() + " ");
    Console.Write(fruits.Peek() + " ");
    Console.Write(fruits.Pop());
    // 체리 바나나 사과 사과
    ```
- `TryPop()` : 안전하게 값을 꺼내는 방법
    ```cs
    //Console.Write(fruits.Pop());    //  ❌ 오류발생
    fruits.TryPop(out string? newFruit);
    Console.Write(newFruit);
    ```