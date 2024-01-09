using Microsoft.AspNetCore.Mvc;
using SchoolManagement.BusinessLogic.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
}
