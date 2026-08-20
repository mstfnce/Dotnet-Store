# Dotnet Store

Dotnet Store is an ASP.NET Core MVC e-commerce application built with product management, category browsing, cart operations, order flow, authentication, role-based authorization, and an admin dashboard.

The UI has been refreshed and translated to Turkish for the end-user experience.

## Features

- Product listing by category
- Product detail pages
- Shopping cart management
- Checkout and order completion flow
- Admin dashboard
- Product, category, and slider management
- User registration and login
- Role-based authorization with ASP.NET Core Identity
- Password reset email infrastructure
- Microsoft SQL Server database support

## Tech Stack

- .NET 9
- ASP.NET Core MVC
- Entity Framework Core
- Microsoft SQL Server
- ASP.NET Core Identity
- Bootstrap
- Iyzipay
- Docker & Docker Compose

## Project Structure

```text
Controllers/      MVC controllers
Data/             Entities and DbContext
Migrations/       Entity Framework Core migrations
Models/           View models and form models
Services/         Cart and email services
ViewComponents/   Navbar and slider components
Views/            Razor views
wwwroot/          CSS, JavaScript, images, and static assets
```

## Documentation

Detailed technical documentation (Turkish) lives in [`docs/`](docs/README.md):

| Doc | Topic |
|---|---|
| [01 — Mimari](docs/01-mimari.md) | Layers, request pipeline, DI, routing |
| [02 — Veri Modeli](docs/02-veri-modeli.md) | Entities, relations, migrations, seed data |
| [03 — Controller ve Rotalar](docs/03-controller-ve-rotalar.md) | Full endpoint and authorization reference |
| [04 — Servisler](docs/04-servisler.md) | Cart, image and email services, view components |
| [05 — Kimlik Doğrulama](docs/05-kimlik-dogrulama.md) | Identity configuration, roles, account flows |
| [06 — Sipariş ve Ödeme](docs/06-siparis-ve-odeme.md) | Cart to checkout to payment, end to end |
| [07 — Kurulum ve Çalıştırma](docs/07-kurulum-ve-calistirma.md) | Setup, configuration, Docker, tests |
| [08 — Geliştirme Notları](docs/08-gelistirme-notlari.md) | Known gaps and technical debt |

## Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- Microsoft SQL Server (local, remote, or via Docker)
- `dotnet-ef` tool for running migrations: `dotnet tool install --global dotnet-ef`

## Getting Started

Clone the repository:

```bash
git clone https://github.com/mstfnce/Dotnet-Store.git
cd Dotnet-Store
```

Restore dependencies:

```bash
dotnet restore
```

Apply database migrations:

```bash
dotnet ef database update
```

Run the application:

```bash
dotnet run
```

The application will be available at the localhost URL shown in the terminal.

## Running with Docker

The project also ships with a multi-stage `Dockerfile` and a `docker-compose.yml` that runs the app together with a SQL Server container.

```bash
docker compose up --build
```

On first run, apply the migrations against the containerized database (from the host machine):

```bash
dotnet ef database update --connection "Server=localhost,1433;Database=DotnetStoreDb;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=True"
```

The app will then be available at `http://localhost:8080`. See [`DOCKER-REHBER.md`](DOCKER-REHBER.md) (Turkish) for a more detailed walkthrough and troubleshooting notes.

## Database

The application uses Microsoft SQL Server with Entity Framework Core. The connection string should be defined in `appsettings.json` or `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=DotnetStoreDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

After configuring the connection string, apply the migrations:

```bash
dotnet ef database update
```

The application seeds basic roles and sample users on startup. For real usage, update the default seed user credentials before deployment.

## Email Configuration

SMTP settings are required for email-based flows such as password reset. For local development, use `appsettings.Development.json`:

```json
{
  "Email": {
    "Host": "smtp.example.com",
    "Username": "mail@example.com",
    "Password": "your-password"
  }
}
```

`appsettings.Development.json` is ignored by Git and should not be committed.

## Git Notes

The following files and folders should stay out of source control:

- `bin/`
- `obj/`
- `build-check/`
- `appsettings.Development.json`
- Local database files

## License

This project was developed for education and portfolio purposes.
