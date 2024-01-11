# How to run
docker compose up --build

# Description
App provides CRUD operations over some entities and starts job that sends (well, just faking) emails to users based on their borrowed books. Users are seeded.

# Architecture
Workflow of a request:
Request -> 
Controller -> 
Mediator (validates data with FluentValidator library, via ValidationBehaviour) -> 
Handler (business logic) -> 
Repository (data access) ->
EF Core

