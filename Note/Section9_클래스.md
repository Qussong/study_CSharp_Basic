# Section 9
클래스는 **"객체 지향 프로그램(Object-Oriented Programming, OOP)"** 의 핵심개념이다.

## 클래스 - 정의, 객체 생성
- 클래스는 객체를 생성하기 위한 청사진(blueprint) 또는 템플릿이다.
    ```cs
    // Car 클래스 생성
    Car car = new Car();

    /// <summary>
    /// 클래스 정의
    /// </summary>
    class Car
    { 

    }
    ```

## 클래스 - 필드, 생성자
- 클래스는 객체의 속성(데이터)과 동작(메서드)을 정의한다.
- 클래스의 구성요소
    - `필드(field)` : 클래스의 데이터 또는 상태를 나타내는 변수
    - `프로퍼티(property)` : 필드에 대한 접근을 제어하는 멤버, 데이터의 캡슐화를 지원한다.
    - `생성자(Constructor)` : 객체가 생성될 떄 호출되는 특별한 메서드로 초기화 작업을 수행한다."ctor" + Tab key 입력을 통해 손쉽게 생성할 수 있다.
        ```cs
        class Car
        {
            // 필드
            string brand;
            string model;
            string color;

            // 생성자 
            public Car()
            {
                brand = "현대";
                model = "소나타";
                color = "검정";
            }
        }
        ```
    - `메서드(method)` 
    - `소멸자(Destructor)`
    - `이벤트(Event)`
    - `인덱서(Indexer)`

## 클래스 - 접근 제어자
- 클래스 멤버의 접근 범위를 제어하는 키워드
- 접근 제어자 종류
    - `public` : 모든 곳에서 접근 가능
    - `private` : 동일 클래스 내에서만 접근 가능, 클래스 내에서 접근제어자를 지정하지 않으면 기본값으로 private 이 적용된다.
    - `protected` : 동일 클래스 및 파생 클래스에서 접근 가능
    - `internal` : 같은 어셈블리 내에서만 접근 가능
    - `protected internal` : 같은 어셈블리 내 또는 파생 클래스에서 접근 가능

## 클래스 - 메서드
- 특정 작업을 수행하는 코드의 집합으로 클래스나 구조체 내에 정의한다.
- 메서드를 사용하면 코드를 재사용할 수 있고, 프로그램의 논리 구조를 명확하게 할 수 있다.
- 메소드의 구성 요소
    - `반환타입 (Return Type)` : 메서드가 반환하는 값의 타입을 지정한다. 반환할 값이 없을 경우 void를 사용한다.
    - `메서드 이름 (Method Name)` : 메서드의 이름은 식별자로 메서드가 수행할 작업을 나타내는 것이 좋다.
    - `접근 제한자 (Access Modifiers)` : 메서드의 접근 범위를 지정한다. 기본값 private
    - `본문 (Body)` : 메소드가 수행할 작업을 정의하는 코드 블록으로 중괄호로 감싸여 있다.
    - `매개변수 (Parameters)`

## 클래스 - 메서드 (매개변수)
- 메서드가 작업을 수행하는 데 필요한 데이터를 전달받는 변수
    ```cs
    // 필드
    string brand;
    string model;
    string color;

    public void SetInformation(string brand, string model, string color)
    {
        // 매개변수와 필드의 이름이 같으면 구분할 수 없게된다.
        // 필드를 구분지어주기위해 this 를 붙여준다.
        this.brand = brand;
        this.model = model;
        this.color = color;
    }
    ```
- 생성자에도 매개변수를 사용할 수 있다.
    ```cs
    class Car
    {
        // 필드
        string brand;
        string model;
        string color;

        // 생성자 
        public Car(string brand, string model, string color)
        {
            // 생성과 동시에 생성자를 통해 필드의 값을 초기화
            this.brand = brand;
            this.model = model;
            this.color = color;
        }

        ...

    }
    ```

## 클래스 - 생성자 (선택적 매개변수)
- 매개변수에 값을 미리 지정할 수 있다.
- 값을 미리 지정해주면 해당 매개변수 값의 입력은 필수가 아니게된다. (=선택적 매개변수)
    ```cs
    // 객체 생성시 color 를 지정해주지 않아도 오류가 발생하지 않는다.
    Car car = new Car("현대", "제네시스");

    class Car
    {
        string brand;
        string model;
        string color;

        // 생성자 정의에서 color 에 기본값을 설정 (color 는 선택적 매개변수)
        public Car(string brand, string model, string color = "파랑")
        {
            this.brand = brand;
            this.model = model;
            this.color = color;
        }

        ...

    }
    ```
- 선택적 매개변수를 설정시 필수 매개변수를 먼저 적고 마지막에 선택적 매개변수를 적어주도록한다.
    ```cs
    // a,b = 필수 매개변수
    // c = 선택적 매개변수
    public MyFunction(int a, int b, int c = 0) { }
    ```

## 클래스 - 메서드 (가변 매개변수)
- `params` 키워드를 사용하게되면 이 이후에 나오는 배열값은 매개변수는 하나지만, 마치 여러개인것처럼 넣어줄 수 있다. (파라미터의 길이가 가변)
    ```cs
    MyFunc("Red", "Blue", "Green");
    MyFunc("Red", "Blue");
    MyFunc("Red");
    MyFunc();

    void MyFunc(params string[] options)
    {
        //
    }
    ```
- 가변 매개변수 뒤에 일반 매개변수가 위치할 순 없다.
    ```cs
    void MyFunc(params string[] options, int num) {}    // ❌ 오류발생
    void MyFunc(int num, params string[] options) {}    // ⭕ 가능
    ```

## 메서드 - 매개변수의 특징
```cs
void Test(int a)
{
    ++a;
    Console.WriteLine($"메서드 내 a : {a}");    // 메서드 내 a : 11
}

int a = 10;
Test(a);
Console.WriteLine($"메서드 외부 a : {a}");   // 메서드 외부 a : 10
```
- 값 타입의 변수를 다른 변수에 할당하면, 값 자체가 복사된다는 특징이 있다. 때문에 매개변수로 복사된 a 가 들어가게된다. (변수a != 복사된 매개변수 a)
- 함수 내부에서 어떠한 변화가 생긴다한들 외부에 있는 변수 a에는 어떠한 영향도 주지못한다.

```cs
void Test(int[] a)
{
    ++a[0];
    Console.WriteLine($"메서드 내 a : {a[0]}");    // 메서드 내 a[0] : 11
}

int[] a = [10];
Test(a);
Console.WriteLine($"메서드 외부 a : {a[0]}");   // 메서드 외부 a[0] : 11
```
- 참조 타입은 힙 영역에 할당되고 변수는 그 데이터를 참조하고 있기에 참조 타입을 다른 변수에 할당하면, 값이 아닌 메모리 주소가 복사된다. 즉, 원본과 사본 둘다 동일한 데이터를 참조하게된다.

```cs
void Test(string copy)
{
    copy = "b";
    Console.WriteLine($"copy : {copy}");    // copy : b
}

string origin = "a";
Test(origin);
Console.WriteLine($"origin : {origin}");    // origin : a
```
- 참조타입은 다른 변수에 값 할당시 메모리 주소를 넘기기에 함수내에서의 변화가 외부에도 적용된다. 하지만, string 은 참조타입이지만 불변의 특징을 가지고 있다.
- 때문에 참조타입이지만 매개변수로 값이 넘어갈때 값 타입처럼 독립된 값이 생성된다.

## 메서드 - ref 키워드
- 함수에서 매개변수의 앞에 ref 키워드를 추가할 수 있다.
- reference 의 약자로 매개변수를 넘길때 참조경로를 넘기도록하는 키워드다.
- 매개변수로 값을 넣어줄 경우 해당 변수는 반드시 초기화가 된 상태여야한다. 
- 또한, 마찬가지로 인자 앞에 ref 키워드를 붙여줘야한다.
    ```cs
    void Test(ref string copy)
    {
        copy = "b";
        Console.WriteLine($"copy : {copy}");    // copy : b
    }

    string origin = "a";
    Test(ref origin);
    Console.WriteLine($"origin : {origin}");    // origin : b
    ```
- 값 타입에도 마찬가지로 적용된다.
    ```cs
    void Test(ref int copy)
    {
        copy = 2;
        Console.WriteLine($"copy : {copy}");    // copy : 2
    }

    int origin = 1;
    Test(ref origin);
    Console.WriteLine($"origin : {origin}");    // origin : 2
    ```

## 메서드 - out 키워드
- ref 와 마찬가지로 참조를 반환한다. 
- 하지만, ref 와 달리 외부에서 초기화되지 않은 값을 인자로 받아도 괘찮다.
    ```cs
    void Test(out int copy)
    {
        copy = 2;
        Console.WriteLine($"copy : {copy}");    // copy : 2
    }

    int origin;
    Test(out origin);
    Console.WriteLine($"origin : {origin}");    // origin : 2
    ```
- 단, out 키워드는 메서드 내부에서 반드시 값이 할당되어야 한다는 조건이있다.
- 만약 값이 할당되지 않는다면 오류가 발생한다.
    ```cs
    void Test(out int copy) 
    {
        if(false)
        {
            copy = 2;   // ❌ 오류 발생
        }
        Console.WriteLine($"copy : {copy}");    // copy : 2
    }
    ```
- out 키워드와 선언문은 합칠 수 있다.
    ```cs
    int origin;
    Test(out origin);
    //  ↓ 합치기 ↓
    Test(out int origin);
    ```
- out 키워드의 일반적이 사용 형태 (주로 boolean 반환값을 가진 메서드와 함께 사용된다.)
    ```cs
    string text = "1";
    bool success = int.TryParse(text, out int result);
    if (success)
        Console.WriteLine(result);
    else
        Console.WriteLine($"변환에 실패했습니다. result : {result}");
    ```

## 속성 - 선언 (Property)
- 클래스나 구조체는 필드에 대한 간접적인 접근 방법을 제공한다.
- `스니펫 (Snipet)` : 프로그래밍에서 자주 사용되는 코드의 조각(템플릿)으로, 반복적인 타이핑을 줄이고 생산성을 높이기 위해 저장해 두었다가 불러와 사용한다.
    - `propfull`</br>클래스 선언 → 클래스 블록안에 propfull + tab + tab → 변수와 속성 구조 생성
        ```cs
        class MyClass
        {
            // ↓ propfull 을 통해 생성된 코드들
            private int myVar;

            public int MyProperty
            {
                get { return myVar; }
                set { myVar = value; }
            }
        }
        ```
    - `prop`</br>prop -> tab -> "자동 구현 속성" 생성
        ```cs
        class MyClass
        {
            // ↓ prop 을 통해 생성된 코드
            public int MyProperty { get; set; }
        }
        ```
        자동 구현 속성은 백업 필드를 명시적으로 정의하지 않고도 속성을 간편하게 선언할 수 있는 방법이다. 컴파일러가 자동으로 백업 필드를 생성해준다.

## 속성 - getter
- 속성은 읽기 전용(Read-Only) 및 쓰기 전용(Write-Only)으로 만들 수 있다.
- Read-Only :</br>필드에 대해 getter 만 존재한다면 해당 값은 생성자에서만 설정할 수 있다.
    ```cs
    MyClass myClass = new MyClass("Jane");
    Console.WriteLine(myClass.Name);    // Jane

    class MyClass
    {
        public MyClass(string name)
        {
            Name = name;
        }
        public string Name { get; }
    }
    ```
- getter 를 통해 가공된 데이터를 반환할 수 있다.
    ```cs
    MyClass myClass = new MyClass("Jane");
    Console.WriteLine(myClass.Name);    // 이름은 Jane 입니다.

    class MyClass
    {
        // ...

        private string _name;
        public string Name 
        {
            get { return $"이름은 {_name} 입니다."; }
        }
    }
    ```
- getter 를 "식 본문 (Expression-bodied members)"을 활용하여 간결하게 작성할 수 있다.
    ```cs
    // 원형
    get { return $"이름은 {_name} 입니다."; }
    // 식 본문_1
    get => $"이름은 {_name} 입니다.";
    // 식 본문_2
    public string Name => $"이름은 {_name} 입니다.";
    ```
    식 본문이란 메서드, 속성, 생성자 등 멤버 구현이 단일 식으로 이루어질 때, `=>`연산자를 사용하여 코드를 매우 간결하게 표현하는 기능으로 C# 6.0 부터 도입되었다.

## 속성 - setter 
- 기본형태
    ```cs
    class MyClass
    {
        private string _name;

        public string Name
        {
            set { _name = value; }
        }
    }
    ```
- setter 도 getter 와 마찬가지로 값 가공이 가능하다.
    ```cs
    public string Name
    {
        set 
        { 
            _name = $"** {value} **";
        }
    }
    ```

## 속성 - setter 접근 제어자
- 속성 내부의 getter, setter 에도 접근제한자 지정이 가능하다.
- 단, 둘 모두에 private 설정을하면 외부에서 접근을 할 수 없기에 그렇게 할순 없다.
    ```cs
    class MyClass
    {
        private string _name;

        public string Name
        {
            get => _name;
            private set
            { 
                _name = $"** {value} **";
            }
        }

        // 속성을 통해 외부에서 접근할 수 없기에 별도의 함수를 통해 _name에 접근하여 값 수정
        public void SetName(string name)
        {
            Name = name;    // 클래스 내부에서 Name 호출가능
        }
    }
    ```
- `init`</br>
    속성에서 init을 설정하면 생성자에서만 속성을 할당할 수 있게된다. (초기값 전용 속성)
    ```cs
    class MyClass
    {
        public MyClass(string name)
        {
            Name = name;   
        }

        private string _name;

        public string Name
        {
            get => _name;
            init
            { 
                _name = $"** {value} **";
            }
        }
    }
    ```

## 클래스 - 소멸자

- 소멸자는 객체가 메모리에서 제거될 때 호출된다.
- 주로 비관리 리소스의 해제를 담당한다.
- C# 에서는 "Dispose 패턴"이나 "using 문"을 사용하는 것이 일반적이다.
    ```cs
    class MyClass
    {
        private readonly int _index;

        public MyClass(int index)
        {
            this._index = index;
        }

        ~MyClass()  // 소멸자
        {
            Console.WriteLine(_index);
        }
    }
    ```

## const, readonly
- `const`(상수)는 컴파일 시점에 값이 결정된다. 때문에 컴파일 이후에는 값을 절대 변경할 수 없다.
- const 는 초기화가 되지 않으면 오류가 발생한다.
    ```cs
    const double PI;            // ❌ 오류발생
    const double PI = 3.14159;  // ⭕

    // 생성자는 런타임도중에 호출되기에 const 값 변경 불가능
    public MyClass()
    {
        PI = 3,14;  // ❌ 오류발생
    }
    ```
- `readonly`(읽기 전용 필드)는 런타임 시점에 값이 설정될 수 있다. 주로 생성자에서 값을 할당하며, 이후 변겨이 불가능하다.
- 즉, 객체가 생성될때에만 값이 할당될 수 있다.
    ```cs
    class MyClass
    {
        readonly double pi;
        // 생성자에서 값 초기화
        public MyClass()
        {
            pi = 3.14;  // ⭕
        }
    }
    ```

## 정적(static) 메서드
- 정적 메서드는 클래스 자체에 속하며, 객체의 인스턴스 없이 호출할 수 있다.
    ```cs
    // 인스턴스 생성후 호출
    MyClass myClass = new MyClass();
    myClass.Print();    // ❌ 오류발생

    // 인스턴스 생성없이 호출
    MyClass.Print();    // ⭕

    class MyClass
    {
        public static void Print()
        {
            Console.WriteLine("Hello");
        }
    }
    ```
- 정적 메서드는 정적 멤버에만 접근할 수 있다.
    ```cs
    public string MyText => "MyText";       // 일반 속성
    public static string Name => "MyClass"; // 정적 속성

    public static void Print()
    {
        Console.WriteLine($"{MyText} Hello");   // ❌ 오류발생
        Console.WriteLine($"{Name} Hello");     // ⭕
    }
    ```
- 만약, 정적 함수에서 정적 멤버가 아닌 일반 멤버에 접근하고자한다면 내부에서 인스턴스를 생성해야한다.
    ```cs
    class MyClass
    {
        public string MyText => "MyText";
        public static void Print()
        {
            MyClass myClass = new MyClass();
            Console.WriteLine(myClass.MyText);
        }
    }
    ```

## 정적 클래스, 확장 함수
- 정적 클래스의 경우 인스턴스를 생성할 수 없으며, 모든 멤버가 static으로 선언되어야 한다.
- 상속 불가능
- 정적 클래스는 프로그램이 시작될때 한번 메모리에 로드된다. 프로그램이 종료될때까지 메모리에 남아있기에 메모리 관리에 주의해야한다.
- 확장 함수를 만들 수 있다.
- 주로 유틸리티 메서드를 제공할때 사용된다.
    ```cs
    string name = "Jane";
    // 일반적인 사용
    MyClass.CustomPrint(name);  // Jane
    // 확장함수 사용
    name.CustomPrint(); // Jane 

    static class MyClass
    {
        // string 타입에대한 확장함수 생성
        public static void CustomPrint(this string text)
        {
            Console.WriteLine(text);
        }
    }
    ```

## 상속
- 상속은 부모 클래스의 기능을 자식 클래스가 사용할 수 있도록하는 기능이다.
    - 부모 클래스(Base Class) : 다른 클래스에 의해 상속되는 클래스
    - 자식 클래스(Derived Class) : 다른 클래스를 상속받는 클래스
    ```cs
    Animal animal = new Animal();
    animal.Eat();
    //animal.Bark();    // ❌ 오류발생

    Dog dog = new Dog();
    dog.Eat();
    dog.Bark();

    class Animal
    { 
        public void Eat()
        {
            Console.WriteLine("먹는다.");
        }
    }

    class Dog : Animal
    {
        public void Bark()
        {
            Console.WriteLine("짖는다.");
        }
    }
    ```

## 상속 - 재정의
- 부모의 메서드를 자식 클래스에서 재정의하는것을 의미한다.
- `virtual` 키워드와 `override` 키워드를 사용한다.
    ```cs
    Animal animal = new Animal();
    animal.Eat();   // 먹는다.

    Dog dog = new Dog();
    dog.Eat();  // 사료를 먹는다.

    class Animal
    { 
        public virtual void Eat()
        {
            Console.WriteLine("먹는다.");
        }
    }

    class Dog : Animal
    {
        public override void Eat()  // 재정의
        {
            Console.WriteLine("사료를 먹는다.");
        }
    }
    ```

## 상속 - override, new 차이
- new 키워드는 부모의 메서드를 숨기고 본인의 메서드를 사용한다.
- new 를 사용하면 업캐스팅시 override 와 차이점을 보인다.
    ```cs
    Animal animal = new Animal();
    Animal dog = new Dog();
    Animal cat = new Cat();

    animal.Eat();   // 먹는다.
    dog.Eat();      // 강아지가 먹는다.
    cat.Eat();      // 먹는다.

    class Dog : Animal
    {
        public override void Eat()
        {
            Console.WriteLine("강아지가 먹는다.");
        }
    }

    class Cat : Animal
    {
        public new void Eat()
        {
            Console.WriteLine("고양이가 먹는다.");
        }
    }
    ```

## 상속 - 접근 제어자
- public : 모든 클래스에서 접근 가능
- protected : 해당 클래스와 자식 클래스에서만 접근 가능
- private : 해당 클래스 내에서만 접근 가능
- internal : 같은 어셈블리 내에서 접근 가능

## 상속 - 추상 클래스
- 추상 클래스는 인스턴스화 할 수 없으며, 상속만 할 수 있다.
    ```cs
    Animal animal = new Animal();   // ❌ 오류발생
    Animal dog = new Dog();         // ⭕
    Animal cat = new Cat();         // ⭕

    // 추상 메서드
    abstract class Animal
    {
        public abstract void Eat(); // 추상 메서드
    }

    class Dog : Animal
    {
        public override void Eat()
        {
            Console.WriteLine("강아지가 먹는다.");
        }
    }

    class Cat : Animal
    {
        public override void Eat()
        {
            Console.WriteLine("고양이가 먹는다.");
        }
    }
    ```
- 추상 메서드를 포함할 수 있으며, 추상 메서드는 자식 클래스에서 반드시 구현해야한다.

## 상속 - 상속 체인
- 연쇄적으로 상속을 할 순 있지만, 다중 상속은 안된다.
    ```cs
    abstract class Animal { }

    // Animal <- Cat
    class Cat : Animal { }

    // Cat <- Tiger
    class Tiger : Cat { }   
    ```
- 다중 상속은 허용되지 않는다.
    ```cs
    class Tiger : Cat, Test // ❌ 오류발생
    { }
    ```

## 상속 - sealed
- 상속을 막는 기능을 한다.
    ```cs
    sealed class Cat : Animal { }
    // Cat <- Tiger
    class Tiger : Cat { }   // ❌ 오류발생
    ```
- 재정의를 막는 기능을 한다.
    ```cs
    abstract class Animal 
    {
        public abstract void Move();
    }

    // Animal <- Cat
    class Cat : Animal 
    {
        public sealed override void Move()
        {
        }
    }

    // Cat <- Tiger
    class Tiger : Cat 
    {
        public override void Move() // ❌ 오류발생
        {
        }
    }
    ```