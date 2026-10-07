using System;
using System.Collections.Generic;

namespace UniversityAPI.Models;

public class CreateUserDto
{
    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsActive { get; set; }
}
