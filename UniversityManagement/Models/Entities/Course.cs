using System;
using System.Collections.Generic;

namespace UniversityManagement.Models;

public partial class Course
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public int CreditHours { get; set; }

    public int DepartmentId { get; set; }

    public CourseType Type { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<CourseOffering> CourseOfferings { get; set; } = new List<CourseOffering>();

    public virtual ICollection<CoursePrerequisite> CoursePrerequisiteCourses { get; set; } = new List<CoursePrerequisite>();

    public virtual ICollection<CoursePrerequisite> CoursePrerequisitePrerequisiteCourses { get; set; } = new List<CoursePrerequisite>();

    public virtual Department Department { get; set; } = null!;
}
