
Point point = new Point { X = 10, Y = 20 };

void ChangePoint(ref Point point)
{
    point.X = 100;
    point.Y = 200;
    Console.WriteLine($"함수 내부 : {point}");
}

ChangePoint(ref point);
Console.WriteLine($"함수 외부 : {point}");

// 함수 내부 : X 100, Y : 200
// 함수 외부 : X 10, Y : 20

Console.ReadKey();

struct Point
{ 
    public int X { get; set; }
    public int Y { get; set; }

    public override string ToString()
    {
        return $"X {X}, Y : {Y}";
    }
}
