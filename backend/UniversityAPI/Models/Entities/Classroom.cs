using System;
using System.Collections.Generic;

namespace UniversityAPI.Models;

public partial class Classroom
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public int BuildingId { get; set; }

    public ClassroomType Type { get; set; }

    public int FloorNumber { get; set; }

    public int Capacity { get; set; }

    public virtual Building Building { get; set; } = null!;

    public virtual ICollection<CourseOfferingSchedule> CourseOfferingSchedules { get; set; } = new List<CourseOfferingSchedule>();
}
