using SchoolManagement.Common.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.DAL.Interfaces;
public interface IStudentRepository
{
    Task<IEnumerable<Mark>> GetStudentMarksAsync(int studentId);
}