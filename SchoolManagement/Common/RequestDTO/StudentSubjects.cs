using System.Collections.Generic;

namespace SchoolManagement.Common.RequestDTO;

public class StudentSubjects
{
    public int StudentId { get; set; }

    public IEnumerable<int> SubjectIds { get; set; }
}
