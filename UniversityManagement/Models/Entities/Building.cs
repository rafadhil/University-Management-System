using System;
using System.Collections.Generic;

namespace UniversityManagement.Models;

public partial class Building
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int NumberOfFloors { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Classroom> Classrooms { get; set; } = new List<Classroom>();
}
