public class SemesterDto
{
    public int Id { get; set; }
    public SemesterTerm Term { get; set; }

    public DateOnly StartDate { get; set; }

    public DateOnly EndDate { get; set; }
}