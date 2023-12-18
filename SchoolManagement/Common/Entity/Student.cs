namespace SchoolManagement.Common.Entity;

public class Student : Person
{
    public int Grade { get; set; }

    public IEnumerable<Course>? Courses { get; set; }
}
