using System;
using System.Collections.Generic;

namespace UniversityManagement.Models;

public partial class Student
{
    public int Id { get; set; }

    public int UniversityId { get; set; }

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string ThirdName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string NationalId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public StudentStatus Status { get; set; }

    public int MajorId { get; set; }

    public DateOnly AdmissionDate { get; set; }

    public int? UserId { get; set; }

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual Major Major { get; set; } = null!;

    public virtual User? User { get; set; }
}
