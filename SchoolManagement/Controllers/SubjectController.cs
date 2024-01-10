using Microsoft.AspNetCore.Mvc;
using SchoolManagement.BusinessLogic.Interfaces;
using SchoolManagement.Common.Entity;
using System.Threading.Tasks;

namespace SchoolManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class SubjectController : ControllerBase
{
    private readonly ISubjectService _subjectService;

    public SubjectController(ISubjectService subjectService)
    {
        _subjectService = subjectService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllSubjects()
    {
        var subjects = await _subjectService.GetAllSubjects();
        return Ok(subjects);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSubjectById(int id)
    {
        var subject = await _subjectService.GetSubjectById(id);

        if (subject == null)
            return NotFound();

        return Ok(subject);
    }

    [HttpPost]
    public async Task<IActionResult> AddSubject([FromBody] Subject subject)
    {
        await _subjectService.AddSubject(subject);
        return CreatedAtAction(nameof(GetSubjectById), new { id = subject.Id }, subject);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSubject(int id, [FromBody] Subject subject)
    {
        if (id != subject.Id)
            return BadRequest();

        await _subjectService.UpdateSubject(id, subject);

        return StatusCode(202, subject);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSubject(int id)
    {
        await _subjectService.DeleteSubject(id);
        return NoContent();
    }
}
