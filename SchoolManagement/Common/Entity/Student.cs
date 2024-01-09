using System.Collections.Generic;

namespace SchoolManagement.Common.Entity;

public class Student : Person
{
    public int Grade { get; set; }

    public virtual ICollection<Subject>? Subjects { get; set; }

    public virtual ICollection<Mark>? Marks { get; set; }
}
