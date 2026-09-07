using System.ComponentModel.DataAnnotations;

public class CreateFacultyMemberDto
{
    [Required, MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string SecondName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string ThirdName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = null!;

    [Required, MaxLength(20)]
    public string NationalId { get; set; } = null!;

    [Required, EmailAddress, MaxLength(255)]
    public string Email { get; set; } = null!;

    [Required, MaxLength(30)]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public AcademicRank AcademicRank { get; set; }

    [Required]
    public FacultyMemberStatus Status { get; set; } = FacultyMemberStatus.Active;

    [Required, Range(1, int.MaxValue)]
    public int DepartmentId { get; set; }

    public int? UserId { get; set; }
}
