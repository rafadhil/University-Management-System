using System.ComponentModel.DataAnnotations;

public class CreateUserDto
{
    [Required]
    public string Username { get; set; } = null!;

    [Required, MinLength(8)]
    public string Password { get; set; } = null!;

    [Required, EmailAddress, MaxLength(255)]
    public string Email { get; set; } = null!;
    public bool IsActive { get; set; } = true;
}