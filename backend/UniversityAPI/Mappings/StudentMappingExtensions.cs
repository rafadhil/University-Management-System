using UniversityAPI.Models;
public static class StudentMappingExtensions
{
    public static StudentDto ToDto(this Student student)
    {
        return new StudentDto
        {
            Id = student.Id,
            UniversityId = student.UniversityId,
            FullName = $"{student.FirstName} {student.SecondName} {student.ThirdName} {student.LastName}",
            DateOfBirth = student.DateOfBirth,
            NationalId = student.NationalId,
            Email = student.Email,
            PhoneNumber = student.PhoneNumber,
            Status = student.Status,
            Major = student.Major?.Name,
            AdmissionDate = student.AdmissionDate,
            UserId = student.UserId
        };
    }
}