using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SchoolManagement.Common.Entity;

namespace SchoolManagement.DAL;

public class SchoolDbContext : DbContext
{

    public SchoolDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    public DbSet<Student> Students { get; set; }

    public DbSet<Teacher> Teachers { get; set; }

    public DbSet<Subject> Subjects { get; set; }

    public DbSet<Mark> Marks { get; set; }

    public DbSet<StudentSubject> StudentSubjects { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Student>()
            .HasMany(s => s.StudentSubjects)
            .WithOne(ss => ss.Student)
            .HasForeignKey(ss => ss.StudentId);

        modelBuilder.Entity<Student>()
            .HasMany(s => s.Marks)
            .WithOne(m => m.Student)
            .HasForeignKey(m => m.StudentId);

        modelBuilder.Entity<Teacher>()
            .HasMany(t => t.Subjects)
            .WithOne(s => s.Teacher)
            .HasForeignKey(s => s.TeacherId);

        modelBuilder.Entity<Subject>()
            .HasMany(s => s.StudentSubjects)
            .WithOne(ss => ss.Subject)
            .HasForeignKey(ss => ss.SubjectId);

        modelBuilder.Entity<Subject>()
            .HasMany(s => s.Marks)
            .WithOne(m => m.Subject)
            .HasForeignKey(m => m.SubjectId);

        modelBuilder.Entity<StudentSubject>()
            .HasKey(ss => ss.Id);

        modelBuilder.Entity<StudentSubject>()
            .HasOne(ss => ss.Student)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(ss => ss.StudentId);

        modelBuilder.Entity<StudentSubject>()
            .HasOne(ss => ss.Subject)
            .WithMany(s => s.StudentSubjects)
            .HasForeignKey(ss => ss.SubjectId);

        modelBuilder.Entity<Mark>()
            .HasKey(m => m.Id);

        modelBuilder.Entity<Mark>()
            .HasOne(m => m.Student)
            .WithMany(s => s.Marks)
            .HasForeignKey(m => m.StudentId);

        modelBuilder.Entity<Mark>()
            .HasOne(m => m.Subject)
            .WithMany(s => s.Marks)
            .HasForeignKey(m => m.SubjectId);
    }
}

