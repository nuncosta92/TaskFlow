# TaskFlow

TaskFlow is an API for managing personal tasks, built with C# and .NET 8.  
It allows creating, assigning, and tracking tasks for different users.

## Technologies Used
- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (SQLite)
- JWT Authentication
- Swagger / Swashbuckle
- AutoMapper
- FluentValidation (coming soon)
- Docker (coming soon)
- GitHub Actions para CI/CD (coming soon)

## Project Structure

``` bash
TaskFlow/
│
├── TaskFlow.API           # API and Controllers
├── TaskFlow.Application   # Business Logic / Use Cases
├── TaskFlow.Domain        # Entities and Interfaces
├── TaskFlow.Infrastructure# Data Persistence and External Services
└── TaskFlow.sln           # Main Solution
``` 


## Main Features
- [ x ] User Registration
- [ x ] Login and JWT Token Generation
- [ x ] CRUD for Tasks (Create, Read, Update, Delete)
- [ ] Task Filtering and Sorting
- [ ] Unit Testing Implementation
- [ ] Docker and Azure Deployment (coming soon)

## How to Run the Project
1. Clone the repository.
2. Open the solution in Visual Studio 2022+.
3. Run the `TaskFlow.API` project.
4. Access the Swagger UI at [https://localhost:7162/swagger](https://localhost:7162/swagger).

---

This project is under development 🚀
