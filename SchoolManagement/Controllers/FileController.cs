using Microsoft.AspNetCore.Mvc;
using SchoolManagement.BusinessLogic.Services;

namespace SchoolManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class FileController : ControllerBase
{
    private readonly FileService _fileService;

    public FileController(FileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet("{fileName}")]
    public async Task<IActionResult> GetFileByNameAsync(string fileName)
    {
        try
        {
            var file = await _fileService.GetFileByNameAsync(fileName);

            if (file == null)
            {
                return NotFound();
            }

            return File(file.Content, "application/octet-stream", file.FileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal Server Error: {ex.Message}");
        }
    }

    [HttpGet]
    public IEnumerable<WeatherForecast> Get()
    {
        string[] Summaries = new[]
{
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }
}
