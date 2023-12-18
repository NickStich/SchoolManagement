using SchoolManagement.Common.Entity;

namespace SchoolManagement.DAL.Repositories;

public class CourseRepository : BaseRepository<Course>
{
    public CourseRepository(SchoolDbContext dbContext) : base(dbContext)
    {
    }
}
