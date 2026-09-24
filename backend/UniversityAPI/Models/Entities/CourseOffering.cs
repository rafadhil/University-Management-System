using System;
using System.Collections.Generic;

namespace UniversityAPI.Models;

public partial class CourseOffering
{
    public int Id { get; set; }

    public int CourseId { get; set; }

    public int SemesterId { get; set; }

    public string Section { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual Course Course { get; set; } = null!;

    public virtual ICollection<CourseEnrollment> CourseEnrollments { get; set; } = new List<CourseEnrollment>();

    public virtual ICollection<CourseOfferingInstructor> CourseOfferingInstructors { get; set; } = new List<CourseOfferingInstructor>();

    public virtual ICollection<CourseOfferingSchedule> CourseOfferingSchedules { get; set; } = new List<CourseOfferingSchedule>();

    public virtual Semester Semester { get; set; } = null!;
}
