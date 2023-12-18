using SchoolManagement.BusinessLogic.Interfaces;
using SchoolManagement.Common.Entity;
using SchoolManagement.DAL.Interfaces;
using SchoolManagement.DAL.Repositories;

namespace SchoolManagement.BusinessLogic.Services;

public class StudentService : IStudentService
{
    private readonly StudentRepository _studentRepository;

    public StudentService(StudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    public async Task AddStudent(Student student)
    {
        try
        {
            await _studentRepository.CreateAsync(student);
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteStudent(int studentId)
    {
        try
        {
            await _studentRepository.DeleteAsync(studentId);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Student>> GetAllStudents()
    {
        try
        {
            return await _studentRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred while fetching entities.", e);
        }
    }

    public async Task<Student?> GetStudentById(int studentId)
    {
        try
        {
            return await _studentRepository.GetByIdAsync(studentId);
        }
        catch
        {
            throw;
        }
    }

    public async Task UpdateStudent(int id, Student student)
    {
        try
        {
            await _studentRepository.UpdateAsync(id, student);
        }
        catch
        {
            throw;
        }
    }
}
