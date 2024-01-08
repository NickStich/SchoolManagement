using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Common.Entity;

namespace SchoolManagement.DAL;

public class SchoolDbContext : DbContext
{
    //public readonly string _connectionString;

    //public SchoolDbContext()
    //{
    //    //_connectionString = @"Data Source=ROMOB41160\SQL2014;Initial Catalog=SchoolManagement;Integrated Security=True; Encrypt=False"; // connection string of the DB | better to store separate
    //}

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
    {
        //_connectionString = @"Data Source=ROMOB41160\SQL2014;Initial Catalog=SchoolManagement;Integrated Security=True; Encrypt=False"; // connection string of the DB | better to store separate
    }

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    if (!optionsBuilder.IsConfigured)
    //    {
    //        // Use the "DefaultConnection" connection string from appsettings.json
    //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
    //    }
    //}

    public DbSet<Student> Students { get; set; }

    public DbSet<Teacher> Teachers { get; set; }

    public DbSet<Subject> Subjects { get; set; }

    public DbSet<Mark> Marks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Student-Subject relationship
        modelBuilder.Entity<Student>()
            .HasMany(s => s.Subjects)
            .WithMany(s => s.Students)
            .UsingEntity(j => j.ToTable("StudentSubjects"));

        // Configure Subject-Teacher relationship
        modelBuilder.Entity<Subject>()
            .HasOne(s => s.Teacher)
            .WithMany(t => t.Subjects)
            .HasForeignKey(s => s.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Mark-Student and Mark-Subject relationships
        modelBuilder.Entity<Mark>()
            .HasOne(m => m.Student)
            .WithMany(s => s.Marks)
            .HasForeignKey(m => m.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Mark>()
            .HasOne(m => m.Subject)
            .WithMany(s => s.Marks)
            .HasForeignKey(m => m.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}
