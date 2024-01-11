using SchoolManagement.Common.Entity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.DAL.Interfaces;
public interface ISchoolRepository
{
    Task AddMarkForStudentSubjectAsync(int studentId, int subjectId, int value, DateTime date);
    Task<IEnumerable<Mark>> GetMarksBetweenDatesAsync(int studentId, int subjectId, DateTime startDate, DateTime endDate);
    Task LinkSubjectsToStudentAsync(int studentId, IEnumerable<int> subjectIds);
    Task UnlinkSubjectsFromStudentAsync(int studentId, int subjectId);
    Task<bool> UpdateMarkAsync(int markId, int newValue);
}