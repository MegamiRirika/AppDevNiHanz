namespace Lab_1
{
    public class Teacher
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public int TeacherId { get; set; }
        public Teacher? Teacher { get; set; }
    }
}