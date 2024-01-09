using SchoolManagement.Common.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.BusinessLogic.Interfaces;
public interface ISubjectService
{
    Task AddSubject(Subject subject);
    Task DeleteSubject(int subjectId);
    Task<IEnumerable<Subject>> GetAllSubjects();
    Task<Subject?> GetSubjectById(int subjectId);
    Task UpdateSubject(int id, Subject subject);
}