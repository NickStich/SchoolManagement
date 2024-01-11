using Microsoft.EntityFrameworkCore;
using SchoolManagement.Common.Entity;
using SchoolManagement.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SchoolManagement.DAL.Repositories;

public class SchoolRepository : ISchoolRepository
{
    private readonly SchoolDbContext _dbContext;

    public SchoolRepository(SchoolDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    #region StudentSubject

    public async Task LinkSubjectsToStudentAsync(int studentId, IEnumerable<int> subjectIds)
    {
        var student = await _dbContext.Students.FindAsync(studentId);

        var allSubjectsExist = subjectIds.All(id => _dbContext.Subjects.Any(s => s.Id == id));

        var subjects = _dbContext.Subjects.Where(subject => subjectIds.Contains(subject.Id)).ToList();

        if (student != null && allSubjectsExist)
        {
            var studentSubjectList = subjects.Select(subject =>
                new StudentSubject
                {
                    Student = student,
                    StudentId = studentId,
                    Subject = subject,
                    SubjectId = subject.Id
                })
                .ToList();

            _dbContext.StudentSubjects.AddRange(studentSubjectList);

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task UnlinkSubjectsFromStudentAsync(int studentId, int subjectId)
    {
        var student = await _dbContext.Students.FindAsync(studentId);

        if (student == null)
        {
            return;
        }

        var studentSubjectsToRemove = _dbContext.StudentSubjects
            .Where(ss => ss.StudentId == studentId && ss.SubjectId == subjectId)
            .ToList();

        if (studentSubjectsToRemove.Any())
        {
            _dbContext.StudentSubjects.RemoveRange(studentSubjectsToRemove);
            await _dbContext.SaveChangesAsync();
        }
    }

    #endregion

    #region Mark

    public async Task AddMarkForStudentSubjectAsync(int studentId, int subjectId, int value, DateTime date)
    {
        var student = await _dbContext.Students.FindAsync(studentId);
        var subject = await _dbContext.Subjects.FindAsync(subjectId);

        if (student != null && subject != null)
        {
            var mark = new Mark { Value = value, Date = date, Student = student, Subject = subject };
            _dbContext.Marks.AddRange(mark);

            await _dbContext.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Mark>> GetMarksBetweenDatesAsync(int studentId, int subjectId, DateTime startDate, DateTime endDate)
    {
        return await _dbContext.Marks
            .Where(mark =>
                mark.StudentId == studentId &&
                mark.SubjectId == subjectId &&
                mark.Date >= startDate && mark.Date <= endDate)
            .ToListAsync();
    }

    public async Task<bool> UpdateMarkAsync(int markId, int newValue)
    {
        var mark = await _dbContext.Marks.FindAsync(markId);

        if (mark == null)
        {
            return false;
        }

        mark.Value = newValue;
        mark.Date = DateTime.Now;

        await _dbContext.SaveChangesAsync();

        return true;
    }

    #endregion
}
