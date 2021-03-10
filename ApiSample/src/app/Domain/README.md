# Domain Layer

This will contain all entities, enums, exceptions, types and logic specific to the domain.
This layer should have no nuget dependencies and be easily tested by unit tests.

## Contains
* Entities / Aggregates
* Value Objects
* Enumerations
* Logic
* Exceptions

## Key Points
* Avoid using data annotations
* Use value objects where appropriate
* Initialise all collections & use private setters
* Create custom domain exceptions

