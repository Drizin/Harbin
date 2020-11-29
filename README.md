# Description

Harbin is a .NET Layered Architecture with support for distributed databases (read replicas) - using Dapper, Generic Repositories (Extensible and Mockable), and Code Generation (CodegenCS).

This repository contains a few reusable components and sample project(s) showing how to use those components:

Project | Description
------------ | -------------
[**Infrastructure.Database**](https://github.com/Drizin/Harbin/tree/master/src/Infrastructure.Database) | Data Access library (based on [Dapper](https://github.com/StackExchange/Dapper), [Dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD/), and [DapperQueryBuilder](https://github.com/Drizin/DapperQueryBuilder)) which implement Repositories (Generic Repository Pattern) and can be used over distributed databases and/or read-replicas.
[**AdventureWorks.Core.Domain**](https://github.com/Drizin/Harbin/tree/master/src/AdventureWorks.Core.Domain) | AdventureWorks (sample database) - Domain Layer (only Entities) 
[**AdventureWorks.Core.CoreDatabase**](https://github.com/Drizin/Harbin/tree/master/src/AdventureWorks.Core.CoreDatabase) | AdventureWorks - Data Layer (aka Persistence Layer) - where we define connections, custom Queries and DbCommands. Also includes [CodegenCS](https://github.com/Drizin/CodegenCS/) scripts to extract the database schema and generate Entities in Domain Layer.
[AdventureWorks.Core.Application](https://github.com/Drizin/Harbin/tree/master/src/AdventureWorks.Core.Application) | Application Layer (Services)
[AdventureWorks.Core.Tests](https://github.com/Drizin/Harbin/tree/master/src/AdventureWorks.Core.Tests) | Unit Tests which tests the Application Layer (Services) but mocking Database Connection or Repositories


#  Design Principles

Harbin architecture is mostly based on traditional 3-tiered architecture (mostly for being pragmatic and keeping things simple for data-driven applications), but borrows some ideas from Domain-Driven Design (and Onion Architecture).

It was designed based on the following ideas (from lowest layer to top):
- Like DDD/Onion, Domain Model (mostly Entities, since this is still a data-driven architecture) is the lowest layer (don't depend on any other layer) and can contain business logic, but no data access.
- Like DDD/Onion, business logic which doesn't fit in the Domain Model should be in upper layers. But we avoid distinction between "Domain Services" and "Application Services" because it's kind of gray area.
- The Data Layer contains Repositories and CRUD, and it's a lower layer which depend only on Domain Entities. So it doesn't implement any interface defined in upper layers or elsewhere.
- Since the Data Layer doesn't use interfaces we avoid code repetition and upper layers can use Data Layer without Dependency Injection or Dependency Inversion. Like traditional 3-tier architecture.
- Application Layer (Services) can use Domain Model but can also use Data Layer, so it can access data stores.
- Data Layer uses a [Database Library](https://github.com/Drizin/Harbin/tree/master/src/Infrastructure.Database) which offers "bare metal" access to Dapper/ADO.NET but also helps to manage multiple database connections (e.g. distributed databases, microservices, heterogeneous databases) or to differentiate masters vs read-only replicas.
- This Database Library makes it easy to manage multiple database connections, including connections to read-only replicas, and separation between Queries (read-only) and DbCommands (read-write), as proposed by CQRS.
- This Database Library implements Generic Repository Pattern (using libraries [Dapper.FastCRUD](https://github.com/MoonStorm/Dapper.FastCRUD/) and [DapperQueryBuilder](https://github.com/Drizin/DapperQueryBuilder)) to make your CRUD as easy as possible - you just have to add attributes to your Domain Entities.
- Even though Data Layer is not based on interfaces it's still fully mockable - any method can be mocked using inheritance, without the hassle of having to write interfaces/classes.
- Data Layer includes [CodegenCS (Code Generator)](https://github.com/Drizin/CodegenCS/) scripts to automatically extract the database schema and generate POCO classes. Easy to plug your own database.


## License
MIT License