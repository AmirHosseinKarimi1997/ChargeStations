
# GreenFlux Assignment

This is GreenFlux solution which is created with ASP.NET Core following the principles of Clean Architecture.

## Technologies

* [ASP.NET Core 6](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core?view=aspnetcore-6.0)
* [Entity Framework Core 6](https://docs.microsoft.com/en-us/ef/core/)
* [MediatR](https://github.com/jbogard/MediatR)
* [FluentValidation](https://fluentvalidation.net/)
* [NUnit](https://nunit.org/), [FluentAssertions](https://fluentassertions.com/), [Moq](https://github.com/moq) 
* [SqlServer Database](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)

### Database Configuration

The solution is configured to use an in-memory database by default. This ensures that you will be able to run the solution without needing to set up additional infrastructure ( SQL Server).

If you would like to use SQL Server, you will need to update **GreenFlux.Presentation.Api\appsettings.json** as follows:

```json
  "UseInMemoryDatabase": false,
```

Verify that the **DefaultConnection** connection string within **appsettings.json** points to a valid SQL Server instance. 

When you run the application the database will be automatically created (if necessary) and the latest migrations will be applied.

### Database Migrations

To use `dotnet-ef` for your migrations please add the following flags to your command (values assume you are executing from repository root)

* `--project src\Infrastructure\Infrastructure` (optional if in this folder)
* `--startup-project src\Presentation\GreenFlux.Presentation.Api`
* `--output-dir Persistence\Migrations`

For example, to add a new migration from the root folder:

 `dotnet ef migrations add "SampleMigration" --project src\Infrastructure\Infrastructure --startup-project src\Presentation\GreenFlux.Presentation.Api --output-dir Persistence\Migrations`

## Overview

### Domain

This will contain all entities, domain exceptions, repository interfaces, types and logic specific to the domain layer.

### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers (except from repository interfaces). For example, if the application need to access a DateTime service, a new interface would be added to application and an implementation would be created within infrastructure.

### Infrastructure

This layer contains classes for accessing external resources such as file systems, database and so on.

### Presentation\Api

This layer is an ASP.NET Core 6 WebApi. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *ServiceRegisteration.cs* should reference Infrastructure.

## Tests
There are 2 different types of test in this section. Domain.UnitTests is responsible for unit testing the domain logic by different unit tests. Here, I have used InMemory version of entity framework for unit testing.
Also we have another project for Application Integration Tests called Application.IntegrationTests. In this types of tests, we should use a real sql database, but in order to being easy for running the application, I have used in memory version of entity framework here as well. So we can not run all the application integration tests with run all test tools in TestExplorer.