# VinDex

VinDex is a Full-Stack VIN (Vehicle Identification Number) lookup tool and management system. Built with an **ASP.NET Core (C#)** API and an **Angular** frontend, VinDex allows users to decode VINs, retrieve extensive vehicle data from the NHTSA database, store lookup history in PostgreSQL, and optionally view associated safety recalls and estimated market values.

---

## Features

- **Detailed VIN Decoding**: Retrieves engine specifications, manufacturing details, and base vehicle information via the NHTSA API.
- **Safety Recalls Integration**: Fetches active safety recalls associated with a specific vehicle model and year.
- **Market Value Estimation**: Custom service to estimate vehicle market values based on decoded specifications.
- **Intelligent Caching**: In-memory caching for VIN decodes and recall lookups to minimize external API calls and improve performance.
- **Robust Authentication**: Secure user login and authorization system.
- **Modern Frontend Interface**: Responsive, user-friendly Angular application for seamlessly searching VINs and viewing results.
- **Containerized Deployment**: Fully dockerized backend, frontend, and database environments for easy setup.

## Technology Stack

### Backend (API)
- **Framework**: .NET 9.0 (ASP.NET Core Web API)
- **Database**: PostgreSQL
- **Architecture**: Repository Pattern, Dependency Injection, API Caching

### Frontend (Web App)
- **Framework**: Angular
- **Language**: TypeScript
- **Web Server**: Nginx (for Dockerized production builds)

### Infrastructure
- **Containerization**: Docker & Docker Compose

---

## Project Structure

```text
VinDex/
├── frontend/               Angular CLI application
│   ├── src/app/            Components, Services, Models, Guards
│   ├── Dockerfile          Multi-stage build (Node -> Nginx)
│   └── nginx.conf          Nginx reverse proxy configuration
├── VinDex.Api/             ASP.NET Core API
│   ├── Controllers/        REST API Endpoints (Auth, VIN)
│   ├── Services/           Business logic (Caching, NHTSA integration)
│   ├── Data/               EF Core DbContext & Repositories
│   ├── Models/             Domain Entities and DTOs
│   └── Dockerfile          .NET isolated environment
├── docker-compose.yml      Orchestrates Postgres, Backend API, and Frontend
└── run.ps1                 Script for quick execution
```