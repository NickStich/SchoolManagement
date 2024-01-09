using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolManagement.BusinessLogic.Interfaces;
using SchoolManagement.Common.Entity;
using System.Threading.Tasks;

namespace SchoolManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class TeacherController : ControllerBase
{
    private readonly ITeacherService _teacherService;

    public TeacherController(ITeacherService teacherService)
    {
        _teacherService = teacherService;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetAllTeachers()
    {
        var teachers = await _teacherService.GetAllTeachers();
        return Ok(teachers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTeacherById(int id)
    {
        var teacher = await _teacherService.GetTeacherById(id);

        if (teacher == null)
            return NotFound();

        return Ok(teacher);
    }

    [HttpPost]
    public async Task<IActionResult> AddTeacher([FromBody] Teacher teacher)
    {
        await _teacherService.AddTeacher(teacher);

        return CreatedAtAction(nameof(GetTeacherById), new { id = teacher.Id }, teacher);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTeacher(int id, [FromBody] Teacher teacher)
    {
        if (id != teacher.Id)
            return BadRequest();

        await _teacherService.UpdateTeacher(id, teacher);

        return StatusCode(202, teacher);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTeacher(int id)
    {
        await _teacherService.DeleteTeacher(id);
        return NoContent();
    }

    [HttpPut("uploadProfilePicture/{teacherId}")]
    public async Task<IActionResult> UploadProfilePicture(int teacherId, [FromForm] IFormFile selectedProfilePicture)
    {
        if (selectedProfilePicture != null)
        {
            await _teacherService.UpdateUserProfilePicture(teacherId, selectedProfilePicture);
        }

        return new OkObjectResult("It worked");
    }
}
