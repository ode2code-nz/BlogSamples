# Presentation Layer - API

This layer contains the ASP.Net Core Web API artifacts and is responsible for converting the command/query results into HTTP action results with HTTP status codes, etc. 

# Features

## Health checks
See [Microsoft docs](https://docs.microsoft.com/en-us/dotnet/architecture/microservices/implement-resilient-applications/monitor-app-health) for purpose and benefits of real-time information about the state of your containers and microservices and the dependencies they rely on.

Navigate to the following URLs:
* /health/live: liveness check - a fast check of whether app is alive or dead.
* /health/ready: readiness check - slightly slower check that also pings app's dependencies, such as SQL Server or messaging.


# Application Services Layer

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project.
This layer defines interfaces that are implemented by outside layers. 
For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.

## Contains
* Interfaces
* Models
* Logic
* Commands / Queries
* Validators
* Exceptions

## Key Points
* Independent of infrastructure and data access concerns