﻿using Microsoft.AspNetCore.Http;
using SchoolManagement.Common.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.BusinessLogic.Interfaces;
public interface IStudentService
{
    Task AddStudent(Student student);
    Task DeleteStudent(int studentId);
    Task<IEnumerable<Student>> GetAllStudents();
    Task<Student?> GetStudentById(int studentId);
    Task UpdateStudent(int id, Student student);
    Task UpdateUserProfilePicture(int studentId, IFormFile selectedProfilePicture);
    Task<IEnumerable<Mark>> GetStudentMarksAsync(int studentId);
}