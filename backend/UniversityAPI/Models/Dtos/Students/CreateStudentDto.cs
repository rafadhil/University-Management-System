using System.ComponentModel.DataAnnotations;

public class CreateStudentDto
{
    [Required, MaxLength(100)]
    public string FirstName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string SecondName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string ThirdName { get; set; } = null!;

    [Required, MaxLength(100)]
    public string LastName { get; set; } = null!;

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required, MaxLength(20)]
    public string NationalId { get; set; } = null!;

    [Required, EmailAddress, MaxLength(255)]
    public string Email { get; set; } = null!;

    [Required, MaxLength(30)]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    public StudentStatus Status { get; set; } = StudentStatus.Active;

    [Required, Range(1, int.MaxValue)]
    public int MajorId { get; set; }

    [Required]
    public DateOnly AdmissionDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);

    public int? UserId { get; set; }
}