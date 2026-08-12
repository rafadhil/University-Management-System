using System;
using System.Collections.Generic;

namespace UniversityManagement.Models;

public partial class CourseOfferingSchedule
{
    public int Id { get; set; }

    public int CourseOfferingId { get; set; }

    public string DayOfWeek { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public int ClassroomId { get; set; }

    public virtual Classroom Classroom { get; set; } = null!;

    public virtual CourseOffering CourseOffering { get; set; } = null!;
}
