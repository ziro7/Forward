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
- Swagger enabled

Blazor:
- SignalR is included 
- Formvalidation
- Complex form validation 
- Custom data validation
- Model logic validation
- Bootstrap
- Login funktionality 

Core:
- Model validation (replaced with blazor framework model validation)

CoreTest:
- Unittests - TODO : Need to update

ToDo: 
- Add Authorization - both blazor and api. (bearer token maybe)
- Add integration test and test on API controller. xUnit - Moq
- Add tips and tricks items
- microservices and container

Out of scope:
- Areas - Decided against using areas as it don't make much sense in my app at this point. Can add later.
