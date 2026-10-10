using System.ComponentModel.DataAnnotations;

public class CreateSemesterDto
{
    [Required]
    public SemesterTerm Term { get; set; }

    [Required]
    public DateOnly StartDate { get; set; }

    [Required]
    public DateOnly EndDate { get; set; }
}