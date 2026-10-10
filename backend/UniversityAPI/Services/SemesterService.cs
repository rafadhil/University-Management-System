using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using UniversityAPI.Data;
using UniversityAPI.Models;

public class SemesterService
{
    private UniversityDbContext _context { get; set; }

    public SemesterService(UniversityDbContext context)
        => _context = context;

    // private async Task<bool> _HasOverlappingPeriod()
    // {

    // }
    public async Task<(SemeseterResult, Semester?)> CreateAsync(CreateSemesterDto dto)
    {
        if (dto.StartDate > dto.EndDate)
        {
            return (SemeseterResult.StartDateAfterEndDate, null);
        }

        bool hasOverlappingSemester =
            await _context.Semesters
                .AnyAsync(s =>
                s.StartDate <= dto.EndDate && dto.StartDate <= s.EndDate);

        if (hasOverlappingSemester)
        {
            return (SemeseterResult.HasOverlappingSemester, null);
        }

        Semester newSemester = new Semester
        {
            StartDate = dto.StartDate,
            EndDate = dto.EndDate,
            Term = dto.Term
        };

        _context.Semesters.Add(newSemester);
        await _context.SaveChangesAsync();

        return (SemeseterResult.Success, newSemester);
    }

    public async Task<(SemeseterResult, Semester?)> GetById(int semesterId)
    {
        var semester = await _context.Semesters.FindAsync(semesterId);

        if (semester == null)
        {
            return (SemeseterResult.SemesterNotFound, null);
        }


        return (SemeseterResult.Success, semester);
    }
    public async Task<(SemeseterResult, Semester?)> GetByTerm(SemesterTerm term, int year)
    {
        var semester = await _context.Semesters
            .FirstOrDefaultAsync(s => s.Term == term && s.StartDate.Year == year);

        if (semester == null)
        {
            return (SemeseterResult.SemesterNotFound, null);
        }


        return (SemeseterResult.Success, semester);
    }

    // public async Task<SemeseterResult> Update(int semesterId, UpdateSemesterDto dto)
    // {
    //     var semester = await _context.Semesters.FindAsync(semesterId);

    //     if (semester == null)
    //         return SemeseterResult.SemesterNotFound;

    //     if (dto.Term.HasValue)
    //     {
    //         semester.Term = dto.Term.Value;
    //     }

    //     if (dto.StartDate.HasValue)
    //     {
    //         semester.StartDate = dto.StartDate.Value;
    //     }

    //     if (dto)
    // }


    public async Task<SemeseterResult> DeleteAsync(int semesterId)
    {
        var semester = await _context.Semesters
            .FindAsync(semesterId);

        if (semester == null)
        {
            return SemeseterResult.SemesterNotFound;
        }

        _context.Semesters.Remove(semester);
        await _context.SaveChangesAsync();
        return SemeseterResult.Success;
    }
}