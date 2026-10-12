
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

        bool codeExists = await _context.Departments
            .AnyAsync(d => d.Code == department.Code);

        if (codeExists)
            return (DepartmentResult.DepartmentWithCodeAlreadyExists, null);

        _context.Departments.Add(department);
        await _context.SaveChangesAsync();

        return (DepartmentResult.Success, department.Id);
    }

    public async Task<Department?> UpdateAsync(
        int id,
        Department updatedDepartment)
    {
        var department = await _context.Departments
            .FindAsync(id);

        if (department is null)
            return null;

        updatedDepartment.Name = updatedDepartment.Name.Trim();
        updatedDepartment.Code =
            updatedDepartment.Code.Trim().ToUpperInvariant();

        bool codeExists = await _context.Departments
            .AnyAsync(d =>
                d.Code == updatedDepartment.Code &&
                d.Id != id);

        if (codeExists)
            return null;

        department.Name = updatedDepartment.Name;
        department.Code = updatedDepartment.Code;
        department.FacultyId = updatedDepartment.FacultyId;
        department.Description = updatedDepartment.Description?.Trim();
        department.DepartmentHeadId = updatedDepartment.DepartmentHeadId;
        department.Email = updatedDepartment.Email?.Trim();
        department.IsActive = updatedDepartment.IsActive;

        await _context.SaveChangesAsync();

        return department;
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
