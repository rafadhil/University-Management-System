public class AdminUpdateFacultyMemberDto
{
    public string? FirstName { get; set; }
    public string? SecondName { get; set; }
    public string? ThirdName { get; set; }
    public string? LastName { get; set; }

    public string? NationalId { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }

    public AcademicRank? AcademicRank { get; set; }
    public FacultyMemberStatus? Status { get; set; }

    public int? DepartmentId { get; set; }
    public int? UserId { get; set; }
}
