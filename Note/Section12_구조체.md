# Section 12

## 구조체
|특징|구조체|클래스|
|---|---|---|
|타입|값(Value) 타입|참조(Reference) 타입|
|메모리 할당|스택(Stack)|힙(Heap)|
|상속|불가능|가능|
|용도|작은 데이터 구조|복잡한 데이터 구조 및 기능|
- 구조체는 `struct` 키워드를 사용한다.
    ```cs
    Point point = new Point { X = 10, Y = 20 };
    // 객체 생성과 동시에 속성을 초기화할땐 위와 같인 중괄호를 사용한다.
    // 클래스와 구조체 모두 사용가능한 속성 초기화 방법이다.
    // 속성이 아닌 public 필드여도 되긴하지만 비추천
    struct Point
    { 
        public int X { get; set; }
        public int Y { get; set; }

        // ToString() 메서드는 모든 타입의 최상위 부모인 object에 이미 정의되어 있는 가상 메서드다. 때문에 구조체에서도 기존 구현을 재정의(override)헤야한다.
        public override string ToString()
        {
            return $"X {X}, Y : {Y}";
        }
    }
    ```
- 구조체가 값 타입인지 확인
    ```cs
    Point point = new Point { X = 10, Y = 20 };

    void ChangePoint(Point point)
    {
        point.X = 100;
        point.Y = 200;
        Console.WriteLine($"함수 내부 : {point}");
    }

    ChangePoint(point);
    Console.WriteLine($"함수 외부 : {point}");

    // 함수 내부 : X 100, Y : 200
    // 함수 외부 : X 10, Y : 20
    ```
- `ref` 키워드를 붙이면 함수 내부에서의 변경을 외부에도 적용할 수 있다.
    ```cs
    void ChangePoint(ref Point point)
    {
        point.X = 100;
        point.Y = 200;
        Console.WriteLine($"함수 내부 : {point}");
    }

    ChangePoint(ref point);
    Console.WriteLine($"함수 외부 : {point}");

    // 함수 내부 : X 100, Y : 200
    // 함수 외부 : X 100, Y : 200
    ```
