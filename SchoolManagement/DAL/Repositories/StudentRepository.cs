using SchoolManagement.Common.Entity;

namespace SchoolManagement.DAL.Repositories;

public class StudentRepository : BaseRepository<Student>
{
    public StudentRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
