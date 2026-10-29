public class FacultyMemberDto
{
    public int Id { get; set; }
    public int EmployeeId { get; set; }

    public string FullName { get; set; } = null!;

    public string NationalId { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public int FacultyId { get; set; }
    public string Faculty { get; set; } = null!;

    public AcademicRank AcademicRank { get; set; }
    public FacultyMemberStatus Status { get; set; }

    public int DepartmentId { get; set; }
    public string DepartmentName { get; set; } = null!;
    public int? UserId { get; set; }
}
