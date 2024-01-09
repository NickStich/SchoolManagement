using SchoolManagement.Common.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.DAL.Interfaces;
public interface IStudentRepository
{
    Task AddStudent(Student student);
    void DeleteStudent(int studentId);
    IEnumerable<Student> GetAllStudents();
    Student GetStudentById(int studentId);
    void UpdateStudent(Student student);
}