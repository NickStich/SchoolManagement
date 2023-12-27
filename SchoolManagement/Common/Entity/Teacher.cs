namespace SchoolManagement.Common.Entity;

public class Teacher : Person
{
    public Subject Subject { get; set; }

    public int SubjectId { get; set; }

}
