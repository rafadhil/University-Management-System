using System;
using System.Collections.Generic;

namespace UniversityAPI.Models;

public partial class Department
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int FacultyId { get; set; }

    public string? Description { get; set; }

    public int? DepartmentHeadId { get; set; }

    public string? Email { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Course> Courses { get; set; } = new List<Course>();

    public virtual FacultyMember? DepartmentHead { get; set; }

    public virtual Faculty Faculty { get; set; } = null!;

    public virtual ICollection<FacultyMember> FacultyMembers { get; set; } = new List<FacultyMember>();

    public virtual ICollection<Major> Majors { get; set; } = new List<Major>();
}
