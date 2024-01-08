using SchoolManagement.BusinessLogic.Interfaces;
using SchoolManagement.Common.Entity;
using SchoolManagement.DAL.Repositories;
using SchoolManagement.Storage;

namespace SchoolManagement.BusinessLogic.Services;

public class TeacherService : ITeacherService
{
    private readonly TeacherRepository _teacherRepository;

    private readonly BlobStorageHelper _blobStorageHelper;

    public TeacherService(TeacherRepository teacherRepository, BlobStorageHelper blobStorageHelper)
    {
        _teacherRepository = teacherRepository;
        _blobStorageHelper = blobStorageHelper;
    }

    public async Task AddTeacher(Teacher teacher)
    {
        try
        {
            await _teacherRepository.CreateAsync(teacher);
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteTeacher(int teacherId)
    {
        try
        {
            await _teacherRepository.DeleteAsync(teacherId);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Teacher>> GetAllTeachers()
    {
        try
        {
            return await _teacherRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred while fetching entities.", e);
        }
    }

    public async Task<Teacher?> GetTeacherById(int teacherId)
    {
        try
        {
            return await _teacherRepository.GetByIdAsync(teacherId);
        }
        catch
        {
            throw;
        }
    }

    public async Task UpdateTeacher(int id, Teacher teacher)
    {
        try
        {
            await _teacherRepository.UpdateAsync(id, teacher);
        }
        catch
        {
            throw;
        }
    }

    public async Task UpdateUserProfilePicture(int teacherId, IFormFile selectedProfilePicture)
    {
        var teacher = await _teacherRepository.GetByIdAsync(teacherId) ?? throw new Exception("Teacher not found.");

        teacher.ProfilePictureFileName = await _blobStorageHelper.UploadFile(selectedProfilePicture);

        await _teacherRepository.UpdateAsync(teacher.Id, teacher);
    }
}
