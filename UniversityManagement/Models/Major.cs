using System;
using System.Collections.Generic;

namespace UniversityManagement.Models;

public partial class Major
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public int DepartmentId { get; set; }

    public string? Description { get; set; }

    public int TotalRquiredCreditHours { get; set; }

    public bool IsActive { get; set; }

    public string DegreeType { get; set; } = null!;

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();
}
