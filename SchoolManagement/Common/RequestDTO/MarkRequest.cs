using System;

namespace SchoolManagement.Common.RequestDTO;

public class MarkRequest
{
    public int StudentId { get; set; }

    public int SubjectId { get; set; }

    public int? Value { get; set; }

    public DateTime? Date { get; set; }
}
