# Requirements
1. DBs (postgre)
	- libraryHangfire (jobs)
	- library

2. Run command (from VS)
	- update-database 

# Description
App provides CRUD operations over some entities and starts job that sends (well, just faking) emails to users based on their borrowed books. Cannot create users.

# Architecture
Workflow of a request:
Request -> 
Controller -> 
Mediator (validates data with FluentValidator library, via ValidationBehaviour) -> 
Handler (business logic) -> 
Repository (data access)

