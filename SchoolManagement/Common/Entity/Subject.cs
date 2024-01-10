using SchoolManagement.Common.Enum;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Common.Entity;

public class Subject
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public int TeacherId { get; set; }

    public Teacher Teacher { get; set; }

    public ICollection<StudentSubject>? StudentSubjects { get; set; }

    public ICollection<Mark>? Marks { get; set; }
}
