namespace SchoolManagement.Common.Entity;

public class Teacher : Person
{
    public virtual ICollection<Subject>? Subjects { get; set; }
}
