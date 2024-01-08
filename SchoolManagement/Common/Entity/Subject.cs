using SchoolManagement.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Common.Entity;

public class Subject
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public int TeacherId { get; set; }
    public virtual Teacher Teacher { get; set; }

    public virtual ICollection<Student>? Students { get; set; }

    public virtual ICollection<Mark>? Marks { get; set; }
}
