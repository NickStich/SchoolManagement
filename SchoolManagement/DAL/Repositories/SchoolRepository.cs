using Microsoft.EntityFrameworkCore;
using SchoolManagement.Common.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.DAL.Repositories;

public class SchoolRepository
{
    private readonly SchoolDbContext _dbContext;

    public SchoolRepository(SchoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //public async Task LinkSubjectsToStudentAsync(int studentId, List<int> subjectIds)
    //{
    //    var student = await _dbContext.Students.FindAsync(studentId);

    //    if (student != null && subjectIds.Any())
    //    {
    //        student.Subjects ??= new List<Subject>();

    //        var subjectsToAdd = await _dbContext.Subjects.Where(s => subjectIds.Contains(s.Id)).ToListAsync();

    //        foreach (var subject in subjectsToAdd)
    //        {
    //            student.Subjects.Add(subject);
    //        }

    //        await _dbContext.SaveChangesAsync();
    //    }
    //}

    #region Mark

    //public async Task AddMarkForStudentSubjectAsync(int studentId, int subjectId, int value, DateTime date)
    //{
    //    var studentSubject = await _dbContext.Students
    //        .Include(s => s.Subjects)
    //        .Include(ss => ss.Marks)
    //        .FirstOrDefaultAsync(ss => ss.StudentId == studentId && s.Id == subjectId);

    //    if (studentSubject != null)
    //    {
    //        var mark = new Mark { Value = value, Date = date };
    //        studentSubject.Marks ??= new List<Mark>();
    //        studentSubject.Marks.Add(mark);

    //        await _dbContext.SaveChangesAsync();
    //    }
    //}

    //public async Task<IEnumerable<Mark>> GetMarksBetweenDatesAsync(int studentId, int subjectId, DateTime startDate, DateTime endDate)
    //{
    //    return await _dbContext.Marks
    //        .Where(mark =>
    //            mark.StudentSubject.StudentId == studentId &&
    //            mark.StudentSubject.SubjectId == subjectId &&
    //            mark.Date >= startDate && mark.Date <= endDate)
    //        .ToListAsync();
    //}

    public async Task<bool> UpdateMarkAsync(int markId, int newValue, DateTime newDate)
    {
        var mark = await _dbContext.Marks.FindAsync(markId);

        if (mark == null)
        {
            return false; 
        }

        mark.Value = newValue;
        mark.Date = newDate;

        await _dbContext.SaveChangesAsync();

        return true;
    }

    #endregion
}
