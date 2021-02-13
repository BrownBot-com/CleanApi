# CleanApi
An attempt to create a fresh secure, scalable and testable ASP.Net 5 Web API solution structure that follows the Clean Architecture principles (somewhat).

The base structure is as follows:
### Clean.Api
The main ASP.Net Core API, only contains the web and DI configuration, some filters, SecurityContext implementation and basic controllers.

### Clean.Api.Common
Just the custom exceptions that get thrown in the logic processors and caught by a filter in the API at this stage.

### Clean.Api.Contracts
The communication contracts sent too and from the API endpoints.

### Clean.Api.DataAccess
Entity Framework implementation and migrations.

### Clean.Api.DataAccess.Models
The data entity models

### Clean.Api.LogicProcessors
Where the real work happens, all the business logic and plumbing to EF lives in here.

### Clean.Api.Mapping
Automapper profile classes to map from the data entities to the response contracts.

### Clean.Api.Security
Classes and interfaces for token generation and storage. 

It implements it's own JWT token/refresh token authentication system, you probably want to offload this to a dedicated identity server.

This relies on an environment variable [ConnectionStrings:clean-api-db] being set with the backing database details.
