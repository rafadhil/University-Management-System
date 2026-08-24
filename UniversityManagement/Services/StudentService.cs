using Azure;
using Microsoft.EntityFrameworkCore;
using UniversityManagement.Data;
using UniversityManagement.Models;

public class StudentService
{
    private UniversityDbContext _context { get; set; }
    public StudentService(UniversityDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<StudentDto>> GetAll(int pageSize, int pageNumber)
    {
        return await _context.Students
            .OrderBy(s => s.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(s => new StudentDto
            {
                FullName = $"{s.FirstName} {s.SecondName} {s.ThirdName} {s.LastName}",
                Id = s.Id,
                UniversityId = s.UniversityId,
                NationalId = s.NationalId,
                Email = s.Email,
                Major = s.Major.Name,
                AdmissionDate = s.AdmissionDate,
                DateOfBirth = s.DateOfBirth,
                PhoneNumber = s.PhoneNumber,
                Status = s.Status,
                UserId = s.UserId
            })
            .ToListAsync();
    }

    public async Task<Student?> GetByIdAsync(int id)
    {
        return await _context.Students
            .Include(s => s.Major)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task<Student?> GetByUniversityIdAsync(int id)
    {
        return await _context.Students
            .Include(s => s.Major)
            .FirstOrDefaultAsync(s => s.UniversityId == id);
    }

    public async Task<Student?> GetByNationalIdAsync(string id)
    {
        return await _context.Students
            .Include(s => s.Major)
            .FirstOrDefaultAsync(s => s.NationalId == id);
    }

    public async Task<(Student? Student, string? Error)> CreateStudentAsync(CreateStudentDto dto)
    {
        var existingStudent = await _context.Students
            .FirstOrDefaultAsync(s =>
                s.Email == dto.Email ||
                s.NationalId == dto.NationalId ||
                s.PhoneNumber == dto.PhoneNumber);

        if (existingStudent != null)
        {
            if (existingStudent.Email == dto.Email)
                return (null, "Student with Email already exists.");

            if (existingStudent.NationalId == dto.NationalId)
                return (null, "Student with National ID already exists.");

            if (existingStudent.PhoneNumber == dto.PhoneNumber)
                return (null, "Student with Phone number already exists.");
        }

        var major = await _context.Majors
        .FirstOrDefaultAsync(m => m.Id == dto.MajorId);

        if (major == null)
            return (null, "Major with provided Id does not exist.");

        var student = new Student
        {
            FirstName = dto.FirstName,
            SecondName = dto.SecondName,
            ThirdName = dto.ThirdName,
            LastName = dto.LastName,
            AdmissionDate = dto.AdmissionDate,
            DateOfBirth = dto.DateOfBirth,
            NationalId = dto.NationalId,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            MajorId = dto.MajorId,
            Major = major,
            UserId = dto.UserId,
            Status = dto.Status
        };

        await _context.Students.AddAsync(student);
        await _context.SaveChangesAsync();

        return (student, null);
    }

    public async Task<StudentResult> UpdateStudentAsync(int studentId, AdminUpdateStudentDto dto)
    {

        var student = await _context.Students
            .FindAsync(studentId);

        if (student == null)
        {
            return StudentResult.StudentNotFound;
        }

        if (dto.FirstName != null)
        {
            student.FirstName = dto.FirstName;
        }

        if (dto.SecondName != null)
        {
            student.SecondName = dto.SecondName;
        }

        if (dto.ThirdName != null)
        {
            student.ThirdName = dto.ThirdName;
        }

        if (dto.LastName != null)
        {
            student.LastName = dto.LastName;
        }

        if (dto.DateOfBirth != null)
        {
            student.DateOfBirth = dto.DateOfBirth.Value;
        }

        if (dto.NationalId != null)
        {
            var studentWithNationalIdExist = await _context.Students
                .AnyAsync(s => s.NationalId == dto.NationalId
                    && s.Id != studentId);

            if (studentWithNationalIdExist)
            {
                return StudentResult.NationalIdAlreadyExists;
            }
            student.NationalId = dto.NationalId;
        }

        if (dto.Email != null)
        {
            var existingStudentWithEmail = await _context.Students
                .AnyAsync(s => s.Email == dto.Email
                    && s.Id != studentId);

            if (existingStudentWithEmail)
            {
                return StudentResult.EmailAlreadyExists;
            }
            student.Email = dto.Email;
        }

        if (dto.PhoneNumber != null)
        {
            student.PhoneNumber = dto.PhoneNumber;
        }

        if (dto.Status != null)
        {
            student.Status = dto.Status.Value;
        }

        if (dto.MajorId != null)
        {
            bool majorExists = await _context.Majors.AnyAsync(m => m.Id == dto.MajorId);
            if (!majorExists)
            {
                return StudentResult.MajorNotFound;
            }

            student.MajorId = dto.MajorId.Value;
        }

        await _context.SaveChangesAsync();
        return StudentResult.Success;
    }

    public async Task<StudentResult> DeleteAsync(int id)
    {
        var student = await _context.Students.FindAsync(id);
        if (student == null)
        {
            return StudentResult.StudentNotFound;
        }

        _context.Students.Remove(student);
        await _context.SaveChangesAsync();

        return StudentResult.Success;
    }

}