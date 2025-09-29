# Glossary Management System

A .NET 9 glossary management API implementing Domain-Driven Design with CQRS pattern, JWT authentication, and SQL Server persistence.

## Build & Run Instructions

### Prerequisites
- .NET 9 SDK
- SQL Server LocalDB
- Entity Framework Core 8 (auto-installed via NuGet restore)

### Setup
1. Extract the ZIP file
2. Navigate to solution directory:
```bash
   cd GlossaryManagement
   dotnet restore
```
### Run the application:
```bash
   cd GlossaryManagement.WebAPI
   dotnet run
```   
### The application will:

1. Create database automatically
2. Seed 2 test authors and 3 published terms
3. Start API on http://localhost:5210 ( swagger UI is available with this URL ) 

### Test Credentials
```bash
Email: admin@gmail.com | Password: admin
Email: admin2@gmail.com | Password: admin2
```
Login at /api/Auth/login to receive JWT token for protected endpoints.

## Assumptions

1. Authors must authenticate to create, modify, or delete terms
2. New terms start in Draft status
3. Publishing requires: 30+ character description, no forbidden words ("lorem", "test", "sample"), Draft terms only
4. Only Draft terms can be deleted; only Published terms can be archived
5. Authors can only manage their own terms
6. Public users can view all Published terms (alphabetically sorted)
7. JWT tokens expire after 1 hour

## Design Decisions

### Domain-Driven Design Architecture

- Separate aggregates for Author and Term (not nested) for independent querying and scalability
- Value Objects (AuthorId, TermId) for type safety and domain clarity
- Business rules encapsulated in entities

### CQRS Pattern

- Commands for state changes (CreateTerm, PublishTerm, etc.)
- Queries for data retrieval (GetTerms, GetTermsByStatus, etc.)
- MediatR library for command/query routing

### Persistence

- Entity Framework Core 8 for object-relational mapping
- Code-first approach with EF migrations
- Value object conversions configured in entity configurations
- Automatic database creation and seeding on first run

### Repository Pattern

- Interfaces in Domain layer, implementations in Infrastructure
- Maintains dependency inversion principle

### Security

- JWT for stateless, scalable authentication
- BCrypt for password hashing
- Authorization checks in business logic layer

## Project Structure

- Domain: Entities, Value Objects, Repository Interfaces (no dependencies)
- Application: Commands, Queries, Handlers, DTOs (depends on Domain)
- Infrastructure: EF Core, Repositories, Authentication (depends on Application)
- WebAPI: Controllers, configuration (composition root)

## API Endpoints

### Public: 
- GET /api/term - All published terms

### Protected (require JWT):
- POST /api/create - Create term
- PUT /api/publish/{id} - Publish term
- PUT /api/archive/{id} - Archive term
- DELETE /api/delete/{id} - Delete draft term
- GET /api/term/my-terms - Author's terms
- GET /api/term/status/{status} - Filter by status
