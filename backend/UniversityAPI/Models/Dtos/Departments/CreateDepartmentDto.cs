using System.ComponentModel.DataAnnotations;

namespace UniversityAPI.DTOs;

public class CreateDepartmentDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)]
    public string Code { get; set; } = string.Empty;

    [Required]
    public int FacultyId { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public int? DepartmentHeadId { get; set; }

    [EmailAddress]
    [MaxLength(255)]
    public string? Email { get; set; }

    public bool IsActive { get; set; } = true;
}
