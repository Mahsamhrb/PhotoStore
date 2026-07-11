# PhotoStore API

A backend Web API for managing photo uploads and storage, built with ASP.NET Core.

## Overview

PhotoStore is a personal backend project created to practice building a real-world Web API using modern .NET technologies.

The project focuses on clean API development, database interaction, validation, and backend fundamentals.

## Tech Stack

* C#
* ASP.NET Core Web API
* Entity Framework Core
* PostgreSQL
* Swagger / OpenAPI
* Dependency Injection
* LINQ
* Clean Architecture

## Features

* Upload, Archive, update photo records
* Store photo information in PostgreSQL
* File upload handling
* Request validation
* API documentation with Swagger
* Database migrations with Entity Framework Core
* Fluent Validation
* Exception & ErrorHandeling
* MiddleWare

## Project Structure

```
PhotoStore
│
├── Controllers
├── Application
├── Domain
├── Infrastructure
└── Program.cs
```

## Getting Started

### Prerequisites

* .NET SDK
* PostgreSQL

### Configuration

Uses ASP.NET Core configuration and User Secrets for managing sensitive settings.

### Run the project

Clone the repository:

```
git clone https://github.com/Mahsamhrb/PhotoStore.git
```

Navigate to the project folder:

```
cd PhotoStore
```

Run the application:

```
dotnet run
```

The API documentation will be available through Swagger.

## Future Improvements

* Authentication and authorization
* Docker support
* storage integration
* Image processing features
* OrderSystem

## Contact

GitHub:
[my-github-link](https://github.com/Mahsamhrb/PhotoStore.git)
