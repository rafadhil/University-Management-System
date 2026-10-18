public class UpdateDepartmentDto
{
    public string? Name { get; set; }

    public string? Code { get; set; } = null!;

    public int? FacultyId { get; set; }

    public string? Description { get; set; }

    public int? DepartmentHeadId { get; set; }

    public string? Email { get; set; }

    public bool? IsActive { get; set; }
}