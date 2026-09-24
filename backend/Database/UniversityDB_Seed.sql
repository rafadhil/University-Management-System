/* ============================================================
   UNIVERSITY DATABASE - SEED DATA
   Target database: UniversityDB

   IMPORTANT:
   - This script assumes the database was freshly created by the
     corresponding UniversityDB creation script.
   - [User] is intentionally NOT seeded.
   - UserRole is also left empty because it requires User rows.
   - Student.UserId and FacultyMember.UserId remain NULL.
   - Identity PKs and the Student/Faculty sequences are generated
     by SQL Server.
   ============================================================ */

USE UniversityDB;
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;
GO

BEGIN TRY
    BEGIN TRANSACTION;


/* ============================================================
   FACULTIES
   ============================================================ */

INSERT INTO Faculty (Name, Code, Description)
VALUES
    ('Faculty of Computing and Information Technology', 'FCIT',
        'Programs in computer science, information technology, and related computing disciplines.'),
    ('Faculty of Engineering', 'ENG',
        'Programs in engineering and applied engineering sciences.'),
    ('Faculty of Science', 'SCI',
        'Programs in mathematics, physics, chemistry, biology, and related sciences.'),
    ('Faculty of Economics and Administration', 'FEA',
        'Programs in business, economics, finance, and administration.'),
    ('Faculty of Arts and Humanities', 'FAH',
        'Programs in languages, humanities, communication, and related disciplines.');


/* ============================================================
   DEPARTMENTS
   ============================================================ */

INSERT INTO Department
    (Name, Code, FacultyID, Description, Email)
VALUES
    ('Computer Science', 'CS',
        (SELECT Id FROM Faculty WHERE Code = 'FCIT'),
        'Computer science and software development programs.',
        'cs@university.edu.sa'),

    ('Information Technology', 'IT',
        (SELECT Id FROM Faculty WHERE Code = 'FCIT'),
        'Information technology, infrastructure, and information systems programs.',
        'it@university.edu.sa'),

    ('Computer Information Systems', 'CIS',
        (SELECT Id FROM Faculty WHERE Code = 'FCIT'),
        'Computer information systems and business technology programs.',
        'cis@university.edu.sa'),

    ('Electrical Engineering', 'EE',
        (SELECT Id FROM Faculty WHERE Code = 'ENG'),
        'Electrical and electronic engineering programs.',
        'ee@university.edu.sa'),

    ('Mechanical Engineering', 'ME',
        (SELECT Id FROM Faculty WHERE Code = 'ENG'),
        'Mechanical engineering programs.',
        'me@university.edu.sa'),

    ('Mathematics', 'MATH',
        (SELECT Id FROM Faculty WHERE Code = 'SCI'),
        'Pure and applied mathematics programs.',
        'math@university.edu.sa'),

    ('Physics', 'PHYS',
        (SELECT Id FROM Faculty WHERE Code = 'SCI'),
        'Physics and applied physics programs.',
        'physics@university.edu.sa'),

    ('Accounting', 'ACC',
        (SELECT Id FROM Faculty WHERE Code = 'FEA'),
        'Accounting and financial reporting programs.',
        'accounting@university.edu.sa'),

    ('Business Administration', 'BA',
        (SELECT Id FROM Faculty WHERE Code = 'FEA'),
        'Business administration and management programs.',
        'ba@university.edu.sa'),

    ('English Language', 'ENG-LANG',
        (SELECT Id FROM Faculty WHERE Code = 'FAH'),
        'English language and literature programs.',
        'english@university.edu.sa');


/* ============================================================
   MAJORS
   ============================================================ */

INSERT INTO Major
    (Name, Code, DepartmentId, Description, TotalRquiredCreditHours, DegreeType)
VALUES
    ('Computer Science', 'CS-BS',
        (SELECT Id FROM Department WHERE Code = 'CS'),
        'Bachelor of Science in Computer Science.',
        128, 'Bachelor'),

    ('Software Engineering', 'SE-BS',
        (SELECT Id FROM Department WHERE Code = 'CS'),
        'Bachelor of Science in Software Engineering.',
        128, 'Bachelor'),

    ('Artificial Intelligence', 'AI-BS',
        (SELECT Id FROM Department WHERE Code = 'CS'),
        'Bachelor of Science in Artificial Intelligence.',
        130, 'Bachelor'),

    ('Computer Science', 'CS-MS',
        (SELECT Id FROM Department WHERE Code = 'CS'),
        'Master of Science in Computer Science.',
        36, 'Master'),

    ('Information Technology', 'IT-BS',
        (SELECT Id FROM Department WHERE Code = 'IT'),
        'Bachelor of Science in Information Technology.',
        128, 'Bachelor'),

    ('Information Systems', 'IS-BS',
        (SELECT Id FROM Department WHERE Code = 'CIS'),
        'Bachelor of Science in Information Systems.',
        128, 'Bachelor'),

    ('Electrical Engineering', 'EE-BS',
        (SELECT Id FROM Department WHERE Code = 'EE'),
        'Bachelor of Science in Electrical Engineering.',
        160, 'Bachelor'),

    ('Mechanical Engineering', 'ME-BS',
        (SELECT Id FROM Department WHERE Code = 'ME'),
        'Bachelor of Science in Mechanical Engineering.',
        160, 'Bachelor'),

    ('Mathematics', 'MATH-BS',
        (SELECT Id FROM Department WHERE Code = 'MATH'),
        'Bachelor of Science in Mathematics.',
        128, 'Bachelor'),

    ('Accounting', 'ACC-BS',
        (SELECT Id FROM Department WHERE Code = 'ACC'),
        'Bachelor of Science in Accounting.',
        128, 'Bachelor'),

    ('Business Administration', 'BA-BS',
        (SELECT Id FROM Department WHERE Code = 'BA'),
        'Bachelor of Science in Business Administration.',
        128, 'Bachelor'),

    ('English Language', 'ENG-BS',
        (SELECT Id FROM Department WHERE Code = 'ENG-LANG'),
        'Bachelor of Arts in English Language.',
        128, 'Bachelor');


/* ============================================================
   FACULTY MEMBERS
   UserId intentionally omitted because [User] is not seeded.
   EmployeeId is generated automatically from its sequence.
   ============================================================ */

INSERT INTO FacultyMember
    (FirstName, SecondName, ThirdName, LastName,
     DateOfBirth, NationalId, Email, PhoneNumber,
     Status, DepartmentId, AcademicRank, HireDate)
VALUES
    ('Ahmed', 'Mohammed', 'Ali', 'Alharbi',
        '1978-03-14', 'SA100000001', 'ahmed.alharbi@university.edu.sa',
        '+966500000001', 'Active',
        (SELECT Id FROM Department WHERE Code = 'CS'),
        'Professor', '2008-09-01'),

    ('Sara', 'Abdullah', 'Hassan', 'Alqahtani',
        '1982-07-22', 'SA100000002', 'sara.alqahtani@university.edu.sa',
        '+966500000002', 'Active',
        (SELECT Id FROM Department WHERE Code = 'CS'),
        'Associate Professor', '2012-01-15'),

    ('Khalid', 'Omar', 'Saleh', 'Alzahrani',
        '1986-11-03', 'SA100000003', 'khalid.alzahrani@university.edu.sa',
        '+966500000003', 'Active',
        (SELECT Id FROM Department WHERE Code = 'CS'),
        'Assistant Professor', '2016-08-20'),

    ('Noura', 'Faisal', 'Ahmed', 'Alghamdi',
        '1989-05-18', 'SA100000004', 'noura.alghamdi@university.edu.sa',
        '+966500000004', 'Active',
        (SELECT Id FROM Department WHERE Code = 'IT'),
        'Assistant Professor', '2018-09-01'),

    ('Omar', 'Saeed', 'Yousef', 'Alotaibi',
        '1975-12-09', 'SA100000005', 'omar.alotaibi@university.edu.sa',
        '+966500000005', 'Active',
        (SELECT Id FROM Department WHERE Code = 'IT'),
        'Professor', '2005-02-01'),

    ('Maha', 'Ibrahim', 'Salem', 'Alshammari',
        '1984-02-27', 'SA100000006', 'maha.alshammari@university.edu.sa',
        '+966500000006', 'Active',
        (SELECT Id FROM Department WHERE Code = 'CIS'),
        'Associate Professor', '2011-09-01'),

    ('Fahad', 'Nasser', 'Abdulrahman', 'Almutairi',
        '1990-08-12', 'SA100000007', 'fahad.almutairi@university.edu.sa',
        '+966500000007', 'Active',
        (SELECT Id FROM Department WHERE Code = 'EE'),
        'Lecturer', '2020-01-12'),

    ('Reem', 'Hamad', 'Abdullah', 'Alenezi',
        '1987-04-05', 'SA100000008', 'reem.alenezi@university.edu.sa',
        '+966500000008', 'Active',
        (SELECT Id FROM Department WHERE Code = 'MATH'),
        'Associate Professor', '2015-09-01'),

    ('Yousef', 'Ali', 'Mohammed', 'Alqahtani',
        '1980-10-30', 'SA100000009', 'yousef.alqahtani@university.edu.sa',
        '+966500000009', 'Active',
        (SELECT Id FROM Department WHERE Code = 'PHYS'),
        'Professor', '2009-02-15'),

    ('Huda', 'Saleh', 'Omar', 'Alsharif',
        '1991-06-16', 'SA100000010', 'huda.alsharif@university.edu.sa',
        '+966500000010', 'Active',
        (SELECT Id FROM Department WHERE Code = 'ACC'),
        'Lecturer', '2021-09-01'),

    ('Turki', 'Abdullah', 'Saeed', 'Alghamdi',
        '1983-01-25', 'SA100000011', 'turki.alghamdi@university.edu.sa',
        '+966500000011', 'Active',
        (SELECT Id FROM Department WHERE Code = 'BA'),
        'Associate Professor', '2013-08-25'),

    ('Lama', 'Faisal', 'Khalid', 'Alharbi',
        '1992-09-07', 'SA100000012', 'lama.alharbi@university.edu.sa',
        '+966500000012', 'Active',
        (SELECT Id FROM Department WHERE Code = 'ENG-LANG'),
        'Teaching Assistant', '2022-09-01');


/* ============================================================
   DEPARTMENT HEADS
   ============================================================ */

UPDATE d
SET DepartmentHeadId =
    (
        SELECT f.Id
        FROM FacultyMember f
        WHERE f.Email = 'ahmed.alharbi@university.edu.sa'
    )
FROM Department d
WHERE d.Code = 'CS';

UPDATE d
SET DepartmentHeadId =
    (
        SELECT f.Id
        FROM FacultyMember f
        WHERE f.Email = 'omar.alotaibi@university.edu.sa'
    )
FROM Department d
WHERE d.Code = 'IT';

UPDATE d
SET DepartmentHeadId =
    (
        SELECT f.Id
        FROM FacultyMember f
        WHERE f.Email = 'maha.alshammari@university.edu.sa'
    )
FROM Department d
WHERE d.Code = 'CIS';

UPDATE d
SET DepartmentHeadId =
    (
        SELECT f.Id
        FROM FacultyMember f
        WHERE f.Email = 'fahad.almutairi@university.edu.sa'
    )
FROM Department d
WHERE d.Code = 'EE';

UPDATE d
SET DepartmentHeadId =
    (
        SELECT f.Id
        FROM FacultyMember f
        WHERE f.Email = 'reem.alenezi@university.edu.sa'
    )
FROM Department d
WHERE d.Code = 'MATH';

UPDATE d
SET DepartmentHeadId =
    (
        SELECT f.Id
        FROM FacultyMember f
        WHERE f.Email = 'yousef.alqahtani@university.edu.sa'
    )
FROM Department d
WHERE d.Code = 'PHYS';

UPDATE d
SET DepartmentHeadId =
    (
        SELECT f.Id
        FROM FacultyMember f
        WHERE f.Email = 'huda.alsharif@university.edu.sa'
    )
FROM Department d
WHERE d.Code = 'ACC';

UPDATE d
SET DepartmentHeadId =
    (
        SELECT f.Id
        FROM FacultyMember f
        WHERE f.Email = 'turki.alghamdi@university.edu.sa'
    )
FROM Department d
WHERE d.Code = 'BA';

UPDATE d
SET DepartmentHeadId =
    (
        SELECT f.Id
        FROM FacultyMember f
        WHERE f.Email = 'lama.alharbi@university.edu.sa'
    )
FROM Department d
WHERE d.Code = 'ENG-LANG';


/* ============================================================
   STUDENTS
   UniversityId is generated automatically starting at 10000.
   UserId intentionally omitted.
   ============================================================ */

INSERT INTO Student
    (FirstName, SecondName, ThirdName, LastName,
     DateOfBirth, NationalId, Email, PhoneNumber,
     Status, MajorId, AdmissionDate)
VALUES
    ('Rayan', 'Fadhil', 'Ahmed', 'Alharbi',
        '2004-02-15', 'SA200000001', 'rayan.alharbi@student.edu.sa',
        '+966550000001', 'Active',
        (SELECT Id FROM Major WHERE Code = 'CS-BS'), '2023-09-01'),

    ('Abdullah', 'Mohammed', 'Saleh', 'Alqahtani',
        '2003-07-21', 'SA200000002', 'abdullah.alqahtani@student.edu.sa',
        '+966550000002', 'Active',
        (SELECT Id FROM Major WHERE Code = 'CS-BS'), '2022-09-01'),

    ('Faisal', 'Omar', 'Nasser', 'Alzahrani',
        '2005-01-09', 'SA200000003', 'faisal.alzahrani@student.edu.sa',
        '+966550000003', 'Active',
        (SELECT Id FROM Major WHERE Code = 'SE-BS'), '2023-09-01'),

    ('Sara', 'Khalid', 'Ahmed', 'Alghamdi',
        '2004-05-17', 'SA200000004', 'sara.alghamdi@student.edu.sa',
        '+966550000004', 'Active',
        (SELECT Id FROM Major WHERE Code = 'AI-BS'), '2023-09-01'),

    ('Mariam', 'Abdullah', 'Faisal', 'Alotaibi',
        '2002-11-28', 'SA200000005', 'mariam.alotaibi@student.edu.sa',
        '+966550000005', 'Active',
        (SELECT Id FROM Major WHERE Code = 'CS-MS'), '2025-09-01'),

    ('Khalid', 'Saeed', 'Ali', 'Almutairi',
        '2003-03-12', 'SA200000006', 'khalid.almutairi@student.edu.sa',
        '+966550000006', 'Active',
        (SELECT Id FROM Major WHERE Code = 'IT-BS'), '2022-09-01'),

    ('Noura', 'Hassan', 'Mohammed', 'Alshammari',
        '2004-08-03', 'SA200000007', 'noura.alshammari@student.edu.sa',
        '+966550000007', 'Active',
        (SELECT Id FROM Major WHERE Code = 'IS-BS'), '2023-09-01'),

    ('Omar', 'Fahad', 'Abdullah', 'Alenezi',
        '2005-02-25', 'SA200000008', 'omar.alenezi@student.edu.sa',
        '+966550000008', 'Active',
        (SELECT Id FROM Major WHERE Code = 'EE-BS'), '2023-09-01'),

    ('Hind', 'Saleh', 'Omar', 'Alqahtani',
        '2001-09-19', 'SA200000009', 'hind.alqahtani@student.edu.sa',
        '+966550000009', 'Graduated',
        (SELECT Id FROM Major WHERE Code = 'CS-BS'), '2020-09-01'),

    ('Yousef', 'Ahmed', 'Khalid', 'Alharbi',
        '2004-12-06', 'SA200000010', 'yousef.alharbi@student.edu.sa',
        '+966550000010', 'Active',
        (SELECT Id FROM Major WHERE Code = 'ME-BS'), '2023-09-01'),

    ('Lina', 'Mohammed', 'Saeed', 'Alghamdi',
        '2003-04-14', 'SA200000011', 'lina.alghamdi@student.edu.sa',
        '+966550000011', 'OnLeave',
        (SELECT Id FROM Major WHERE Code = 'MATH-BS'), '2022-09-01'),

    ('Majed', 'Abdullah', 'Nasser', 'Alsharif',
        '2004-06-30', 'SA200000012', 'majed.alsharif@student.edu.sa',
        '+966550000012', 'Active',
        (SELECT Id FROM Major WHERE Code = 'ACC-BS'), '2023-09-01'),

    ('Hala', 'Faisal', 'Omar', 'Alqahtani',
        '2005-10-11', 'SA200000013', 'hala.alqahtani@student.edu.sa',
        '+966550000013', 'Active',
        (SELECT Id FROM Major WHERE Code = 'BA-BS'), '2024-09-01'),

    ('Saad', 'Khalid', 'Mohammed', 'Alzahrani',
        '2002-01-22', 'SA200000014', 'saad.alzahrani@student.edu.sa',
        '+966550000014', 'Suspended',
        (SELECT Id FROM Major WHERE Code = 'CS-BS'), '2021-09-01'),

    ('Noor', 'Ahmed', 'Saleh', 'Almutairi',
        '2004-03-08', 'SA200000015', 'noor.almutairi@student.edu.sa',
        '+966550000015', 'Active',
        (SELECT Id FROM Major WHERE Code = 'ENG-BS'), '2023-09-01');


/* ============================================================
   COURSES
   ============================================================ */

INSERT INTO Course
    (Name, Code, Description, CreditHours, DepartmentId, Type)
VALUES
    ('Introduction to Programming', 'CS101',
        'Fundamentals of programming and problem solving.',
        3, (SELECT Id FROM Department WHERE Code = 'CS'), 'Core'),

    ('Object-Oriented Programming', 'CS201',
        'Object-oriented programming concepts and design.',
        3, (SELECT Id FROM Department WHERE Code = 'CS'), 'Core'),

    ('Data Structures', 'CS202',
        'Fundamental data structures and their algorithms.',
        3, (SELECT Id FROM Department WHERE Code = 'CS'), 'Core'),

    ('Database Systems', 'CS301',
        'Relational databases, SQL, normalization, and database design.',
        3, (SELECT Id FROM Department WHERE Code = 'CS'), 'Core'),

    ('Operating Systems', 'CS302',
        'Processes, memory management, file systems, and operating system concepts.',
        3, (SELECT Id FROM Department WHERE Code = 'CS'), 'Core'),

    ('Computer Networks', 'CS303',
        'Computer networking architectures, protocols, and network applications.',
        3, (SELECT Id FROM Department WHERE Code = 'CS'), 'Core'),

    ('Software Engineering', 'CS304',
        'Software development processes, requirements, architecture, and testing.',
        3, (SELECT Id FROM Department WHERE Code = 'CS'), 'Core'),

    ('Artificial Intelligence', 'CS401',
        'Fundamentals of artificial intelligence and intelligent systems.',
        3, (SELECT Id FROM Department WHERE Code = 'CS'), 'Elective'),

    ('Machine Learning', 'CS402',
        'Supervised and unsupervised machine learning techniques.',
        3, (SELECT Id FROM Department WHERE Code = 'CS'), 'Elective'),

    ('Web Development', 'CS403',
        'Client-side and server-side web application development.',
        3, (SELECT Id FROM Department WHERE Code = 'CS'), 'Elective'),

    ('Information Technology Fundamentals', 'IT101',
        'Fundamentals of information technology and computing infrastructure.',
        3, (SELECT Id FROM Department WHERE Code = 'IT'), 'Core'),

    ('Network Administration', 'IT201',
        'Administration and management of computer networks.',
        3, (SELECT Id FROM Department WHERE Code = 'IT'), 'Core'),

    ('Information Security', 'IT301',
        'Security principles, threats, controls, and secure systems.',
        3, (SELECT Id FROM Department WHERE Code = 'IT'), 'Core'),

    ('Systems Analysis and Design', 'CIS201',
        'Analysis and design of information systems.',
        3, (SELECT Id FROM Department WHERE Code = 'CIS'), 'Core'),

    ('Business Intelligence', 'CIS301',
        'Data analytics and decision support systems.',
        3, (SELECT Id FROM Department WHERE Code = 'CIS'), 'Elective'),

    ('Circuit Analysis', 'EE201',
        'Analysis of electrical circuits and fundamental circuit laws.',
        3, (SELECT Id FROM Department WHERE Code = 'EE'), 'Core'),

    ('Calculus I', 'MATH101',
        'Differential and integral calculus.',
        4, (SELECT Id FROM Department WHERE Code = 'MATH'), 'General Education'),

    ('Linear Algebra', 'MATH201',
        'Vectors, matrices, linear transformations, and applications.',
        3, (SELECT Id FROM Department WHERE Code = 'MATH'), 'General Education'),

    ('General Physics I', 'PHYS101',
        'Fundamentals of mechanics, energy, and motion.',
        4, (SELECT Id FROM Department WHERE Code = 'PHYS'), 'General Education'),

    ('Financial Accounting', 'ACC101',
        'Fundamentals of financial accounting and reporting.',
        3, (SELECT Id FROM Department WHERE Code = 'ACC'), 'Core'),

    ('Principles of Management', 'BA101',
        'Fundamental principles and practices of management.',
        3, (SELECT Id FROM Department WHERE Code = 'BA'), 'Core'),

    ('Academic English', 'ENG101',
        'English language skills for academic study.',
        3, (SELECT Id FROM Department WHERE Code = 'ENG-LANG'), 'General Education');


/* ============================================================
   COURSE PREREQUISITES
   ============================================================ */

INSERT INTO CoursePrerequisite (CourseId, PrerequisiteCourseId)
VALUES
    ((SELECT Id FROM Course WHERE Code = 'CS201'),
     (SELECT Id FROM Course WHERE Code = 'CS101')),

    ((SELECT Id FROM Course WHERE Code = 'CS202'),
     (SELECT Id FROM Course WHERE Code = 'CS201')),

    ((SELECT Id FROM Course WHERE Code = 'CS301'),
     (SELECT Id FROM Course WHERE Code = 'CS202')),

    ((SELECT Id FROM Course WHERE Code = 'CS302'),
     (SELECT Id FROM Course WHERE Code = 'CS202')),

    ((SELECT Id FROM Course WHERE Code = 'CS303'),
     (SELECT Id FROM Course WHERE Code = 'CS202')),

    ((SELECT Id FROM Course WHERE Code = 'CS304'),
     (SELECT Id FROM Course WHERE Code = 'CS201')),

    ((SELECT Id FROM Course WHERE Code = 'CS401'),
     (SELECT Id FROM Course WHERE Code = 'CS202')),

    ((SELECT Id FROM Course WHERE Code = 'CS402'),
     (SELECT Id FROM Course WHERE Code = 'CS202')),

    ((SELECT Id FROM Course WHERE Code = 'CS402'),
     (SELECT Id FROM Course WHERE Code = 'MATH201')),

    ((SELECT Id FROM Course WHERE Code = 'IT201'),
     (SELECT Id FROM Course WHERE Code = 'IT101')),

    ((SELECT Id FROM Course WHERE Code = 'IT301'),
     (SELECT Id FROM Course WHERE Code = 'IT201')),

    ((SELECT Id FROM Course WHERE Code = 'CIS301'),
     (SELECT Id FROM Course WHERE Code = 'CIS201')),

    ((SELECT Id FROM Course WHERE Code = 'MATH201'),
     (SELECT Id FROM Course WHERE Code = 'MATH101'));


/* ============================================================
   SEMESTERS
   ============================================================ */

INSERT INTO Semester (Term, StartDate, EndDate)
VALUES
    ('Fall 2024',   '2024-08-18', '2024-12-26'),
    ('Spring 2025', '2025-01-19', '2025-05-29'),
    ('Fall 2025',   '2025-08-24', '2025-12-25'),
    ('Spring 2026', '2026-01-18', '2026-05-28'),
    ('Fall 2026',   '2026-08-23', '2026-12-24');


/* ============================================================
   BUILDINGS
   ============================================================ */

INSERT INTO Building (Name, NumberOfFloors)
VALUES
    ('Computing Building', 4),
    ('Engineering Building', 5),
    ('Science Building', 4),
    ('Business Building', 3),
    ('Humanities Building', 3);


/* ============================================================
   CLASSROOMS
   ============================================================ */

INSERT INTO Classroom
    (Number, BuildingId, Type, FloorNumber, Capacity)
VALUES
    ('101',
        (SELECT Id FROM Building WHERE Name = 'Computing Building'),
        'Classroom', 1, 40),

    ('102',
        (SELECT Id FROM Building WHERE Name = 'Computing Building'),
        'Classroom', 1, 40),

    ('201',
        (SELECT Id FROM Building WHERE Name = 'Computing Building'),
        'Computer Lab', 2, 30),

    ('202',
        (SELECT Id FROM Building WHERE Name = 'Computing Building'),
        'Computer Lab', 2, 30),

    ('301',
        (SELECT Id FROM Building WHERE Name = 'Computing Building'),
        'Lecture Hall', 3, 100),

    ('401',
        (SELECT Id FROM Building WHERE Name = 'Engineering Building'),
        'Laboratory', 4, 35),

    ('402',
        (SELECT Id FROM Building WHERE Name = 'Engineering Building'),
        'Classroom', 4, 50),

    ('101',
        (SELECT Id FROM Building WHERE Name = 'Science Building'),
        'Laboratory', 1, 35),

    ('201',
        (SELECT Id FROM Building WHERE Name = 'Science Building'),
        'Classroom', 2, 50),

    ('301',
        (SELECT Id FROM Building WHERE Name = 'Business Building'),
        'Seminar Room', 3, 30);


/* ============================================================
   COURSE OFFERINGS
   ============================================================ */

INSERT INTO CourseOffering
    (CourseId, SemesterId, Section, Capacity)
VALUES
    ((SELECT Id FROM Course WHERE Code = 'CS101'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2024'), '01', 40),

    ((SELECT Id FROM Course WHERE Code = 'CS201'),
     (SELECT Id FROM Semester WHERE Term = 'Spring 2025'), '01', 35),

    ((SELECT Id FROM Course WHERE Code = 'CS202'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2025'), '01', 35),

    ((SELECT Id FROM Course WHERE Code = 'CS202'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2025'), '02', 30),

    ((SELECT Id FROM Course WHERE Code = 'CS301'),
     (SELECT Id FROM Semester WHERE Term = 'Spring 2026'), '01', 35),

    ((SELECT Id FROM Course WHERE Code = 'CS302'),
     (SELECT Id FROM Semester WHERE Term = 'Spring 2026'), '01', 35),

    ((SELECT Id FROM Course WHERE Code = 'CS303'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2026'), '01', 35),

    ((SELECT Id FROM Course WHERE Code = 'CS304'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2026'), '01', 35),

    ((SELECT Id FROM Course WHERE Code = 'CS401'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2026'), '01', 25),

    ((SELECT Id FROM Course WHERE Code = 'CS402'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2026'), '01', 25),

    ((SELECT Id FROM Course WHERE Code = 'IT101'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2025'), '01', 40),

    ((SELECT Id FROM Course WHERE Code = 'IT201'),
     (SELECT Id FROM Semester WHERE Term = 'Spring 2026'), '01', 35),

    ((SELECT Id FROM Course WHERE Code = 'IT301'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2026'), '01', 35),

    ((SELECT Id FROM Course WHERE Code = 'CIS201'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2025'), '01', 40),

    ((SELECT Id FROM Course WHERE Code = 'CIS301'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2026'), '01', 30),

    ((SELECT Id FROM Course WHERE Code = 'EE201'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2025'), '01', 35),

    ((SELECT Id FROM Course WHERE Code = 'MATH101'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2024'), '01', 50),

    ((SELECT Id FROM Course WHERE Code = 'MATH201'),
     (SELECT Id FROM Semester WHERE Term = 'Spring 2025'), '01', 40),

    ((SELECT Id FROM Course WHERE Code = 'PHYS101'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2024'), '01', 50),

    ((SELECT Id FROM Course WHERE Code = 'ACC101'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2025'), '01', 40),

    ((SELECT Id FROM Course WHERE Code = 'BA101'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2025'), '01', 40),

    ((SELECT Id FROM Course WHERE Code = 'ENG101'),
     (SELECT Id FROM Semester WHERE Term = 'Fall 2024'), '01', 50);


/* ============================================================
   COURSE OFFERING INSTRUCTORS
   ============================================================ */

INSERT INTO CourseOfferingInstructor
    (CourseOfferingId, InstructorId)
VALUES
    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'ahmed.alharbi@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS201' AND s.Term = 'Spring 2025' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'khalid.alzahrani@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS202' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'sara.alqahtani@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS202' AND s.Term = 'Fall 2025' AND co.Section = '02'),
        (SELECT Id FROM FacultyMember WHERE Email = 'khalid.alzahrani@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS301' AND s.Term = 'Spring 2026' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'ahmed.alharbi@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS302' AND s.Term = 'Spring 2026' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'sara.alqahtani@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS303' AND s.Term = 'Fall 2026' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'khalid.alzahrani@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS304' AND s.Term = 'Fall 2026' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'sara.alqahtani@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS401' AND s.Term = 'Fall 2026' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'ahmed.alharbi@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS402' AND s.Term = 'Fall 2026' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'khalid.alzahrani@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'IT101' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'omar.alotaibi@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'IT201' AND s.Term = 'Spring 2026' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'noura.alghamdi@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CIS201' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'maha.alshammari@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'MATH101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'reem.alenezi@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'MATH201' AND s.Term = 'Spring 2025' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'reem.alenezi@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'PHYS101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'yousef.alqahtani@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'ACC101' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'huda.alsharif@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'BA101' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'turki.alghamdi@university.edu.sa')
    ),

    (
        (SELECT co.Id
         FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'ENG101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        (SELECT Id FROM FacultyMember WHERE Email = 'lama.alharbi@university.edu.sa')
    );


/* ============================================================
   COURSE OFFERING SCHEDULES
   Saudi academic week is represented using SUN-THU here.
   ============================================================ */

INSERT INTO CourseOfferingSchedule
    (CourseOfferingId, DayOfWeek, StartTime, EndTime, ClassroomId)
VALUES
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        'SUN', '08:00', '09:30',
        (SELECT cl.Id FROM Classroom cl
         JOIN Building b ON b.Id = cl.BuildingId
         WHERE b.Name = 'Computing Building' AND cl.Number = '101')
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        'TUE', '08:00', '09:30',
        (SELECT cl.Id FROM Classroom cl
         JOIN Building b ON b.Id = cl.BuildingId
         WHERE b.Name = 'Computing Building' AND cl.Number = '101')
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS201' AND s.Term = 'Spring 2025' AND co.Section = '01'),
        'MON', '10:00', '11:30',
        (SELECT cl.Id FROM Classroom cl
         JOIN Building b ON b.Id = cl.BuildingId
         WHERE b.Name = 'Computing Building' AND cl.Number = '102')
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS201' AND s.Term = 'Spring 2025' AND co.Section = '01'),
        'WED', '10:00', '11:30',
        (SELECT cl.Id FROM Classroom cl
         JOIN Building b ON b.Id = cl.BuildingId
         WHERE b.Name = 'Computing Building' AND cl.Number = '102')
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS202' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        'SUN', '10:00', '11:30',
        (SELECT cl.Id FROM Classroom cl
         JOIN Building b ON b.Id = cl.BuildingId
         WHERE b.Name = 'Computing Building' AND cl.Number = '201')
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS202' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        'TUE', '10:00', '11:30',
        (SELECT cl.Id FROM Classroom cl
         JOIN Building b ON b.Id = cl.BuildingId
         WHERE b.Name = 'Computing Building' AND cl.Number = '201')
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS301' AND s.Term = 'Spring 2026' AND co.Section = '01'),
        'MON', '08:00', '09:30',
        (SELECT cl.Id FROM Classroom cl
         JOIN Building b ON b.Id = cl.BuildingId
         WHERE b.Name = 'Computing Building' AND cl.Number = '202')
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS301' AND s.Term = 'Spring 2026' AND co.Section = '01'),
        'WED', '08:00', '09:30',
        (SELECT cl.Id FROM Classroom cl
         JOIN Building b ON b.Id = cl.BuildingId
         WHERE b.Name = 'Computing Building' AND cl.Number = '202')
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'MATH101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        'SUN', '12:00', '13:30',
        (SELECT cl.Id FROM Classroom cl
         JOIN Building b ON b.Id = cl.BuildingId
         WHERE b.Name = 'Science Building' AND cl.Number = '201')
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'MATH101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        'TUE', '12:00', '13:30',
        (SELECT cl.Id FROM Classroom cl
         JOIN Building b ON b.Id = cl.BuildingId
         WHERE b.Name = 'Science Building' AND cl.Number = '201')
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'PHYS101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        'MON', '12:00', '13:30',
        (SELECT cl.Id FROM Classroom cl
         JOIN Building b ON b.Id = cl.BuildingId
         WHERE b.Name = 'Science Building' AND cl.Number = '101')
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'PHYS101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        'WED', '12:00', '13:30',
        (SELECT cl.Id FROM Classroom cl
         JOIN Building b ON b.Id = cl.BuildingId
         WHERE b.Name = 'Science Building' AND cl.Number = '101')
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'BA101' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        'SUN', '14:00', '15:30',
        (SELECT cl.Id FROM Classroom cl
         JOIN Building b ON b.Id = cl.BuildingId
         WHERE b.Name = 'Business Building' AND cl.Number = '301')
    );


/* ============================================================
   COURSE ENROLLMENTS
   ============================================================ */

INSERT INTO CourseEnrollment
    (CourseOfferingId, StudentId, FinalGrade, EnrollmentDate)
VALUES
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10000),
        91.50, '2024-08-20'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10001),
        86.00, '2024-08-20'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10002),
        78.50, '2024-08-21'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10003),
        94.00, '2024-08-21'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'MATH101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10000),
        89.00, '2024-08-20'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'MATH101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10001),
        93.50, '2024-08-20'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'PHYS101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10002),
        81.00, '2024-08-20'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'PHYS101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10003),
        88.50, '2024-08-20'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'ENG101' AND s.Term = 'Fall 2024' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10000),
        96.00, '2024-08-19'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS201' AND s.Term = 'Spring 2025' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10000),
        92.00, '2025-01-21'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS201' AND s.Term = 'Spring 2025' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10001),
        84.50, '2025-01-21'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'MATH201' AND s.Term = 'Spring 2025' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10000),
        90.00, '2025-01-21'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'MATH201' AND s.Term = 'Spring 2025' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10001),
        88.00, '2025-01-21'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS202' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10000),
        95.00, '2025-08-26'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS202' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10001),
        89.50, '2025-08-26'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'IT101' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10005),
        87.00, '2025-08-25'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CIS201' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10006),
        91.00, '2025-08-25'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'EE201' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10007),
        83.50, '2025-08-25'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'ACC101' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10011),
        94.00, '2025-08-25'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'BA101' AND s.Term = 'Fall 2025' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10012),
        89.00, '2025-08-25'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS301' AND s.Term = 'Spring 2026' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10000),
        NULL, '2026-01-20'
    ),
    (
        (SELECT co.Id FROM CourseOffering co
         JOIN Course c ON c.Id = co.CourseId
         JOIN Semester s ON s.Id = co.SemesterId
         WHERE c.Code = 'CS302' AND s.Term = 'Spring 2026' AND co.Section = '01'),
        (SELECT Id FROM Student WHERE UniversityId = 10001),
        NULL, '2026-01-20'
    );


/* ============================================================
   ROLES
   [User] is intentionally empty, therefore UserRole is empty.
   ============================================================ */

INSERT INTO Role (Name)
VALUES
    ('Administrator'),
    ('Faculty'),
    ('Student');


/* ============================================================
   VERIFICATION
   ============================================================ */

PRINT 'Seed completed successfully.';
PRINT 'University IDs should start at 10000.';
PRINT 'Faculty Employee IDs should start at 50000.';
PRINT '[User] and UserRole were intentionally not seeded.';


    COMMIT TRANSACTION;
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;

    THROW;
END CATCH;
GO
