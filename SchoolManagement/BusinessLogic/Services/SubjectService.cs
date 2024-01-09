using SchoolManagement.BusinessLogic.Interfaces;
using SchoolManagement.Common.Entity;
using SchoolManagement.DAL.Repositories;
using SchoolManagement.Storage;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SchoolManagement.BusinessLogic.Services;

public class SubjectService : ISubjectService
{
    private readonly SubjectRepository _subjectRepository;

    public SubjectService(SubjectRepository subjectRepository)
    {
        _subjectRepository = subjectRepository;
    }

    public async Task AddSubject(Subject subject)
    {
        try
        {
            await _subjectRepository.CreateAsync(subject);
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteSubject(int subjectId)
    {
        try
        {
            await _subjectRepository.DeleteAsync(subjectId);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Subject>> GetAllSubjects()
    {
        try
        {
            return await _subjectRepository.GetAllAsync();
        }
        catch (Exception e)
        {
            throw new ApplicationException("An error occurred while fetching entities.", e);
        }
    }

    public async Task<Subject?> GetSubjectById(int subjectId)
    {
        try
        {
            return await _subjectRepository.GetByIdAsync(subjectId);
        }
        catch
        {
            throw;
        }
    }

    public async Task UpdateSubject(int id, Subject subject)
    {
        try
        {
            await _subjectRepository.UpdateAsync(id, subject);
        }
        catch
        {
            throw;
        }
    }
}
