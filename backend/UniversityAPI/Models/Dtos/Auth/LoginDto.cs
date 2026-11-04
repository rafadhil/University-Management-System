using System.ComponentModel.DataAnnotations;

public class LoginDto
{
    [Required, MaxLength(255)]
    public string UsernameOrEmail { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}