using System;
using System.Collections.Generic;

namespace UniversityAPI.Models;

public partial class FacultyMember
{
    public int Id { get; set; }

    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string SecondName { get; set; } = null!;

    public string ThirdName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string NationalId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public FacultyMemberStatus Status { get; set; }

    public int DepartmentId { get; set; }

    public AcademicRank AcademicRank { get; set; }

    public int? UserId { get; set; }

    public DateOnly HireDate { get; set; }

    public virtual ICollection<CourseOfferingInstructor> CourseOfferingInstructors { get; set; } = new List<CourseOfferingInstructor>();

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual User? User { get; set; }
}
