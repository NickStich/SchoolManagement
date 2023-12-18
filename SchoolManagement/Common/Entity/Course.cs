using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Common.Entity;

public class Course
{
    [Key]
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public int SubjectId { get; set; }

    public Subject Subject { get; set; }

    public int TeacherId { get; set; }

    public Teacher Teacher { get; set; }
}
