using System;
using System.Collections.Generic;

namespace UniversityAPI.Models;

public partial class CourseOfferingInstructor
{
    public int Id { get; set; }

    public int CourseOfferingId { get; set; }

    public int InstructorId { get; set; }

    public virtual CourseOffering CourseOffering { get; set; } = null!;

    public virtual FacultyMember Instructor { get; set; } = null!;
}
