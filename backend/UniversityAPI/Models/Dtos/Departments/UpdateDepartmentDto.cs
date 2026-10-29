using System.ComponentModel.DataAnnotations;

public class UpdateDepartmentDto
{
    [MaxLength(100)]
    public string? Name { get; set; }

    [MaxLength(20)]
    public string? Code { get; set; } = null!;

    public int? FacultyId { get; set; }

    [MaxLength(500)]
    public string? Description { get; set; }

    public int? DepartmentHeadId { get; set; }

    [EmailAddress]
    [MaxLength(255)]
    public string? Email { get; set; }

    public bool? IsActive { get; set; }
}