using System.Collections.Generic;

namespace SchoolManagement.Common.Entity;

public class Student : Person
{
    public int Grade { get; set; }

    public ICollection<StudentSubject>? StudentSubjects { get; set; }

    public ICollection<Mark>? Marks { get; set; }
}
