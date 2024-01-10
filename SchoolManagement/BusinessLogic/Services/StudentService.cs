using Microsoft.AspNetCore.Http;
using SchoolManagement.BusinessLogic.Interfaces;
using SchoolManagement.Common.Entity;
using SchoolManagement.DAL.Interfaces;
using SchoolManagement.DAL.Repositories;
using SchoolManagement.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.BusinessLogic.Services;

public class StudentService : IStudentService
{
    private readonly StudentRepository _studentRepository;

    private readonly BlobStorageHelper _blobStorageHelper;

    public StudentService(StudentRepository studentRepository, BlobStorageHelper blobStorageHelper)
    {
        _studentRepository = studentRepository;
        _blobStorageHelper = blobStorageHelper;
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

    public async Task UpdateUserProfilePicture(int studentId, IFormFile selectedProfilePicture)
    {
        var student = await _studentRepository.GetByIdAsync(studentId) ?? throw new Exception("Student not found.");

        student.ProfilePictureFileName = await _blobStorageHelper.UploadFile(selectedProfilePicture);

        await _studentRepository.UpdateAsync(student.Id, student);
    }

    public async Task<IEnumerable<Mark>> GetStudentMarksAsync(int studentId)
    {
        try
        {
            return await _studentRepository.GetStudentMarksAsync(studentId);
        }
        catch
        {
            throw;
        }
    }
}
