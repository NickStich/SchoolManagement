using SchoolManagement.Common.Entity;

namespace SchoolManagement.DAL.Repositories;

public class SubjectRepository : BaseRepository<Subject>
{
    public SubjectRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
