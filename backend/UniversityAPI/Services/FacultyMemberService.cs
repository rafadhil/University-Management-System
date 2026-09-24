using Microsoft.EntityFrameworkCore;
using UniversityAPI.Data;
using UniversityAPI.Models;


public class FacultyMemberService
{
    private readonly UniversityDbContext _context;

    public FacultyMemberService(UniversityDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<FacultyMemberDto>> GetAllAsync(
        int pageSize = 50,
        int pageNumber = 1)
    {
        return await _context.FacultyMembers
            .AsNoTracking()
            .Select(f => new FacultyMemberDto
            {
                Id = f.Id,
                EmployeeId = f.EmployeeId,

                FullName = $"{f.FirstName} {f.SecondName} {f.ThirdName} {f.LastName}",

                NationalId = f.NationalId,
                Email = f.Email,
                PhoneNumber = f.PhoneNumber,

                AcademicRank = f.AcademicRank,
                Status = f.Status,

                DepartmentId = f.DepartmentId,
                UserId = f.UserId
            })
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
    }

    public async Task<FacultyMemberDto?> GetByIdAsync(int id)
    {
        return await _context.FacultyMembers
            .AsNoTracking()
            .Where(f => f.Id == id)
            .Select(f => new FacultyMemberDto
            {
                Id = f.Id,
                EmployeeId = f.EmployeeId,

                FullName = $"{f.FirstName} {f.SecondName} {f.ThirdName} {f.LastName}",

                NationalId = f.NationalId,
                Email = f.Email,
                PhoneNumber = f.PhoneNumber,

                AcademicRank = f.AcademicRank,
                Status = f.Status,

                DepartmentId = f.DepartmentId,
                UserId = f.UserId
            })
            .FirstOrDefaultAsync();
    }

    public async Task<FacultyMemberDto?> GetByEmployeeIdAsync(int employeeId)
    {
        return await _context.FacultyMembers
            .AsNoTracking()
            .Where(f => f.EmployeeId == employeeId)
            .Select(f => new FacultyMemberDto
            {
                Id = f.Id,
                EmployeeId = f.EmployeeId,

                FullName = $"{f.FirstName} {f.SecondName} {f.ThirdName} {f.LastName}",

                NationalId = f.NationalId,
                Email = f.Email,
                PhoneNumber = f.PhoneNumber,

                AcademicRank = f.AcademicRank,
                Status = f.Status,

                DepartmentId = f.DepartmentId,
                UserId = f.UserId
            })
            .FirstOrDefaultAsync();
    }

    public async Task<(FacultyMemberDto? FacultyMember, FacultyMemberResult Result)>
        CreateAsync(CreateFacultyMemberDto dto)
    {
        if (await _context.FacultyMembers
            .AnyAsync(f => f.Email == dto.Email))
        {
            return (null, FacultyMemberResult.EmailAlreadyExists);
        }

        if (await _context.FacultyMembers
            .AnyAsync(f => f.NationalId == dto.NationalId))
        {
            return (null, FacultyMemberResult.NationalIdAlreadyExists);
        }

        if (!await _context.Departments
            .AnyAsync(d => d.Id == dto.DepartmentId))
        {
            return (null, FacultyMemberResult.DepartmentNotFound);
        }

        if (dto.UserId.HasValue &&
            !await _context.Users
                .AnyAsync(u => u.Id == dto.UserId.Value))
        {
            return (null, FacultyMemberResult.UserNotFound);
        }

        var facultyMember = new FacultyMember
        {
            FirstName = dto.FirstName,
            SecondName = dto.SecondName,
            ThirdName = dto.ThirdName,
            LastName = dto.LastName,

            NationalId = dto.NationalId,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,

            AcademicRank = dto.AcademicRank,
            Status = dto.Status,

            DepartmentId = dto.DepartmentId,
            UserId = dto.UserId
        };

        _context.FacultyMembers.Add(facultyMember);

        await _context.SaveChangesAsync();

        var result = await GetByIdAsync(facultyMember.Id);

        return (result, FacultyMemberResult.Success);
    }

    public async Task<FacultyMemberResult> UpdateAsync(
        int id,
        AdminUpdateFacultyMemberDto dto)
    {
        var facultyMember = await _context.FacultyMembers
            .FirstOrDefaultAsync(f => f.Id == id);

        if (facultyMember == null)
        {
            return FacultyMemberResult.FacultyMemberNotFound;
        }

        if (dto.Email != null &&
            await _context.FacultyMembers.AnyAsync(
                f => f.Email == dto.Email && f.Id != id))
        {
            return FacultyMemberResult.EmailAlreadyExists;
        }

        if (dto.NationalId != null &&
            await _context.FacultyMembers.AnyAsync(
                f => f.NationalId == dto.NationalId && f.Id != id))
        {
            return FacultyMemberResult.NationalIdAlreadyExists;
        }

        if (dto.DepartmentId.HasValue &&
            !await _context.Departments
                .AnyAsync(d => d.Id == dto.DepartmentId.Value))
        {
            return FacultyMemberResult.DepartmentNotFound;
        }

        if (dto.UserId.HasValue &&
            !await _context.Users
                .AnyAsync(u => u.Id == dto.UserId.Value))
        {
            return FacultyMemberResult.UserNotFound;
        }

        if (dto.FirstName != null)
            facultyMember.FirstName = dto.FirstName;

        if (dto.SecondName != null)
            facultyMember.SecondName = dto.SecondName;

        if (dto.ThirdName != null)
            facultyMember.ThirdName = dto.ThirdName;

        if (dto.LastName != null)
            facultyMember.LastName = dto.LastName;

        if (dto.NationalId != null)
            facultyMember.NationalId = dto.NationalId;

        if (dto.Email != null)
            facultyMember.Email = dto.Email;

        if (dto.PhoneNumber != null)
            facultyMember.PhoneNumber = dto.PhoneNumber;

        if (dto.AcademicRank.HasValue)
            facultyMember.AcademicRank = dto.AcademicRank.Value;

        if (dto.Status.HasValue)
            facultyMember.Status = dto.Status.Value;

        if (dto.DepartmentId.HasValue)
            facultyMember.DepartmentId = dto.DepartmentId.Value;

        if (dto.UserId.HasValue)
            facultyMember.UserId = dto.UserId.Value;

        await _context.SaveChangesAsync();

        return FacultyMemberResult.Success;
    }

    public async Task<FacultyMemberResult> DeleteAsync(int id)
    {
        var facultyMember = await _context.FacultyMembers
            .FirstOrDefaultAsync(f => f.Id == id);

        if (facultyMember == null)
        {
            return FacultyMemberResult.FacultyMemberNotFound;
        }

        _context.FacultyMembers.Remove(facultyMember);

        await _context.SaveChangesAsync();

        return FacultyMemberResult.Success;
    }
}