using System;
using System.Collections.Generic;

namespace UniversityAPI.Models;

public partial class CourseOfferingSchedule
{
    public int Id { get; set; }

    public int CourseOfferingId { get; set; }

    public DayOfWeek DayOfWeek { get; set; }

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int ClassroomId { get; set; }

    public virtual Classroom Classroom { get; set; } = null!;

    public virtual CourseOffering CourseOffering { get; set; } = null!;
}
