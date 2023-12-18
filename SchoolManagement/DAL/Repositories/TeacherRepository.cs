using SchoolManagement.Common.Entity;

namespace SchoolManagement.DAL.Repositories;

public class TeacherRepository : BaseRepository<Teacher>
{
    public TeacherRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
