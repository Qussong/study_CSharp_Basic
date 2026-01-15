


async Task TaskAsync()
{
    Console.WriteLine("TaskAsync Started");
    await Task.Delay(10000);
    Console.WriteLine("TaskAsync Finished");
}

async Task TaskAsync2()
{
    Console.WriteLine("TaskAsync2 Started");
    await Task.Delay(5000);
    Console.WriteLine("TaskAsync2 Finished");
}

Task task1 = TaskAsync();
Task task2 = TaskAsync2();

await Task.WhenAll(task1, task2);

Console.ReadKey();