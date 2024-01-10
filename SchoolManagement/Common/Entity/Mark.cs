using System;
using System.ComponentModel.DataAnnotations;

namespace SchoolManagement.Common.Entity;

public class Mark
{
    [Key]
    public int Id { get; set; }

    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public DateTime Date { get; set; }

    public int Value { get; set; }

    public Student Student { get; set; }

    public Subject Subject { get; set; }
}
