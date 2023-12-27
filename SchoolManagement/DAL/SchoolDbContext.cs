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

    //public DbSet<Course> Courses { get; set; }

    public DbSet<StudentSubject> StudentSubjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure Person
        modelBuilder.Entity<Person>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Person>()
            .Property(u => u.ProfilePictureFileName)
            .HasMaxLength(255);

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
            .Property(s => s.Name)
            .HasMaxLength(255)
            .IsRequired();

        // Configure StudentSubject
        modelBuilder.Entity<StudentSubject>()
            .HasKey(ss => new { ss.StudentId, ss.SubjectId });

        modelBuilder.Entity<StudentSubject>()
            .HasOne(ss => ss.Student)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(ss => ss.StudentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<StudentSubject>()
            .HasOne(ss => ss.Subject)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(ss => ss.SubjectId)
            .OnDelete(DeleteBehavior.Cascade);

        // Adjust cascade behavior for the Subject-Student relationship to prevent cycles
        modelBuilder.Entity<Student>()
            .HasMany(s => s.StudentSubjects)
            .WithOne(ss => ss.Student)
            .HasForeignKey(ss => ss.StudentId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}
