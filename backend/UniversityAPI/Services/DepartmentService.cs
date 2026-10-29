using Microsoft.EntityFrameworkCore;
using UniversityAPI.Data;
using UniversityAPI.Models;

public class DepartmentService
{
    private readonly UniversityDbContext _context;

    public DepartmentService(UniversityDbContext context)
        => _context = context;

    public async Task<List<Department>> GetAllAsync(int? facultyId = null)
    {
        return await _context.Departments
            .AsNoTracking()
            .Include(d => d.Faculty)
            .Include(d => d.DepartmentHead)
            .Where(d => facultyId == null
                || d.FacultyId == facultyId)
            .ToListAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _context.Departments
            .Include(d => d.Faculty)
            .Include(d => d.DepartmentHead)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<Department?> GetByCodeAsync(string code)
    {
        code = code.Trim().ToUpperInvariant();

        return await _context.Departments
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Code == code);
    }

    public async Task<(DepartmentResult, int?)> CreateAsync(Department department)
    {
        department.Name = department.Name.Trim();
        department.Code = department.Code.Trim().ToUpperInvariant();
        department.Email = string.IsNullOrWhiteSpace(department.Email)
            ? null
            : department.Email.Trim().ToLowerInvariant();

        bool codeExists = await _context.Departments
            .AnyAsync(d => d.Code == department.Code);

        if (codeExists)
            return (DepartmentResult.DepartmentWithCodeAlreadyExists, null);

        if (!string.IsNullOrWhiteSpace(department.Email))
        {
            bool emailExists = await _context.Departments
                .AnyAsync(d => d.Email == department.Email);

            if (emailExists)
                return (DepartmentResult.DepartmentWithEmailAlreadyExists, null);
        }


        bool facultyExists = await _context.Faculties
            .AnyAsync(f => f.Id == department.FacultyId);

        if (!facultyExists)
            return (DepartmentResult.FacultyNotFound, null);

        if (department.DepartmentHeadId.HasValue)
        {
            int departmentHeadId = department.DepartmentHeadId.Value;

            bool departmentHeadExists = await _context.FacultyMembers
                .AnyAsync(m => m.Id == departmentHeadId);

            if (!departmentHeadExists)
                return (DepartmentResult.FacultyMemberNotFound, null);

            bool departmentHeadBelongsToFaculty =
                await _context.FacultyMembers.AnyAsync(fm =>
                    fm.Id == departmentHeadId &&
                    fm.Department.FacultyId == department.FacultyId);

            if (!departmentHeadBelongsToFaculty)
                return (DepartmentResult.FacultyMemberBelongsToAnotherFaculty, null);
        }


        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return (DepartmentResult.Success, department.Id);
    }

    public async Task<DepartmentResult> UpdateAsync(
        int departmentId,
        UpdateDepartmentDto updatedDepartment)
    {
        var department = await _context.Departments
            .FindAsync(departmentId);

        if (department == null)
            return DepartmentResult.DepartmentNotFound;

        updatedDepartment.Code =
            updatedDepartment.Code?.Trim().ToUpperInvariant();

        updatedDepartment.Name =
            updatedDepartment.Name?.Trim();

        updatedDepartment.Email =
        updatedDepartment.Email?.Trim();

        updatedDepartment.Description =
            updatedDepartment.Description?.Trim();

        int targetFacultyId =
            updatedDepartment.FacultyId ?? department.FacultyId;

        if (!string.IsNullOrWhiteSpace(updatedDepartment.Code))
        {
            bool codeExists = await _context.Departments
                .AnyAsync(d =>
                    d.Code == updatedDepartment.Code &&
                    d.Id != departmentId);

            if (codeExists)
                return DepartmentResult
                    .DepartmentWithCodeAlreadyExists;
        }

        if (!string.IsNullOrWhiteSpace(updatedDepartment.Email))
        {
            bool emailExists = await _context.Departments
                .AnyAsync(d =>
                    d.Email == updatedDepartment.Email &&
                    d.Id != departmentId);

            if (emailExists)
                return DepartmentResult
                    .DepartmentWithEmailAlreadyExists;
        }

        if (updatedDepartment.FacultyId.HasValue)
        {
            bool facultyExists = await _context.Faculties
                .AnyAsync(f => f.Id == targetFacultyId);

            if (!facultyExists)
                return DepartmentResult.FacultyNotFound;
        }

        if (updatedDepartment.DepartmentHeadId.HasValue)
        {
            int departmentHeadId =
                updatedDepartment.DepartmentHeadId.Value;

            bool facultyMemberExists = await _context.FacultyMembers
                .AnyAsync(fm => fm.Id == departmentHeadId);

            if (!facultyMemberExists)
                return DepartmentResult.FacultyMemberNotFound;

            bool facultyMemberBelongsToFaculty =
                await _context.FacultyMembers
                    .AnyAsync(fm =>
                        fm.Id == departmentHeadId &&
                        fm.Department.FacultyId == targetFacultyId);

            if (!facultyMemberBelongsToFaculty)
                return DepartmentResult
                    .FacultyMemberBelongsToAnotherFaculty;
        }

        if (!string.IsNullOrWhiteSpace(updatedDepartment.Name))
            department.Name = updatedDepartment.Name;

        if (!string.IsNullOrWhiteSpace(updatedDepartment.Code))
            department.Code = updatedDepartment.Code;

        if (updatedDepartment.FacultyId.HasValue)
            department.FacultyId = targetFacultyId;

        if (!string.IsNullOrWhiteSpace(updatedDepartment.Description))
            department.Description = updatedDepartment.Description;

        if (updatedDepartment.Email is not null)
            department.Email = string.IsNullOrWhiteSpace(updatedDepartment.Email)
                ? null
                : updatedDepartment.Email;

        if (updatedDepartment.DepartmentHeadId.HasValue)
            department.DepartmentHeadId =
                updatedDepartment.DepartmentHeadId.Value;

        if (updatedDepartment.IsActive.HasValue)
            department.IsActive = updatedDepartment.IsActive.Value;

        await _context.SaveChangesAsync();

        return DepartmentResult.Success;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var department = await _context.Departments
            .FindAsync(id);

        if (department is null)
            return false;

        _context.Departments.Remove(department);
        await _context.SaveChangesAsync();

        return true;
    }
}
