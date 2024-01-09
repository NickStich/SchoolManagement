using Microsoft.AspNetCore.Http;
using SchoolManagement.Common.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.BusinessLogic.Interfaces;
public interface ITeacherService
{
    Task AddTeacher(Teacher teacher);

    Task DeleteTeacher(int teacherId);

    Task<IEnumerable<Teacher>> GetAllTeachers();

    Task<Teacher?> GetTeacherById(int teacherId);

    Task UpdateTeacher(int id, Teacher teacher);

    Task UpdateUserProfilePicture(int teacherId, IFormFile selectedProfilePicture);
}