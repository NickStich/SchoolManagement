using Microsoft.AspNetCore.Mvc;
using SchoolManagement.BusinessLogic.Services;
using System.Threading.Tasks;
using System;
using SchoolManagement.Common.RequestDTO;
using SchoolManagement.BusinessLogic.Interfaces;

namespace SchoolManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class SchoolController : ControllerBase
{
    private readonly ISchoolService _schoolService;

    public SchoolController(ISchoolService schoolService)
    {
        _schoolService = schoolService;
    }

    [HttpPost("linkSubjectsToStudent")]
    public async Task<IActionResult> LinkSubjectsToStudentAsync([FromBody] StudentSubjects request)
    {
        try
        {
            await _schoolService.LinkSubjectsToStudentAsync(request.StudentId, request.SubjectIds);
            return Ok("Subjects linked to student successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("unlinkSubjectFromStudent")]
    public async Task<IActionResult> UnlinkSubjectsFromStudentAsync([FromBody] StudentSubject request)
    {
        try
        {
            await _schoolService.UnlinkSubjectsFromStudentAsync(request.StudentId, request.SubjectId);
            return Ok("Subject unlinked from student successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost("addMarkForStudentSubject")]
    public async Task<IActionResult> AddMarkForStudentSubjectAsync([FromBody] MarkRequest request)
    {
        try
        {
            await _schoolService.AddMarkForStudentSubjectAsync(request.StudentId, request.SubjectId, (int)request.Value, (DateTime)request.Date);
            return Ok("Mark added for student subject successfully");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpGet("getMarksBetweenDates")]
    public async Task<IActionResult> GetMarksBetweenDatesAsync([FromQuery] MarkRequest request, [FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
    {
        try
        {
            var marks = await _schoolService.GetMarksBetweenDatesAsync(request.StudentId, request.SubjectId, startDate, endDate);
            return Ok(marks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPut("updateMark")]
    public async Task<IActionResult> UpdateMarkAsync([FromBody] MarkUpdate request)
    {
        try
        {
            var success = await _schoolService.UpdateMarkAsync(request.MarkId, request.NewValue);
            if (success)
                return Ok("Mark updated successfully");
            else
                return NotFound("Mark not found");
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Internal Server Error");
        }
    }
}

