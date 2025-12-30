
enum Days
{
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
        Console.WriteLine(day);

        Console.ReadKey();
    }
}



