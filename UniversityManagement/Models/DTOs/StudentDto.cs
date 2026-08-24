
using System.ComponentModel.DataAnnotations;

public class StudentDto
{
    public int Id { get; set; }
    public int UniversityId { get; set; }
    public string FullName { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
    public string NationalId { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public StudentStatus Status { get; set; } = StudentStatus.Active;
    public string Major { get; set; } = null!;
    public DateOnly AdmissionDate { get; set; } = DateOnly.FromDateTime(DateTime.Now);
    public int? UserId { get; set; }
}