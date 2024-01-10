using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.BusinessLogic.Interfaces;
using SchoolManagement.Common.Entity;
using System;
using System.Threading.Tasks;

namespace SchoolManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly IStudentService _studentService;

    public StudentController(IStudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllStudents()
    {
        var students = await _studentService.GetAllStudents();
        return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetStudentById(int id)
    {
        var student = await _studentService.GetStudentById(id);

        if (student == null)
            return NotFound();

        return Ok(student);
    }

    [HttpPost]
    public async Task<IActionResult> AddStudent([FromBody] Student student)
    {
        await _studentService.AddStudent(student);
        return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student student)
    {
        if (id != student.Id)
            return BadRequest();

        await _studentService.UpdateStudent(id, student);

        return StatusCode(202, student);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteStudent(int id)
    {
        await _studentService.DeleteStudent(id);
        return NoContent();
    }

    [HttpPut("uploadProfilePicture/{studentId}")]
    public async Task<IActionResult> UploadProfilePicture(int studentId, [FromForm] IFormFile selectedProfilePicture)
    {
        if (selectedProfilePicture != null)
        {
            await _studentService.UpdateUserProfilePicture(studentId, selectedProfilePicture);
        }

        return new OkObjectResult("It worked");
    }

    [HttpGet("{studentId}/marks")]
    public async Task<IActionResult> GetStudentMarks(int studentId)
    {
        try
        {
            var marks = await _studentService.GetStudentMarksAsync(studentId);

            return Ok(marks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }
}
