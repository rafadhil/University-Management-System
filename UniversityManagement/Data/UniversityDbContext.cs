using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UniversityManagement.Models;

namespace UniversityManagement.Data;

public partial class UniversityDbContext : DbContext
{
    public UniversityDbContext(DbContextOptions<UniversityDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Building> Buildings { get; set; }

    public virtual DbSet<Classroom> Classrooms { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseEnrollment> CourseEnrollments { get; set; }

    public virtual DbSet<CourseOffering> CourseOfferings { get; set; }

    public virtual DbSet<CourseOfferingInstructor> CourseOfferingInstructors { get; set; }

    public virtual DbSet<CourseOfferingSchedule> CourseOfferingSchedules { get; set; }

    public virtual DbSet<CoursePrerequisite> CoursePrerequisites { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<FacultyMember> FacultyMembers { get; set; }

    public virtual DbSet<Major> Majors { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Semester> Semesters { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Building>(entity =>
        {
            entity.ToTable("Building");

            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Classroom>(entity =>
        {
            entity.ToTable("Classroom");

            entity.HasIndex(e => e.BuildingId, "IX_Classroom_BuildingId");

            entity.Property(e => e.Number).HasMaxLength(20);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasConversion<string>();

            entity.HasOne(d => d.Building).WithMany(p => p.Classrooms)
                .HasForeignKey(d => d.BuildingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Classroom_Building");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");

            entity.HasIndex(e => e.DepartmentId, "IX_Course_DepartmentId");

            entity.HasIndex(e => e.Code, "UQ_Course_Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(150);
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasConversion<string>();

            entity.HasOne(d => d.Department).WithMany(p => p.Courses)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Department");
        });

        modelBuilder.Entity<CourseEnrollment>(entity =>
        {
            entity.ToTable("CourseEnrollment");

            entity.HasIndex(e => e.CourseOfferingId, "IX_CourseEnrollment_CourseOfferingId");

            entity.HasIndex(e => e.StudentId, "IX_CourseEnrollment_StudentId");

            entity.Property(e => e.EnrollmentDate).HasDefaultValueSql("(CONVERT([date],getdate()))");
            entity.Property(e => e.FinalGrade).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.CourseOffering).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.CourseOfferingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseEnrollment_CourseOffering");

            entity.HasOne(d => d.Student).WithMany(p => p.CourseEnrollments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseEnrollment_Student");
        });

        modelBuilder.Entity<CourseOffering>(entity =>
        {
            entity.ToTable("CourseOffering");

            entity.HasIndex(e => e.CourseId, "IX_CourseOffering_CourseId");

            entity.HasIndex(e => e.SemesterId, "IX_CourseOffering_SemesterId");

            entity.Property(e => e.Section).HasMaxLength(10);

            entity.HasOne(d => d.Course).WithMany(p => p.CourseOfferings)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseOffering_Course");

            entity.HasOne(d => d.Semester).WithMany(p => p.CourseOfferings)
                .HasForeignKey(d => d.SemesterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseOffering_Semester");
        });

        modelBuilder.Entity<CourseOfferingInstructor>(entity =>
        {
            entity.ToTable("CourseOfferingInstructor");

            entity.HasIndex(e => e.CourseOfferingId, "IX_CourseOfferingInstructor_CourseOfferingId");

            entity.HasIndex(e => e.InstructorId, "IX_CourseOfferingInstructor_InstructorId");

            entity.HasOne(d => d.CourseOffering).WithMany(p => p.CourseOfferingInstructors)
                .HasForeignKey(d => d.CourseOfferingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseOfferingInstructor_CourseOffering");

            entity.HasOne(d => d.Instructor).WithMany(p => p.CourseOfferingInstructors)
                .HasForeignKey(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseOfferingInstructor_Instructor");
        });

        modelBuilder.Entity<CourseOfferingSchedule>(entity =>
        {
            entity.ToTable("CourseOfferingSchedule");

            entity.HasIndex(e => e.ClassroomId, "IX_CourseOfferingSchedule_ClassroomId");

            entity.HasIndex(e => e.CourseOfferingId, "IX_CourseOfferingSchedule_CourseOfferingId");

            entity.Property(e => e.DayOfWeek)
                .HasMaxLength(3)
                .IsUnicode(false)
                .IsFixedLength()
                .HasConversion<string>();

            entity.HasOne(d => d.Classroom).WithMany(p => p.CourseOfferingSchedules)
                .HasForeignKey(d => d.ClassroomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseOfferingSchedule_Classroom");

            entity.HasOne(d => d.CourseOffering).WithMany(p => p.CourseOfferingSchedules)
                .HasForeignKey(d => d.CourseOfferingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseOfferingSchedule_CourseOffering");
        });



        modelBuilder.Entity<CoursePrerequisite>(entity =>
        {
            entity.ToTable("CoursePrerequisite");

            entity.HasIndex(e => e.CourseId, "IX_CoursePrerequisite_CourseId");

            entity.HasIndex(e => e.PrerequisiteCourseId, "IX_CoursePrerequisite_PrerequisiteCourseId");

            entity.HasOne(d => d.Course).WithMany(p => p.CoursePrerequisiteCourses)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursePrerequisite_Course");

            entity.HasOne(d => d.PrerequisiteCourse).WithMany(p => p.CoursePrerequisitePrerequisiteCourses)
                .HasForeignKey(d => d.PrerequisiteCourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CoursePrerequisite_PrerequisiteCourse");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("Department");

            entity.HasIndex(e => e.DepartmentHeadId, "IX_Department_DepartmentHeadId");

            entity.HasIndex(e => e.FacultyId, "IX_Department_FacultyID");

            entity.HasIndex(e => e.Code, "UQ_Department_Code").IsUnique();

            entity.HasIndex(e => e.Email, "UQ_Department_Email").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FacultyId).HasColumnName("FacultyID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.DepartmentHead).WithMany(p => p.Departments)
                .HasForeignKey(d => d.DepartmentHeadId)
                .HasConstraintName("FK_Department_DepartmentHead");

            entity.HasOne(d => d.Faculty).WithMany(p => p.Departments)
                .HasForeignKey(d => d.FacultyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Department_Faculty");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.ToTable("Faculty");

            entity.HasIndex(e => e.Code, "UQ_Faculty_Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(30);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<FacultyMember>(entity =>
        {
            entity.ToTable("FacultyMember");

            entity.HasIndex(e => e.DepartmentId, "IX_FacultyMember_DepartmentId");

            entity.HasIndex(e => e.UserId, "IX_FacultyMember_UserId");

            entity.HasIndex(e => e.Email, "UQ_FacultyMember_Email").IsUnique();

            entity.HasIndex(e => e.EmployeeId, "UQ_FacultyMember_EmployeeId").IsUnique();

            entity.HasIndex(e => e.NationalId, "UQ_FacultyMember_NationalId").IsUnique();

            entity.Property(e => e.AcademicRank)
                .HasMaxLength(50)
                .HasConversion<string>();
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.EmployeeId).HasDefaultValueSql("(NEXT VALUE FOR [FacultyEmployeeIdSequence])");
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.NationalId).HasMaxLength(20);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);
            entity.Property(e => e.SecondName).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasConversion<string>();
            entity.Property(e => e.ThirdName).HasMaxLength(100);

            entity.HasOne(d => d.Department).WithMany(p => p.FacultyMembers)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_FacultyMember_Department");

            entity.HasOne(d => d.User).WithMany(p => p.FacultyMembers)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_FacultyMember_User");
        });

        modelBuilder.Entity<Major>(entity =>
        {
            entity.ToTable("Major");

            entity.HasIndex(e => e.DepartmentId, "IX_Major_DepartmentId");

            entity.HasIndex(e => e.Code, "UQ_Major_Code").IsUnique();

            entity.Property(e => e.Code).HasMaxLength(20);
            entity.Property(e => e.DegreeType)
                .HasMaxLength(50)
                .HasConversion<string>();
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(100);

            entity.HasOne(d => d.Department).WithMany(p => p.Majors)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Major_Department");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.HasIndex(e => e.Name, "UQ_Role_Name").IsUnique();

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Semester>(entity =>
        {
            entity.ToTable("Semester");

            entity.Property(e => e.Term).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.ToTable("Student");

            entity.HasIndex(e => e.MajorId, "IX_Student_MajorId");

            entity.HasIndex(e => e.UserId, "IX_Student_UserId");

            entity.HasIndex(e => e.Email, "UQ_Student_Email").IsUnique();

            entity.HasIndex(e => e.NationalId, "UQ_Student_NationalId").IsUnique();

            entity.HasIndex(e => e.UniversityId, "UQ_Student_UniversityId").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(100);
            entity.Property(e => e.LastName).HasMaxLength(100);
            entity.Property(e => e.NationalId).HasMaxLength(20);
            entity.Property(e => e.PhoneNumber).HasMaxLength(30);
            entity.Property(e => e.SecondName).HasMaxLength(100);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue(StudentStatus.Active)
                .HasConversion<string>();
            entity.Property(e => e.ThirdName).HasMaxLength(100);
            entity.Property(e => e.UniversityId).HasDefaultValueSql("(NEXT VALUE FOR [StudentUniversityIdSequence])");

            entity.HasOne(d => d.Major).WithMany(p => p.Students)
                .HasForeignKey(d => d.MajorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Student_Major");

            entity.HasOne(d => d.User).WithMany(p => p.Students)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK_Student_User");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "UQ_User_Email").IsUnique();

            entity.HasIndex(e => e.Username, "UQ_User_Username").IsUnique();

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(sysdatetime())");
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.Username).HasMaxLength(100);
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("UserRole");

            entity.HasIndex(e => e.RoleId, "IX_UserRole_RoleId");

            entity.HasIndex(e => e.UserId, "IX_UserRole_UserId");

            entity.HasOne(d => d.Role).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_Role");

            entity.HasOne(d => d.User).WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserRole_User");
        });

        modelBuilder.HasSequence<int>("FacultyEmployeeIdSequence").StartsAt(50000L);
        modelBuilder.HasSequence<int>("StudentUniversityIdSequence").StartsAt(10000L);

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
