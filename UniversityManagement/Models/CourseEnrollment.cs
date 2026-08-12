using System;
using System.Collections.Generic;

namespace UniversityManagement.Models;

public partial class CourseEnrollment
{
    public int Id { get; set; }

    public int CourseOfferingId { get; set; }

    public int StudentId { get; set; }

    public decimal? FinalGrade { get; set; }

    public DateOnly EnrollmentDate { get; set; }

    public virtual CourseOffering CourseOffering { get; set; } = null!;

    public virtual Student Student { get; set; } = null!;
}
