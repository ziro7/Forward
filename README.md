This is a solution for education purposes.

It is developed in ASP.Net Core 3.0 and consist of the following projects:
- ASP.NET Core web api with CV CRUD operations.
- Integrations tests project for the CV CRUD operation project.
- Blazor - Server side blazor frontend.
- Core - Shared project with models
- Core tests - Unit test for the core project.
- ASP.NET Core web api with Graph Route endpoints.

The solution is a page about me, where there is some pages about hobbies, a CV part and later a section on coding puzzles i might not add until later. The CV part is the main are of focus, as it talk to the backend.
The data model is fairly simple and only consist of two classes - a "job" which have a list of 0 to many "experiences" or job funktions. This was done to add a bit to the complexity, as the CRUD operations was a small bit more complicated and the form validation on the front end was also a bit more involved. 

To run the solution
1. Download/Clone the solution
2. Go to the Console and type "dotnet ef migrations add Initial"
2.1 also type "dotnet ef migrations add CreateIdentitySchema" (Identity database)
3. In the same console run "dotnet ef database update"
(still need to test if this works or something else is needed.)

Features include:
- CI/CD pipeline to azure - It is not published so currently it only build continueously.

Api - ASP.Net Core 3.0 backend:
- Entity Framework Core - include a one to many relationship.
- EF Migrations is added in the data folder. 
- Seed funtion will populate database if none is allready there.
- Logging - In program.cs, startup and controller. 
- CRUD operations is enabled. 
- Swagger enabled to have a better overview of the API.
- Authorization is enabled and accesstoken is required to access data. The Azure Active Directory is used to generate accesstokens, and the frontend is set as a party that can receive an accesstoken.
- Added integration test of the jobController.

Blazor frontend:
- SignalR is included in blazor and is added in the configure method in startup.
- Formvalidation - Using Blazor's build in formvalidation in the AddJobDialog component as it only create a "job" without a job function.
- Complex form validation - Extending the built in functionality to enable validation of the nested class structure in EditCV page, as it handles both job and experiences.
- Bootstrap - Too make the page better looking I have added some bootstrap code, aswell as some css, and load some icons etc.
- Login funktionality - Using ASP.Net Identity cookie funktionality to add a cookie when logging in. When logging out the cookie is removed. The user will only see authorized content.
- Blocking pages from unathorized users (when setting the url to the subpages.)
- JobService that call the API now include a bearer token in the header of the request.

Core - Class library:
- Custom data validation - Added a custom validation so the beginning job date can't be before I was born - Se StartDateValidator. This validation still uses Blazors validation engine.
- Model logic validation - Added a custom ValidationResult where the data on the job and the experience on that job is being valuated.

CoreTest - Unit test library:
- Unittests - TODO : Need to update

ToDo: 
- remove the core.tests project.
- Add integration test for API (route one). 
- Add a resource table to get rid of CA1303
- Add tips and tricks items 
- Maybe seed user database with an admin user and make add/remove/edit only valid for the user while view can be seen by all.

Out of scope:
- Areas - Decided against using areas as it don't make much sense in my app at this point. It also mainly used in a project that have all of the MVC parts, where my frontend only have pages (views) and services (controllers) and backend only have controllers as the models are in the core solution. As I added ASP.Net Identity to enable login/logout functionality it was added in an area so technically it is included.
- Microservices and container - I have used the idea of microservices and each API is fokused on 1 thing (except the ForwardBackend which also hold identity - but will be discussed in the report). I have not added docker support, which have not been a priority for me as, the project is not expected to launch on multiple platforms / enviroments and send to other people.
- Unit test of blazor pages/components. It is a backend course, so wanted to fokus on backend functionality.
