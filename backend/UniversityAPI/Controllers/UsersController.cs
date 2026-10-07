using Microsoft.AspNetCore.Mvc;
using UniversityAPI.Models;
using UniversityAPI.Services;

namespace UniversityAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService service)
    {
        _userService = service;
    }

    [HttpPost]
    public async Task<ActionResult> Create(CreateUserDto user)
    {
        var (result, id) = await _userService.CreateAsync(user);

        return result switch
        {
            UserResult.Success => CreatedAtAction(
                nameof(GetById),
                new { id },
                new { id }),

            UserResult.UserWithEmailAlreadyExists =>
                Conflict("A user with this email already exists."),

            UserResult.UserWithUsernameAlreadyExists =>
                Conflict("A user with this username already exists."),

            _ => BadRequest()
        };
    }

    [HttpGet("all")]
    public async Task<ActionResult<List<UserDto>>> GetAll()
    {
        return Ok(await _userService.GetAllAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserDto>> GetById(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound("User not found.");
        }

        return Ok(new UserDto
        {
            Id = user.Id,
            CreatedAt = user.CreatedAt,
            Email = user.Email,
            IsActive = user.IsActive,
            Username = user.Username
        });
    }

    [HttpGet]
    public async Task<ActionResult<UserDto>> Get(
    string? username,
    string? email)
    {
        User? user;

        if (!string.IsNullOrWhiteSpace(username))
        {
            user = await _userService.GetByUsernameAsync(username);
        }
        else if (!string.IsNullOrWhiteSpace(email))
        {
            user = await _userService.GetByEmailAsync(email);
        }
        else
        {
            return BadRequest("Provide a username or email.");
        }

        if (user == null)
        {
            return NotFound("User not found.");
        }

        return Ok(new UserDto
        {
            Id = user.Id,
            CreatedAt = user.CreatedAt,
            Email = user.Email,
            IsActive = user.IsActive,
            Username = user.Username
        });
    }

    [HttpPatch("{id:int}")]
    public async Task<ActionResult> Update(
        int id,
        UpdateUserDto dto)
    {
        var result = await _userService.UpdateAsync(id, dto);

        return result switch
        {
            UserResult.Success => NoContent(),

            UserResult.UserNotFound =>
                NotFound("User not found."),

            UserResult.UserWithEmailAlreadyExists =>
                Conflict("A user with this email already exists."),

            UserResult.UserWithUsernameAlreadyExists =>
                Conflict("A user with this username already exists."),

            _ => BadRequest()
        };
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        var deleted = await _userService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound("User not found.");
        }

        return NoContent();
    }
}