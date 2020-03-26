This is a solution for education purposes.

It consist of the following projects.
- Api ASP.NET Core web api.
- Blazor - Server side blazro frontend.
- Core - Shared project with models
- Core tests - Unit test for the core project.

Features include:
- CI/CD pipeline to azure

Api:
- Entity Framework Core - include one to many relationship
- EF Migrations is added in the data folder. Run "dotnet ef migrations add Initial" aswell as "dotnet ef database update".
- Seed funtion will populate database.
- Logging - In program.cs, startup and controller. 
- CRUD operations

Blazor:
- SignalR is included 
- Formvalidation
- Bootstrap

Core:
- Model validation

CoreTest:
- Unittests

ToDo: 
Add Areas - add a new section with puzzles
Add Swagger
Add Authorization
Add complex form validation
Add integration test and test on API controller.
