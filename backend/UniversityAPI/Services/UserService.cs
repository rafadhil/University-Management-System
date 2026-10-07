using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityAPI.Data;
using UniversityAPI.Models;

namespace UniversityAPI.Services;

public class UserService
{
    private readonly UniversityDbContext _context;
    private readonly IPasswordHasher<User> _passwordHasher;

    public UserService(
        UniversityDbContext context,
        IPasswordHasher<User> passwordHasher)
    {
        _context = context;
        _passwordHasher = passwordHasher;
    }

    public async Task<(UserResult, int? id)> CreateAsync(User user)
    {
        user.Email = user.Email.Trim().ToLowerInvariant();
        user.Username = user.Username.Trim().ToLowerInvariant();

        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
        {
            return (UserResult.UserWithEmailAlreadyExists, null);
        }

        if (await _context.Users.AnyAsync(u => u.Username == user.Username))
        {
            return (UserResult.UserWithUsernameAlreadyExists, null);
        }

        user.Password = _passwordHasher.HashPassword(
            user,
            user.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (UserResult.Success, user.Id);
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> GetByUsernameAsync(string userName)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Username == userName);
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserResult> UpdateAsync(int userId, UpdateUserDto dto)
    {
        dto.Username = dto.Username?.Trim().ToLowerInvariant();
        dto.Email = dto.Email?.Trim().ToLowerInvariant();

        var existingUser = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (existingUser == null)
        {
            return UserResult.UserNotFound;
        }

        if (!string.IsNullOrWhiteSpace(dto.Username))
        {
            bool userWithUsernameExist = await _context.Users
                .AnyAsync(u => u.Id != userId && u.Username == dto.Username);

            if (userWithUsernameExist)
            {
                return UserResult.UserWithUsernameAlreadyExists;
            }
            existingUser.Username = dto.Username;
        }

        if (!string.IsNullOrWhiteSpace(dto.Email))
        {
            bool userWithEmailExist = await _context.Users
                .AnyAsync(u => u.Id != userId && u.Email == dto.Email);

            if (userWithEmailExist)
            {
                return UserResult.UserWithEmailAlreadyExists;
            }
            existingUser.Email = dto.Email;
        }

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            existingUser.Password = _passwordHasher.HashPassword(existingUser, dto.Password);
        }

        if (dto.IsActive.HasValue)
        {
            existingUser.IsActive = dto.IsActive.Value;
        }

        await _context.SaveChangesAsync();

        return UserResult.Success;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return false;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }
}