# University Management System

## 1. Purpose

The University Management System (UMS) is a web application that enables
universities to manage students, instructors, departments, courses,
enrollment, grades, attendance, tuition payments, announcements, and
administrative tasks from a centralized platform.

The system should reduce paperwork while allowing different user roles
to securely access only the information relevant to them.

------------------------------------------------------------------------

## 2. User Roles

### Administrator

Responsible for configuring and maintaining the entire university.

**Permissions:**

-   Manage users
-   Manage departments
-   Manage courses
-   Manage semesters
-   Manage classrooms
-   Manage student records
-   Manage instructors
-   View reports
-   Configure system settings

### Student

-   Register
-   Login
-   View profile
-   Register courses
-   Drop courses
-   View grades
-   View attendance
-   View announcements
-   Download transcripts
-   Update personal information

### Instructor

-   View assigned courses
-   View enrolled students
-   Record attendance
-   Enter grades
-   Upload course materials
-   Post announcements
-   View teaching schedule

### Department Head

-   Approve courses
-   Assign instructors
-   Review department reports
-   View student statistics

------------------------------------------------------------------------

## 3. Functional Requirements

### Authentication

#### Registration

Fields:

-   First name
-   Last name
-   Email
-   Student ID
-   Password
-   Confirm Password

Rules:

-   Email must be unique.
-   Student ID must be unique.
-   Password must contain at least 8 characters.
-   Passwords must be securely hashed before storage.

#### Login

Supports email/password, Remember Me, optional 2FA, and account lockout.

#### Password Reset

Users can request a reset link and securely reset their password.

### Core Modules

-   User Management
-   Department Management
-   Semester Management
-   Course Management
-   Classroom Management
-   Instructor Management
-   Student Management
-   Course Registration
-   Attendance
-   Grade Management
-   GPA Calculation
-   Timetable
-   Announcements
-   Transcript Generation
-   Reports
-   Dashboards
-   Search
-   Notifications

### Business Rules

-   One active semester at a time.
-   Unique course codes.
-   No timetable conflicts.
-   Registration requires prerequisites.
-   Registration must be within the allowed period.
-   No duplicate enrollments.
-   Classroom double-booking is prohibited.
