using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Common.Entity;

public class Mark
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public DateTime Date { get; set; }

    public int Score { get; set; }

    public virtual Student Student { get; set; }

    public virtual Subject Subject { get; set; }
}
