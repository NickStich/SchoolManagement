using Microsoft.EntityFrameworkCore;
using SchoolManagement.Common.Entity;

namespace SchoolManagement.DAL;

public class SchoolDbContext : DbContext
{
    public readonly string _connectionString;

	public SchoolDbContext()
	{
		_connectionString = @"Data Source=ROMOB41160\SQL2014;Initial Catalog=SchoolManagement;Integrated Security=True; Encrypt=False"; // connection string of the DB | better to store separate
	}

    public SchoolDbContext(DbContextOptions<SchoolDbContext> options)
    {
        _connectionString = @"Data Source=ROMOB41160\SQL2014;Initial Catalog=SchoolManagement;Integrated Security=True; Encrypt=False"; // connection string of the DB | better to store separate
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_connectionString);
    }

    public DbSet<Person> People { get; set; }

    public DbSet<Student> Students { get; set; }

    public DbSet<Teacher> Teachers { get; set; }

    public DbSet<Subject> Subjects { get; set; }

    public DbSet<Course> Courses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Person
        modelBuilder.Entity<Person>()
            .HasKey(p => p.Id);

        // Configure Student
        modelBuilder.Entity<Student>()
            .HasBaseType<Person>()
            .Property(s => s.Grade)
            .IsRequired();

        // Configure Teacher
        modelBuilder.Entity<Teacher>()
            .HasBaseType<Person>();

        // Configure Subject
        modelBuilder.Entity<Subject>()
            .HasKey(s => s.Id);

        modelBuilder.Entity<Subject>()
            .HasMany(s => s.Teachers)
            .WithOne(t => t.Subject)
            .HasForeignKey(t => t.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        // Configure Course
        modelBuilder.Entity<Course>()
            .HasKey(c => c.Id);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Subject)
            .WithMany(s => s.Courses)
            .HasForeignKey(c => c.SubjectId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.Teacher)
            .WithMany(t => t.Courses)
            .HasForeignKey(c => c.TeacherId);

        base.OnModelCreating(modelBuilder);
    }
}
