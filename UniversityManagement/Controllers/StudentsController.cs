using Microsoft.AspNetCore.Mvc;
using UniversityManagement.Models;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly StudentService _studentService;

    public StudentsController(StudentService studentService)
    {
        _studentService = studentService;
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<StudentDto>>> GetAll(int pageSize = 50, int pageNumber = 1)
    {
        if (pageSize < 1 || pageNumber < 1)
        {
            return BadRequest("Page size and page number must be greater than 0.");
        }

        return Ok(await _studentService.GetAll(pageSize, pageNumber));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<StudentDto>> GetStudentById(int id)
    {
        var student = await _studentService.GetByIdAsync(id);

        if (student == null)
            return NotFound();

        return Ok(student.ToDto());
    }

    [HttpGet]
    public async Task<ActionResult<StudentDto>> GetStudent(
    int? universityId,
    string? nationalId)
    {
        Student? student;

        if (universityId.HasValue)
        {
            student = await _studentService.GetByUniversityIdAsync(universityId.Value);
        }
        else if (nationalId != null)
        {
            student = await _studentService.GetByNationalIdAsync(nationalId);
        }
        else
        {
            return BadRequest("University ID or National ID is required.");
        }

        if (student == null)
            return NotFound();

        return Ok(student.ToDto());
    }

    /// <summary>
    /// Creates a new student.
    /// </summary>
    /// <param name="dto">
    /// Student data. Status must be one of: Active, Suspended, Graduated, Withdrawn.
    /// </param>
    [HttpPost]
    public async Task<ActionResult<StudentDto>> CreateStudent(CreateStudentDto dto)
    {
        var (student, result) = await _studentService.CreateStudentAsync(dto);

        if (result == StudentResult.EmailAlreadyExists)
            return Conflict(new { message = "Student with provided Email already exists." });

        if (result == StudentResult.MajorNotFound)
            return Conflict(new { message = "Major with provided Id does not exist." });

        if (result == StudentResult.NationalIdAlreadyExists)
            return Conflict(new { message = "Student with provided National Id already exists." });

        if (result == StudentResult.Success)
        {
            return CreatedAtAction(
                nameof(GetStudentById),
                new { id = student!.Id },
                student.ToDto());
        }

        return Problem();
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateStudent(int id, AdminUpdateStudentDto dto)
    {
        var result = await _studentService.UpdateStudentAsync(id, dto);

        if (result == StudentResult.Success)
        {
            return NoContent();
        }

        if (result == StudentResult.StudentNotFound)
        {
            return NotFound("Student with provided id does not exist.");
        }

        if (result == StudentResult.EmailAlreadyExists)
        {
            return Conflict("Student with provided Email already exists.");
        }

        if (result == StudentResult.NationalIdAlreadyExists)
        {
            return Conflict("Student with provided National Id already exists.");
        }

        if (result == StudentResult.MajorNotFound)
        {
            return NotFound("Major with provided Id does not exist.");
        }

        return Problem();
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var result = await _studentService.DeleteAsync(id);
        if (result == StudentResult.StudentNotFound)
        {
            return NotFound("Student with provided id does not exist.");
        }

        return NoContent();

    }
}