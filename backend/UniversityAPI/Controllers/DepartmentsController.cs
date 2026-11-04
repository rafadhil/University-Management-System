using Microsoft.AspNetCore.Mvc;
using UniversityAPI.DTOs;
using UniversityAPI.Models;

namespace UniversityAPI.Controllers;

[ApiController]
[Route("api/departments")]
public class DepartmentsController : ControllerBase
{
    private readonly DepartmentService _departmentService;

    public DepartmentsController(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<DepartmentResponseDto>>>
        GetAll([FromQuery] int? facultyId = null)
    {
        var departments =
            await _departmentService.GetAllAsync(facultyId);

        if (departments.Count == 0)
            return NotFound();

        return Ok(departments.Select(ToResponseDto));
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DepartmentResponseDto>>
        GetById(int id)
    {
        var department =
            await _departmentService.GetByIdAsync(id);

        if (department is null)
            return NotFound();

        return Ok(ToResponseDto(department));
    }

    [HttpGet("code/{code}")]
    public async Task<ActionResult<DepartmentResponseDto>>
        GetByCode(string code)
    {
        var department =
            await _departmentService.GetByCodeAsync(code);

        if (department is null)
            return NotFound();

        return Ok(ToResponseDto(department));
    }

    [HttpPost]
    public async Task<ActionResult<DepartmentResponseDto>> Create(
        CreateDepartmentDto dto)
    {
        var department = new Department
        {
            Name = dto.Name,
            Code = dto.Code,
            FacultyId = dto.FacultyId,
            Description = dto.Description,
            DepartmentHeadId = dto.DepartmentHeadId,
            Email = dto.Email,
            IsActive = dto.IsActive
        };

        var (result, id) =
            await _departmentService.CreateAsync(department);

        if (result == DepartmentResult.DepartmentWithCodeAlreadyExists)
        {
            return Conflict(new
            {
                message = "A department with this code already exists."
            });
        }

        if (result == DepartmentResult.DepartmentWithEmailAlreadyExists)
        {
            return Conflict(new
            {
                message = "A department with this email already exists."
            });
        }

        if (result == DepartmentResult.FacultyNotFound)
            return BadRequest("Faculty not found.");

        if (result == DepartmentResult.FacultyMemberNotFound)
            return BadRequest("Faculty member (Department Head) not found.");

        if (result == DepartmentResult.FacultyMemberBelongsToAnotherFaculty)
        {
            return BadRequest(
                "The department head must belong to the specified faculty.");
        }

        if (result != DepartmentResult.Success || id is null)
            return BadRequest();

        var createdDepartment =
            await _departmentService.GetByIdAsync(id.Value);

        if (createdDepartment is null)
            return StatusCode(StatusCodes.Status500InternalServerError);

        return CreatedAtAction(
            nameof(GetById),
            new { id = id.Value },
            ToResponseDto(createdDepartment));
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateDepartmentDto dto)
    {
        var result =
            await _departmentService.UpdateAsync(id, dto);

        return result switch
        {
            DepartmentResult.Success =>
                NoContent(),

            DepartmentResult.DepartmentNotFound =>
                NotFound(),

            DepartmentResult.DepartmentWithCodeAlreadyExists =>
                Conflict(new
                {
                    message = "A department with this code already exists."
                }),

            DepartmentResult.DepartmentWithEmailAlreadyExists =>
                Conflict(new
                {
                    message = "A department with this email already exists."
                }),

            DepartmentResult.FacultyNotFound =>
                BadRequest(new
                {
                    message = "The specified faculty does not exist."
                }),

            DepartmentResult.FacultyMemberNotFound =>
                BadRequest(new
                {
                    message = "The specified faculty member does not exist."
                }),

            DepartmentResult.FacultyMemberBelongsToAnotherFaculty =>
                BadRequest(new
                {
                    message = "The department head must belong to the department's faculty."
                }),

            _ => BadRequest(new
            {
                message = "The department could not be updated."
            })
        };
    }

    // DELETE: api/departments/5
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        bool deleted =
            await _departmentService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }

    private static DepartmentResponseDto ToResponseDto(
        Department department)
    {
        return new DepartmentResponseDto
        {
            Id = department.Id,
            Name = department.Name,
            Code = department.Code,
            FacultyId = department.FacultyId,
            FacultyName = department.Faculty.Name,
            Description = department.Description,
            DepartmentHeadId = department.DepartmentHeadId,
            DepartmentHeadName =
            department.DepartmentHead != null ?
                $"{department.DepartmentHead.FirstName} {department.DepartmentHead.LastName}"
                : null,
            Email = department.Email,
            IsActive = department.IsActive
        };
    }
}
