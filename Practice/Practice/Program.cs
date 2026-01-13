


void 출금()
{
    잔액확인1();
}

void 잔액확인1()
{
    잔액확인2();
}

void 잔액확인2()
{
    잔액확인3();
}

void 잔액확인3()
{
    throw new 잔액부족Exception("잔액이 부족합니다.", 5000, 10000);
    // 잔액 부족 예외 발생
    // 잔액 : 5,000원 , 출금 10,000원
}

try
{
    출금();
}
catch (잔액부족Exception ex)
{
    Console.WriteLine($"Message : {ex.Message}, 잔액 : {ex.잔액}, 출금 : {ex.출금액}");
}
// Message : 잔액이 부족합니다., 잔액 : 5000, 출금 : 10000

Console.ReadKey();

class 잔액부족Exception : Exception
{
    public 잔액부족Exception(string message, decimal 잔액, decimal 출금액) : base(message)
    {
        this.잔액 = 잔액;
        this.출금액 = 출금액;
    }

    public decimal 잔액 { get; }
    public decimal 출금액 { get; }
}
