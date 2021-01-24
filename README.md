# CodeBase

## Description
CodeBase is a web application that offers users to learn by reading materials on different topics from the programing world. Those materials are in the form of individual lectures gathered in courses. The user needs to purchase those courses first in order to read the lectures. If purchased those courses will be added to his personal library where he can access them anytime. To every course there is also a cheatsheet that holds summarised information from all the lectures in that course.

## Functionality
### Guest Users
- Login, Register
- View all courses and lectures in each course but is unable to read the lectures
### Logged in Users
- Purchase a course
- Read lectures
- Read cheatsheet
- Create comments in a lecture
### Users in role "Administrator"
- Have all the functionality of a non-administrator user
- Manage (create, read, update, delete) all the database entities

## Technologies
- .NET Core 3.1
- ASP.NET Core 3.1
- Entity Framework Core 3.1
- xUnit, Moq
- Bootstrap
- JQuery, JavaScript, tinyMCE
- Automapper

## Local setup
1. Open CodeBase.sln
2. Change the DefaultConnection string in appsettings.json if necessary
2. Press Ctrl + F5 to run the app
