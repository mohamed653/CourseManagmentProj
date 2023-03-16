# Course Management System

This is a course management system built using ASP.NET Core. The system allows for the management of courses, instructors, and students. The frontend of the system is built using HTML, CSS, Bootstrap, JavaScript, and Ajax. The backend of the system uses Microsoft SQL Server for the database and implements the code first approach. The Identity framework is used for user authentication and authorization.

## Installation
To run the course management system, you will need to have the following installed:

⋅⋅* Visual Studio 2019 or later
⋅⋅* .NET Core SDK 3.1 or later
⋅⋅* Microsoft SQL Server 2017 or later

Once you have these installed, you can follow these steps to set up the system:

1. Clone the repository to your local machine.
2. Open the solution file in Visual Studio.
3. Build the solution to restore any missing packages.
4. In the appsettings.json file, update the connection string to match your SQL Server configuration.
5. Open the Package Manager Console and run the following command to create the database:
sql

```
Update-Database
```
Run the application in Visual Studio.

## Features
The course management system allows for the following features:

Registration and login for students and instructors using the Identity framework.
Creation, updating, and deletion of courses by instructors.
Enrollment and unenrollment of students in courses.
Viewing of courses by students and instructors.
Viewing of students enrolled in a course by instructors.

## Technologies Used
The following technologies were used in the development of the course management system:

ASP.NET Core
HTML
CSS
Bootstrap
JavaScript
Ajax
Microsoft SQL Server
Code First approach
Identity Framework

## Contributing
Contributions to the course management system are welcome. To contribute, please fork the repository, make your changes, and submit a pull request.
