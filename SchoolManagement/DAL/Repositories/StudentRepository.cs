using Microsoft.EntityFrameworkCore;
using SchoolManagement.Common.Entity;
using SchoolManagement.DAL.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.DAL.Repositories;

public class StudentRepository : BaseRepository<Student>, IStudentRepository
{

    private readonly SchoolDbContext _dbContext;

    public StudentRepository(SchoolDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Mark>> GetStudentMarksAsync(int studentId)
    {
        var marks = await _dbContext.Marks
            .Where(m => m.StudentId == studentId)
            .ToListAsync();

        return marks ?? Enumerable.Empty<Mark>();
    }
}
