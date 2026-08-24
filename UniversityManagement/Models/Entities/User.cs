using System;
using System.Collections.Generic;

namespace UniversityManagement.Models;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<FacultyMember> FacultyMembers { get; set; } = new List<FacultyMember>();

    public virtual ICollection<Student> Students { get; set; } = new List<Student>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
