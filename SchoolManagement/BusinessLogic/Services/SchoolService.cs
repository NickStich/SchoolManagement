using SchoolManagement.DAL.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using SchoolManagement.Common.Entity;
using SchoolManagement.BusinessLogic.Interfaces;
using SchoolManagement.DAL.Interfaces;

namespace SchoolManagement.BusinessLogic.Services;

public class SchoolService : ISchoolService
{
    private readonly ISchoolRepository _schoolRepository;

    public SchoolService(ISchoolRepository repository)
    {
        _schoolRepository = repository;
    }

    public async Task LinkSubjectsToStudentAsync(int studentId, IEnumerable<int> subjectIds)
    {
        try
        {
            await _schoolRepository.LinkSubjectsToStudentAsync(studentId, subjectIds);
        }
        catch
        {
            throw;
        }
    }

    public async Task UnlinkSubjectsFromStudentAsync(int studentId, int subjectId)
    {
        try
        {
            await _schoolRepository.UnlinkSubjectsFromStudentAsync(studentId, subjectId);
        }
        catch
        {
            throw;
        }
    }

    public async Task AddMarkForStudentSubjectAsync(int studentId, int subjectId, int value, DateTime date)
    {
        try
        {
            await _schoolRepository.AddMarkForStudentSubjectAsync(studentId, subjectId, value, date);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Mark>> GetMarksBetweenDatesAsync(int studentId, int subjectId, DateTime startDate, DateTime endDate)
    {
        try
        {
            return await _schoolRepository.GetMarksBetweenDatesAsync(studentId, subjectId, startDate, endDate);
        }
        catch
        {
            throw;
        }
    }

    public async Task<bool> UpdateMarkAsync(int markId, int newValue)
    {
        try
        {
            return await _schoolRepository.UpdateMarkAsync(markId, newValue);
        }
        catch
        {
            throw;
        }
    }
}

