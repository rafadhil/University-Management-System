namespace UniversityAPI.DTOs;

public class DepartmentResponseDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Code { get; set; } = string.Empty;

    public int FacultyId { get; set; }

    public string? FacultyName { get; set; }

    public string? Description { get; set; }

    public int? DepartmentHeadId { get; set; }

    public string? DepartmentHeadName { get; set; }

    public string? Email { get; set; }

    public bool IsActive { get; set; }
}
