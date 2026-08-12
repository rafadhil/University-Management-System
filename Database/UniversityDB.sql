
/* ============================================================
   UNIVERSITY DATABASE
   SQL SERVER DATABASE CREATION SCRIPT
   ============================================================ */

CREATE DATABASE UniversityDB;
GO

USE UniversityDB;
GO


/* ============================================================
   SEQUENCES
   ============================================================ */

CREATE SEQUENCE StudentUniversityIdSequence
    AS INT
    START WITH 10000
    INCREMENT BY 1;
GO

CREATE SEQUENCE FacultyEmployeeIdSequence
    AS INT
    START WITH 50000
    INCREMENT BY 1;
GO


/* ============================================================
   FACULTY
   ============================================================ */

CREATE TABLE Faculty
(
    Id INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Code NVARCHAR(30) NOT NULL,
    Description NVARCHAR(500) NULL,
    IsActive BIT NOT NULL
        CONSTRAINT DF_Faculty_IsActive DEFAULT 1,

    CONSTRAINT PK_Faculty
        PRIMARY KEY (Id),

    CONSTRAINT UQ_Faculty_Code
        UNIQUE (Code)
);
GO


/* ============================================================
   USER
   ============================================================ */

CREATE TABLE [User]
(
    Id INT IDENTITY(1,1) NOT NULL,
    Username NVARCHAR(100) NOT NULL,
    [Password] NVARCHAR(255) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME2 NOT NULL
        CONSTRAINT DF_User_CreatedAt DEFAULT SYSDATETIME(),
    IsActive BIT NOT NULL
        CONSTRAINT DF_User_IsActive DEFAULT 1,

    CONSTRAINT PK_User
        PRIMARY KEY (Id),

    CONSTRAINT UQ_User_Username
        UNIQUE (Username),

    CONSTRAINT UQ_User_Email
        UNIQUE (Email)
);
GO


/* ============================================================
   ROLE
   ============================================================ */

CREATE TABLE Role
(
    Id INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_Role
        PRIMARY KEY (Id),

    CONSTRAINT UQ_Role_Name
        UNIQUE (Name)
);
GO


/* ============================================================
   DEPARTMENT
   ============================================================ */

CREATE TABLE Department
(
    Id INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Code NVARCHAR(20) NOT NULL,
    FacultyID INT NOT NULL,
    Description NVARCHAR(500) NULL,
    DepartmentHeadId INT NULL,
    Email NVARCHAR(255) NULL,
    IsActive BIT NOT NULL
        CONSTRAINT DF_Department_IsActive DEFAULT 1,

    CONSTRAINT PK_Department
        PRIMARY KEY (Id),

    CONSTRAINT UQ_Department_Code
        UNIQUE (Code),

    CONSTRAINT UQ_Department_Email
        UNIQUE (Email),

    CONSTRAINT FK_Department_Faculty
        FOREIGN KEY (FacultyID)
        REFERENCES Faculty(Id)
);
GO


/* ============================================================
   MAJOR
   ============================================================ */

CREATE TABLE Major
(
    Id INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    Code NVARCHAR(20) NOT NULL,
    DepartmentId INT NOT NULL,
    Description NVARCHAR(500) NULL,
    TotalRquiredCreditHours INT NOT NULL,
    IsActive BIT NOT NULL
        CONSTRAINT DF_Major_IsActive DEFAULT 1,
    DegreeType NVARCHAR(50) NOT NULL,

    CONSTRAINT PK_Major
        PRIMARY KEY (Id),

    CONSTRAINT UQ_Major_Code
        UNIQUE (Code),

    CONSTRAINT FK_Major_Department
        FOREIGN KEY (DepartmentId)
        REFERENCES Department(Id),

    CONSTRAINT CK_Major_TotalRquiredCreditHours
        CHECK (TotalRquiredCreditHours > 0),

    CONSTRAINT CK_Major_DegreeType
        CHECK (DegreeType IN
        (
            'Diploma',
            'Associate',
            'Bachelor',
            'Master',
            'Doctorate'
        ))
);
GO


/* ============================================================
   FACULTY MEMBER
   ============================================================ */

CREATE TABLE FacultyMember
(
    Id INT IDENTITY(1,1) NOT NULL,

    EmployeeId INT NOT NULL
        CONSTRAINT DF_FacultyMember_EmployeeId
        DEFAULT NEXT VALUE FOR FacultyEmployeeIdSequence,

    FirstName NVARCHAR(100) NOT NULL,
    SecondName NVARCHAR(100) NOT NULL,
    ThirdName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    NationalId NVARCHAR(20) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(30) NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    DepartmentId INT NOT NULL,
    AcademicRank NVARCHAR(50) NOT NULL,
    UserId INT NULL,
    HireDate DATE NOT NULL,

    CONSTRAINT PK_FacultyMember
        PRIMARY KEY (Id),

    CONSTRAINT UQ_FacultyMember_EmployeeId
        UNIQUE (EmployeeId),

    CONSTRAINT UQ_FacultyMember_NationalId
        UNIQUE (NationalId),

    CONSTRAINT UQ_FacultyMember_Email
        UNIQUE (Email),

    CONSTRAINT FK_FacultyMember_Department
        FOREIGN KEY (DepartmentId)
        REFERENCES Department(Id),

    CONSTRAINT FK_FacultyMember_User
        FOREIGN KEY (UserId)
        REFERENCES [User](Id),


    CONSTRAINT CK_FacultyMember_Status
    CHECK (Status IN
    (
        'Active',
        'Suspended',
        'Retired',
        'Resigned',
        'Terminated'
    )),

    CONSTRAINT CK_FacultyMember_AcademicRank
    CHECK (AcademicRank IN
    (
        'Teaching Assistant',
        'Lecturer',
        'Assistant Professor',
        'Associate Professor',
        'Professor'
    ))
);
GO


/* ============================================================
   DEPARTMENT HEAD FOREIGN KEY
   DepartmentHeadId references FacultyMember.Id
   ============================================================ */

ALTER TABLE Department
ADD CONSTRAINT FK_Department_DepartmentHead
    FOREIGN KEY (DepartmentHeadId)
    REFERENCES FacultyMember(Id);
GO


/* ============================================================
   STUDENT
   ============================================================ */

CREATE TABLE Student
(
    Id INT IDENTITY(1,1) NOT NULL,

    UniversityId INT NOT NULL
        CONSTRAINT DF_Student_UniversityId
        DEFAULT NEXT VALUE FOR StudentUniversityIdSequence,

    FirstName NVARCHAR(100) NOT NULL,
    SecondName NVARCHAR(100) NOT NULL,
    ThirdName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    DateOfBirth DATE NOT NULL,
    NationalId NVARCHAR(20) NOT NULL,
    Email NVARCHAR(255) NOT NULL,
    PhoneNumber NVARCHAR(30) NOT NULL,
    Status NVARCHAR(50) NOT NULL
        CONSTRAINT DF_Student_Status DEFAULT 'Active',
    MajorId INT NOT NULL,
    AdmissionDate DATE NOT NULL,
    UserId INT NULL,

    CONSTRAINT PK_Student
        PRIMARY KEY (Id),

    CONSTRAINT UQ_Student_UniversityId
        UNIQUE (UniversityId),

    CONSTRAINT UQ_Student_NationalId
        UNIQUE (NationalId),

    CONSTRAINT UQ_Student_Email
        UNIQUE (Email),

    CONSTRAINT FK_Student_Major
        FOREIGN KEY (MajorId)
        REFERENCES Major(Id),

    CONSTRAINT FK_Student_User
        FOREIGN KEY (UserId)
        REFERENCES [User](Id),

    CONSTRAINT CK_Student_Status
    CHECK (Status IN
    (
        'Active',
        'Graduated',
        'Suspended',
        'Withdrawn',
        'Dismissed'
    ))
);
GO


/* ============================================================
   COURSE
   ============================================================ */

CREATE TABLE Course
(
    Id INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(150) NOT NULL,
    Code NVARCHAR(20) NOT NULL,
    Description NVARCHAR(500) NULL,
    CreditHours INT NOT NULL,
    DepartmentId INT NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    IsActive BIT NOT NULL
        CONSTRAINT DF_Course_IsActive DEFAULT 1,

    CONSTRAINT PK_Course
        PRIMARY KEY (Id),

    CONSTRAINT UQ_Course_Code
        UNIQUE (Code),

    CONSTRAINT FK_Course_Department
        FOREIGN KEY (DepartmentId)
        REFERENCES Department(Id),

    CONSTRAINT CK_Course_CreditHours
        CHECK (CreditHours > 0),

    CONSTRAINT CK_Course_Type
    CHECK (Type IN
    (
        'Core',
        'Elective',
        'General Education'
    ))
);
GO


/* ============================================================
   USER ROLE
   ============================================================ */

CREATE TABLE UserRole
(
    Id INT IDENTITY(1,1) NOT NULL,
    UserId INT NOT NULL,
    RoleId INT NOT NULL,

    CONSTRAINT PK_UserRole
        PRIMARY KEY (Id),

    CONSTRAINT FK_UserRole_User
        FOREIGN KEY (UserId)
        REFERENCES [User](Id),

    CONSTRAINT FK_UserRole_Role
        FOREIGN KEY (RoleId)
        REFERENCES Role(Id)
);
GO


/* ============================================================
   SEMESTER
   ============================================================ */

CREATE TABLE Semester
(
    Id INT IDENTITY(1,1) NOT NULL,
    Term NVARCHAR(50) NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,

    CONSTRAINT PK_Semester
        PRIMARY KEY (Id),

    CONSTRAINT CK_Semester_DateRange
        CHECK (StartDate < EndDate)
);
GO


/* ============================================================
   COURSE OFFERING
   ============================================================ */

CREATE TABLE CourseOffering
(
    Id INT IDENTITY(1,1) NOT NULL,
    CourseId INT NOT NULL,
    SemesterId INT NOT NULL,
    Section NVARCHAR(10) NOT NULL,
    Capacity INT NOT NULL,

    CONSTRAINT PK_CourseOffering
        PRIMARY KEY (Id),

    CONSTRAINT FK_CourseOffering_Course
        FOREIGN KEY (CourseId)
        REFERENCES Course(Id),

    CONSTRAINT FK_CourseOffering_Semester
        FOREIGN KEY (SemesterId)
        REFERENCES Semester(Id),

    CONSTRAINT CK_CourseOffering_Capacity
        CHECK (Capacity > 0)
);
GO


/* ============================================================
   COURSE ENROLLMENT
   ============================================================ */

CREATE TABLE CourseEnrollment
(
    Id INT IDENTITY(1,1) NOT NULL,
    CourseOfferingId INT NOT NULL,
    StudentId INT NOT NULL,
    FinalGrade DECIMAL(5,2) NULL,
    EnrollmentDate DATE NOT NULL
        CONSTRAINT DF_CourseEnrollment_EnrollmentDate
        DEFAULT CAST(GETDATE() AS DATE),

    CONSTRAINT PK_CourseEnrollment
        PRIMARY KEY (Id),

    CONSTRAINT FK_CourseEnrollment_CourseOffering
        FOREIGN KEY (CourseOfferingId)
        REFERENCES CourseOffering(Id),

    CONSTRAINT FK_CourseEnrollment_Student
        FOREIGN KEY (StudentId)
        REFERENCES Student(Id),

    CONSTRAINT CK_CourseEnrollment_FinalGrade
        CHECK
        (
            FinalGrade IS NULL
            OR
            (
                FinalGrade >= 0
                AND FinalGrade <= 100
            )
        )
);
GO


/* ============================================================
   COURSE OFFERING INSTRUCTOR
   ============================================================ */

CREATE TABLE CourseOfferingInstructor
(
    Id INT IDENTITY(1,1) NOT NULL,
    CourseOfferingId INT NOT NULL,
    InstructorId INT NOT NULL,

    CONSTRAINT PK_CourseOfferingInstructor
        PRIMARY KEY (Id),

    CONSTRAINT FK_CourseOfferingInstructor_CourseOffering
        FOREIGN KEY (CourseOfferingId)
        REFERENCES CourseOffering(Id),

    CONSTRAINT FK_CourseOfferingInstructor_Instructor
        FOREIGN KEY (InstructorId)
        REFERENCES FacultyMember(Id)
);
GO


/* ============================================================
   COURSE PREREQUISITE
   ============================================================ */

CREATE TABLE CoursePrerequisite
(
    Id INT IDENTITY(1,1) NOT NULL,
    CourseId INT NOT NULL,
    PrerequisiteCourseId INT NOT NULL,

    CONSTRAINT PK_CoursePrerequisite
        PRIMARY KEY (Id),

    CONSTRAINT FK_CoursePrerequisite_Course
        FOREIGN KEY (CourseId)
        REFERENCES Course(Id),

    CONSTRAINT FK_CoursePrerequisite_PrerequisiteCourse
        FOREIGN KEY (PrerequisiteCourseId)
        REFERENCES Course(Id),

    CONSTRAINT CK_CoursePrerequisite_NotSelf
        CHECK (CourseId <> PrerequisiteCourseId)
);
GO


/* ============================================================
   BUILDING
   ============================================================ */

CREATE TABLE Building
(
    Id INT IDENTITY(1,1) NOT NULL,
    Name NVARCHAR(100) NOT NULL,
    NumberOfFloors INT NOT NULL,
    IsActive BIT NOT NULL
        CONSTRAINT DF_Building_IsActive DEFAULT 1,

    CONSTRAINT PK_Building
        PRIMARY KEY (Id),

    CONSTRAINT CK_Building_NumberOfFloors
        CHECK (NumberOfFloors > 0)
);
GO


/* ============================================================
   CLASSROOM
   ============================================================ */

CREATE TABLE Classroom
(
    Id INT IDENTITY(1,1) NOT NULL,
    Number NVARCHAR(20) NOT NULL,
    BuildingId INT NOT NULL,
    Type NVARCHAR(50) NOT NULL,
    FloorNumber INT NOT NULL,
    Capacity INT NOT NULL,

    CONSTRAINT PK_Classroom
        PRIMARY KEY (Id),

    CONSTRAINT FK_Classroom_Building
        FOREIGN KEY (BuildingId)
        REFERENCES Building(Id),

    CONSTRAINT CK_Classroom_FloorNumber
        CHECK (FloorNumber >= 0),

    CONSTRAINT CK_Classroom_Capacity
        CHECK (Capacity > 0),

    CONSTRAINT CK_Classroom_Type
        CHECK (Type IN
        (
            'Lecture Hall',
            'Classroom',
            'Laboratory',
            'Computer Lab',
            'Seminar Room'
        ))
);
GO


/* ============================================================
   COURSE OFFERING SCHEDULE
   ============================================================ */

CREATE TABLE CourseOfferingSchedule
(
    Id INT IDENTITY(1,1) NOT NULL,
    CourseOfferingId INT NOT NULL,
    DayOfWeek CHAR(3) NOT NULL,
    StartTime TIME NOT NULL,
    EndTime TIME NOT NULL,
    ClassroomId INT NOT NULL,

    CONSTRAINT PK_CourseOfferingSchedule
        PRIMARY KEY (Id),

    CONSTRAINT FK_CourseOfferingSchedule_CourseOffering
        FOREIGN KEY (CourseOfferingId)
        REFERENCES CourseOffering(Id),

    CONSTRAINT FK_CourseOfferingSchedule_Classroom
        FOREIGN KEY (ClassroomId)
        REFERENCES Classroom(Id),

    CONSTRAINT CK_CourseOfferingSchedule_TimeRange
        CHECK (StartTime < EndTime),

    CONSTRAINT CK_CourseOfferingSchedule_DayOfWeek
        CHECK (DayOfWeek IN
        (
            'SUN',
            'MON',
            'TUE',
            'WED',
            'THU',
            'FRI',
            'SAT'
        ))
);
GO


/* ============================================================
   INDEXES
   ============================================================ */

CREATE INDEX IX_Department_FacultyID
    ON Department(FacultyID);

CREATE INDEX IX_Department_DepartmentHeadId
    ON Department(DepartmentHeadId);

CREATE INDEX IX_Major_DepartmentId
    ON Major(DepartmentId);

CREATE INDEX IX_FacultyMember_DepartmentId
    ON FacultyMember(DepartmentId);

CREATE INDEX IX_FacultyMember_UserId
    ON FacultyMember(UserId);

CREATE INDEX IX_Student_MajorId
    ON Student(MajorId);

CREATE INDEX IX_Student_UserId
    ON Student(UserId);

CREATE INDEX IX_Course_DepartmentId
    ON Course(DepartmentId);

CREATE INDEX IX_UserRole_UserId
    ON UserRole(UserId);

CREATE INDEX IX_UserRole_RoleId
    ON UserRole(RoleId);

CREATE INDEX IX_CourseEnrollment_CourseOfferingId
    ON CourseEnrollment(CourseOfferingId);

CREATE INDEX IX_CourseEnrollment_StudentId
    ON CourseEnrollment(StudentId);

CREATE INDEX IX_CourseOffering_CourseId
    ON CourseOffering(CourseId);

CREATE INDEX IX_CourseOffering_SemesterId
    ON CourseOffering(SemesterId);

CREATE INDEX IX_CourseOfferingInstructor_CourseOfferingId
    ON CourseOfferingInstructor(CourseOfferingId);

CREATE INDEX IX_CourseOfferingInstructor_InstructorId
    ON CourseOfferingInstructor(InstructorId);

CREATE INDEX IX_CoursePrerequisite_CourseId
    ON CoursePrerequisite(CourseId);

CREATE INDEX IX_CoursePrerequisite_PrerequisiteCourseId
    ON CoursePrerequisite(PrerequisiteCourseId);

CREATE INDEX IX_CourseOfferingSchedule_CourseOfferingId
    ON CourseOfferingSchedule(CourseOfferingId);

CREATE INDEX IX_CourseOfferingSchedule_ClassroomId
    ON CourseOfferingSchedule(ClassroomId);

CREATE INDEX IX_Classroom_BuildingId
    ON Classroom(BuildingId);
GO

