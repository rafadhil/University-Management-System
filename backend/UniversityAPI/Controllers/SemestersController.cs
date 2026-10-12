using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Models;

namespace UniversityAPI.Controllers;

[ApiController]
[Route("api/semesters")]
public class SemestersController : ControllerBase
{
    private readonly SemesterService _semesterService;

    public SemestersController(SemesterService semesterService)
        => _semesterService = semesterService;

    [HttpPost]
    public async Task<ActionResult<SemesterDto>> Create(CreateSemesterDto dto)
    {
        var (result, semester) = await _semesterService.CreateAsync(dto);

        switch (result)
        {
            case SemeseterResult.Success:
                return CreatedAtAction(
                    nameof(GetById),
                    new { semesterId = semester!.Id },
                    new SemesterDto
                    {
                        Id = semester.Id,
                        Term = semester.Term,
                        StartDate = semester.StartDate,
                        EndDate = semester.EndDate
                    });

            case SemeseterResult.StartDateAfterEndDate:
                return BadRequest(new
                {
                    message = "Start date must be before or equal to end date."
                });

            case SemeseterResult.HasOverlappingSemester:
                return Conflict(new
                {
                    message = "The semester overlaps with an existing semester."
                });

            default:
                return StatusCode(500);
        }
    }

    [HttpGet("all")]
    public async Task<ActionResult<IEnumerable<SemesterDto>>> GetAll(int pageSize = 20, int pageNumber = 1)
    {
        return Ok(await _semesterService.GetAllAsync(pageSize, pageNumber));
    }

    [HttpGet("{semesterId:int}")]
    public async Task<ActionResult<SemesterDto>> GetById(int semesterId)
    {
        var semester =
            await _semesterService.GetByIdAsync(semesterId);

        if (semester == null)
            return NotFound("Semester with provided Id not found");


        return Ok(new SemesterDto
        {
            Id = semester.Id,
            Term = semester.Term,
            StartDate = semester.StartDate,
            EndDate = semester.EndDate
        });
    }

    [HttpGet("by-term")]
    public async Task<ActionResult<SemesterDto>> GetByTerm(
        [FromQuery] SemesterTerm term,
        [FromQuery] int year)
    {
        var semester =
            await _semesterService.GetByTermAsync(term, year);

        if (semester == null)
            return NotFound("Semester with provided term not found");

        return Ok(new SemesterDto
        {
            Id = semester.Id,
            Term = semester.Term,
            StartDate = semester.StartDate,
            EndDate = semester.EndDate
        });
    }

    [HttpPatch("{semesterId:int}")]
    public async Task<IActionResult> Update(
        int semesterId,
        UpdateSemesterDto dto)
    {
        var result = await _semesterService.UpdateAsync(semesterId, dto);

        switch (result)
        {
            case SemeseterResult.Success:
                return NoContent();

            case SemeseterResult.SemesterNotFound:
                return NotFound();

            case SemeseterResult.StartDateAfterEndDate:
                return BadRequest(new
                {
                    message = "Start date must be before or equal to end date."
                });

            case SemeseterResult.HasOverlappingSemester:
                return Conflict(new
                {
                    message = "The semester overlaps with an existing semester."
                });

            default:
                return StatusCode(500);
        }
    }

    [HttpDelete("{semesterId:int}")]
    public async Task<IActionResult> Delete(int semesterId)
    {
        var result = await _semesterService.DeleteAsync(semesterId);

        if (result == SemeseterResult.SemesterNotFound)
            return NotFound();

        return NoContent();
    }

    [HttpGet("active")]
    public async Task<ActionResult<SemesterDto>> GetActiveSemester()
    {
        var result = await _semesterService.GetActiveSemesterAsync();

        if (result == null)
            return NotFound("No active semester exists");

        return Ok(new SemesterDto
        {
            Id = result.Id,
            Term = result.Term,
            StartDate = result.StartDate,
            EndDate = result.EndDate
        });
    }
}