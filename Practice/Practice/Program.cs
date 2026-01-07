

using System.Reflection.Metadata;

List<Student> students = [
    new Student { Id = 1, Name = "Alice", Age = 20},
    new Student { Id = 2, Name = "Bob", Age = 22},
    new Student { Id = 3, Name = "Charlie", Age = 23},
    new Student { Id = 4, Name = "David", Age = 21},
    new Student { Id = 5, Name = "Eve", Age = 20},
];


 Console.ReadKey();

class Student
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public string Gender { get; set; } = "";
    public List<int> Scores { get; set; } = [];

    public override string ToString()
    {
        return $"Id : {Id}, Name : {Name}, Age : {Age}";
    }
}