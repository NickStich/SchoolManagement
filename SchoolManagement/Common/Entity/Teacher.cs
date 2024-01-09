using System.Collections.Generic;

namespace SchoolManagement.Common.Entity;

public class Teacher : Person
{
    public ICollection<Subject>? Subjects { get; set; }
}
