using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using UniversityAPI.Data;
using UniversityAPI.Models;

public class SemesterService
{
    private readonly UniversityDbContext _context;

    public SemesterService(UniversityDbContext context)
        => _context = context;

    private async Task<bool> HasOverlappingSemester(DateOnly startDate,
        DateOnly endDate,
        int? excludeSemesterId = null)
    {
        return await _context.Semesters
                 .AnyAsync(s =>
                 s.StartDate <= endDate &&
                 startDate <= s.EndDate &&
                 (excludeSemesterId == null || s.Id != excludeSemesterId)
                 );

    }

    public async Task<IEnumerable<SemesterDto>> GetAllAsync(int pageSize, int pageNumber)
    {
        return await _context.Semesters
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new SemesterDto
            {
                Id = s.Id,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Term = s.Term
            })
            .ToListAsync();
    }

    public async Task<(SemeseterResult, Semester?)> CreateAsync(CreateSemesterDto dto)
    {
        if (dto.StartDate > dto.EndDate)
        {
            return (SemeseterResult.StartDateAfterEndDate, null);
        }

        bool hasOverlappingSemester =
            await HasOverlappingSemester(dto.StartDate, dto.EndDate);

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

    public async Task<Semester?> GetByIdAsync(int semesterId)
    {
        var semester = await _context.Semesters.FindAsync(semesterId);

        if (semester == null)
        {
            return null;
        }


        return semester;
    }
    public async Task<Semester?> GetByTermAsync(SemesterTerm term, int year)
    {
        var semester = await _context.Semesters
            .FirstOrDefaultAsync(s => s.Term == term && s.StartDate.Year == year);

        if (semester == null)
        {
            return null;
        }


        return semester;
    }

    public async Task<SemeseterResult> UpdateAsync(int semesterId, UpdateSemesterDto dto)
    {
        var semester = await _context.Semesters.FindAsync(semesterId);

        if (semester == null)
            return SemeseterResult.SemesterNotFound;

        var startDate = dto.StartDate ?? semester.StartDate;
        var endDate = dto.EndDate ?? semester.EndDate;

        if (startDate > endDate)
            return SemeseterResult.StartDateAfterEndDate;

        bool hasOverlappingSemester = await HasOverlappingSemester(
            startDate,
            endDate,
            excludeSemesterId: semesterId);

        if (hasOverlappingSemester)
            return SemeseterResult.HasOverlappingSemester;

        if (dto.Term.HasValue)
        {
            semester.Term = dto.Term.Value;
        }

        semester.StartDate = startDate;
        semester.EndDate = endDate;

        await _context.SaveChangesAsync();
        return SemeseterResult.Success;
    }


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

    public async Task<Semester?> GetActiveSemesterAsync()
    {
        var todaysDate = DateOnly.FromDateTime(DateTime.Now);

        return await _context.Semesters
            .FirstOrDefaultAsync(s =>
                s.StartDate <= todaysDate
                && s.EndDate >= todaysDate);
    }
}