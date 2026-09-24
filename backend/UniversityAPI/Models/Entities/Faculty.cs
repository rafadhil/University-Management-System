using System;
using System.Collections.Generic;

namespace UniversityAPI.Models;

public partial class Faculty
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Code { get; set; } = null!;

    public string? Description { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();
}
