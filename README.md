# Finance Tracker

A modern, containerized personal finance management application. This repository uses a monorepo structure, currently housing the .NET backend and PostgreSQL database, with a Kotlin-based frontend planned for the future. The project features a fully automated Docker setup with "on-the-fly" Entity Framework Core migrations.

## Tech Stack

**Backend**
* **Framework:** .NET 10.0 (REST API)
* **Database:** PostgreSQL 15 (Alpine)
* **ORM:** Entity Framework Core 10

**Frontend (Upcoming)**
* **Language/Framework:** Kotlin

**Infrastructure & Tools**
* **Containerization:** Docker & Docker Compose
* **Version Control:** Git

---

## Getting Started

### 1. Prerequisites
To run and contribute to this project, ensure you have the following installed on your machine:
* [Docker Desktop](https://www.docker.com/products/docker-desktop/) (must be running)
* [Git](https://git-scm.com/) (to clone the repository and manage version control)

*Note: You do not need to install the .NET SDK or PostgreSQL locally to run the backend.*

### 2. Environment Configuration
This project uses environment variables for secure and flexible configuration. 

Create your local `.env` file by copying the provided example from the root directory:

```bash
cp .env.example .env
```

Open the `.env` file and fill in your custom values, especially:
* `DB_PASSWORD`: Ensure this is secure if deploying to production.
* `JWT_SECRET`: Must be at least 32 characters long.
* `PORT_API` and `PORT_DB`: Change these if the default ports (5200 and 5935) are already in use on your host machine.

### 3. Initial Build
For the first time (or after changing Dockerfiles/adding new packages), build and start the stack:

```bash
docker-compose up --build
```

---

## Architecture Overview

When you run Docker Compose, it orchestrates the following backend containers:

1. **`db`**: Initializes the PostgreSQL database. It includes a healthcheck to ensure it is ready to accept connections before other services start.
2. **`migration_tool`**: A short-lived utility container. It waits for the DB to be healthy, installs the `dotnet-ef` CLI, restores dependencies, and applies the latest database migrations. Once done, it exits gracefully (Exit Code 0).
3. **`api`**: The main .NET application. It waits for the `migration_tool` to finish successfully before starting, ensuring that the database schema is always up-to-date before handling any HTTP requests.

---

## Ports & Access

By default (based on `.env.example`), the services are exposed as follows:

* **API Endpoint:** `http://localhost:5200`
* **Scalar:** `http://localhost:5200/scalar` (Available only when `ASPNETCORE_ENVIRONMENT=Development`)
* **PostgreSQL:** `localhost:5935` (Use this port to connect via external tools like DBeaver or pgAdmin)

---

## Development & Git Workflow

### Monorepo Structure
The project is organized into separate domains:
* `/src/backend/Api` - .NET 10.0 REST API
* `/src/frontend/...` - (Planned) Kotlin frontend

### Adding New Backend Migrations
Since the containerized environment handles applying migrations, you can generate them locally using standard EF Core CLI tools inside the backend directory:

```bash
cd src/backend/Api
dotnet ef migrations add NameOfYourNewMigration
```

After generating the migration files, the `migration_tool` container will automatically detect and apply the new migration on the next startup.

### Host Isolation (Volume Strategy)
This setup uses a combination of bind mounts and anonymous volumes:
* The source code is mounted using `.:/src` to allow for rapid development without rebuilding the image.
* To prevent Linux build artifacts from polluting your Windows/macOS host, the `/obj` and `/bin` directories are isolated inside Docker-managed anonymous volumes. 

### Version Control (.gitignore)
Before committing your code, ensure your `.gitignore` includes at least the following to prevent pushing sensitive data or heavy artifacts to the repository:

```text
# Environments
.env

# .NET Build results
[Bb]in/
[Oo]bj/

# Docker DB Data (if mapped locally in the future)
db_data/
```

## Security Notes
* **Never commit your `.env` file to version control.** It should be ignored by Git.
* Ensure `JWT_SECRET` is strong and securely managed in production environments.

---

## Ready to go?
To simply run the project on a daily basis (once configured), just run:

```bash
docker-compose up
```
