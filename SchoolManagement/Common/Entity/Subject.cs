using SchoolManagement.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Common.Entity;

public class Subject
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public Profile Profile { get; set; }

    public IEnumerable<Teacher> Teachers { get; set; }

    public IEnumerable<Course> Courses { get; set; }
}
