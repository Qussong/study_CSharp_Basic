


var fruits = new Stack<string>();
fruits.Push("사과");
fruits.Push("바나나");
fruits.Push("체리");

Console.Write(fruits.Pop() + " ");
Console.Write(fruits.Pop() + " ");
Console.Write(fruits.Peek() + " ");
Console.Write(fruits.Pop());
// 체리 바나나 사과 사과

//Console.Write(fruits.Pop());    //  ❌ 오류발생
fruits.TryPop(out string? newFruit);
Console.Write(newFruit);

Console.ReadKey();