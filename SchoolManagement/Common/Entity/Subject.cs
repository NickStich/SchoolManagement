using SchoolManagement.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Common.Entity;

public class Subject
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public ICollection<StudentSubject> StudentSubjects { get; set; }
}
