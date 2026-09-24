using System;
using System.Collections.Generic;

namespace UniversityAPI.Models;

public partial class Semester
{
    public int Id { get; set; }

    public string Term { get; set; } = null!;

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }

    public virtual ICollection<CourseOffering> CourseOfferings { get; set; } = new List<CourseOffering>();
}
