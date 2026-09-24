using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Models;

public class AdminUpdateStudentDto
{

    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? SecondName { get; set; }

    [MaxLength(100)]
    public string? ThirdName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [MaxLength(20)]
    public string? NationalId { get; set; }

    [EmailAddress, MaxLength(255)]
    public string? Email { get; set; }

    [MaxLength(30)]
    public string? PhoneNumber { get; set; }

    public StudentStatus? Status { get; set; }

    [Range(1, int.MaxValue)]
    public int? MajorId { get; set; }

}