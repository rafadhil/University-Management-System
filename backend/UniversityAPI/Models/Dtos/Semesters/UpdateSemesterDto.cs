public class UpdateSemesterDto
{
    public SemesterTerm? Term { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }
}