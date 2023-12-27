using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Common.Entity;

public class StudentSubject
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    [ForeignKey(nameof(StudentId))]
    public Student Student { get; set; }

    [ForeignKey(nameof(SubjectId))]
    public Subject Subject { get; set; }
}
