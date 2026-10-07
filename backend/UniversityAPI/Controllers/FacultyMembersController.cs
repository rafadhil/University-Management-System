using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/facultyMembers")]
public class FacultyMembersController : ControllerBase
{
    private readonly FacultyMemberService _service;

    public FacultyMembersController(FacultyMemberService service)
    {
        _service = service;
    }

    // GET: api/facultyMembers/all?pageSize=50&pageNumber=1
    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<FacultyMemberDto>>> GetAll(
        int pageSize = 50,
        int pageNumber = 1)
    {
        if (pageSize < 1 || pageNumber < 1)
        {
            return BadRequest("Page size and page number must be greater than 0.");
        }

        var facultyMembers = await _service.GetAllAsync(pageSize, pageNumber);

        return Ok(facultyMembers);
    }

    // GET: api/facultyMembers/5
    [HttpGet("{id:int}")]
    public async Task<ActionResult<FacultyMemberDto>> GetById(int id)
    {
        var facultyMember = await _service.GetByIdAsync(id);

        if (facultyMember == null)
        {
            return NotFound("Faculty member not found.");
        }

        return Ok(facultyMember);
    }

    [HttpGet]
    public async Task<ActionResult<FacultyMemberDto>> GetFacultyMember(
        int? employeeId,
        string? nationalId)
    {
        FacultyMemberDto? facultyMember;

        if (employeeId.HasValue)
        {
            facultyMember = await _service.GetByEmployeeIdAsync(employeeId.Value);
        }

        else if (!string.IsNullOrEmpty(nationalId))
        {
            facultyMember = await _service.GetByNationalIdAsync(nationalId);
        }

        else
        {
            return BadRequest("Provide either an Employee Id or a National Number");
        }

        if (facultyMember == null)
        {
            return NotFound("Faculty member not found.");
        }

        return Ok(facultyMember);
    }

    // POST: api/FacultyMembers
    [HttpPost]
    public async Task<ActionResult<FacultyMemberDto>> Create(
        CreateFacultyMemberDto dto)
    {
        var (facultyMember, result) = await _service.CreateAsync(dto);

        return result switch
        {
            FacultyMemberResult.Success =>
                CreatedAtAction(
                    nameof(GetById),
                    new { id = facultyMember!.Id },
                    facultyMember),

            FacultyMemberResult.EmailAlreadyExists =>
                Conflict("Email already exists."),

            FacultyMemberResult.NationalIdAlreadyExists =>
                Conflict("National ID already exists."),

            FacultyMemberResult.DepartmentNotFound =>
                NotFound("Department not found."),

            FacultyMemberResult.UserNotFound =>
                NotFound("User not found."),

            _ => Problem()
        };
    }

    // PATCH: api/facultyMembers/5
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        AdminUpdateFacultyMemberDto dto)
    {
        var result = await _service.UpdateAsync(id, dto);

        return result switch
        {
            FacultyMemberResult.Success =>
                NoContent(),

            FacultyMemberResult.FacultyMemberNotFound =>
                NotFound("Faculty member not found."),

            FacultyMemberResult.EmailAlreadyExists =>
                Conflict("Email already exists."),

            FacultyMemberResult.NationalIdAlreadyExists =>
                Conflict("National ID already exists."),

            FacultyMemberResult.DepartmentNotFound =>
                NotFound("Department not found."),

            FacultyMemberResult.UserNotFound =>
                NotFound("User not found."),

            _ => Problem()
        };
    }

    // DELETE: api/facultyMembers/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAsync(id);

        return result switch
        {
            FacultyMemberResult.Success =>
                NoContent(),

            FacultyMemberResult.FacultyMemberNotFound =>
                NotFound("Faculty member not found."),

            _ => Problem()
        };
    }
}