# TT Maintenance

<div align="center">

![T&T Technologia](https://img.shields.io/badge/T%26T-Technologia-00aa66?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?style=for-the-badge&logo=dotnet)
![C#](https://img.shields.io/badge/C%23-89%25-239120?style=for-the-badge&logo=csharp)
![Solidity](https://img.shields.io/badge/Solidity-Blockchain-363636?style=for-the-badge&logo=solidity)
![Docker](https://img.shields.io/badge/Docker-Ready-2496ED?style=for-the-badge&logo=docker)
![License](https://img.shields.io/badge/License-MIT-yellow?style=for-the-badge)
![Stars](https://img.shields.io/github/stars/Techenologia/tt-maintenance?style=for-the-badge)

**Lightweight open source maintenance management backend**  
*Built by T&T Technologia — Sovereign Digital Infrastructure for Africa*

[🚀 Quick Start](#-quick-start) · [📖 Documentation](#-architecture) · [🤝 Contributing](#-contributing) · [🌍 About](#-about-tt-technologia)

</div>

---

## 📋 Overview

**TT Maintenance** is a modular, production-ready ASP.NET Core 8 backend for maintenance management. It combines preventive and corrective maintenance workflows with telecom management, blockchain traceability, and fintech capabilities — all in one unified, open source platform.

Designed for African enterprises and institutions, it addresses real operational needs while being fully deployable anywhere in the world.

### ✨ Key Features

- 🔧 **Maintenance Module** — Preventive and corrective maintenance workflows
- 📡 **Telecom Module** — Telecom infrastructure management
- ⛓️ **Blockchain Module** — On-chain traceability via Solidity smart contracts (Nethereum)
- 💳 **Fintech/Wallet Module** — Multi-currency wallet, TTM token, GTTM carbon credits
- 🔐 **Security** — JWT authentication, role-based access, BCrypt password hashing
- 📊 **Audit Trail** — Full audit logging via `AuditService`
- 🐳 **Docker Ready** — `Dockerfile` + `docker-compose.yml` included
- 🧪 **Tested** — Unit tests in `tests/TT.Backend.Tests`
- 📄 **Swagger UI** — Interactive API docs at `/swagger`

---

## 🏗️ Architecture

```
tt-maintenance/
├── Core/                          # Domain layer
│   ├── Interfaces/                # Service contracts
│   ├── Pipeline/                  # MediatR pipeline (GlobalExceptionMiddleware)
│   └── Security/                  # Roles, JWT helpers
│
├── Infrastructure/                # Data & services layer
│   ├── Data/                      # AppDbContext (EF Core / SQL Server)
│   └── Services/                  # AuthService, AuditService, UserService
│
├── Modules/                       # Feature modules
│   ├── Maintenance/               # Core maintenance management
│   ├── Telecom/                   # Telecom infrastructure
│   ├── Dev/                       # Developer utilities
│   └── Fintech/
│       ├── Blockchain/            # BlockchainService, GTTMService
│       └── Wallet/                # WalletService, MultiCurrencyMintService
│
├── TT.Blockchain/                 # Solidity smart contracts
├── Migrations/                    # EF Core database migrations
├── tests/TT.Backend.Tests/        # Unit & integration tests
│
├── Program.cs                     # App entry point & DI configuration
├── Dockerfile                     # Container definition
├── docker-compose.yml             # Multi-service orchestration
└── audit-security.sh              # Security audit script
```

---

## ⚙️ Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core 8 (.NET 8.0) |
| Language | C# 12 |
| ORM | Entity Framework Core 8 |
| Database | SQL Server (InMemory for tests) |
| Auth | JWT Bearer + BCrypt |
| Validation | FluentValidation |
| Mediator | MediatR 12 |
| Blockchain | Nethereum 6.1 + Solidity |
| Logging | Serilog (Console + File) |
| API Docs | Swashbuckle / Swagger UI |
| Containers | Docker + docker-compose |
| Error Handling | ErrorOr 2.1 |

---

## 🚀 Quick Start

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8)
- [SQL Server](https://www.microsoft.com/sql-server) or Docker
- [Docker](https://www.docker.com/) (optional)

### 1. Clone the repository

```bash
git clone https://github.com/Techenologia/tt-maintenance.git
cd tt-maintenance
```

### 2. Configure the environment

Create an `appsettings.Development.json` file:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TTMaintenance;Trusted_Connection=True;TrustServerCertificate=True;"
  },
  "Jwt": {
    "Key": "your-super-secret-key-minimum-32-characters",
    "Issuer": "TT.Backend",
    "Audience": "TT.Frontend"
  },
  "Cors": {
    "AllowedOrigins": ["http://localhost:3000", "http://localhost:5173"]
  },
  "BlockchainConfig": {
    "RpcUrl": "https://polygon-mumbai.infura.io/v3/YOUR_KEY",
    "ContractAddress": ""
  }
}
```

### 3. Run database migrations

```bash
dotnet ef database update
```

### 4. Start the server

```bash
dotnet run
```

API available at: `http://localhost:5094`  
Swagger UI: `http://localhost:5094/swagger`

### 5. Or run with Docker

```bash
docker-compose up --build
```

---

## 🔑 Authentication

The API uses **JWT Bearer** authentication. Roles available:

| Role | Access Level |
|---|---|
| `Admin` | Full access to all endpoints |
| `Manager` | Access to maintenance + reports |
| `User` | Read-only + own task management |

**Get a token:**
```http
POST /api/auth/login
Content-Type: application/json

{
  "email": "admin@tt.ne",
  "password": "your-password"
}
```

**Use the token:**
```http
Authorization: Bearer <your-jwt-token>
```

---

## 📡 API Endpoints

### Maintenance Module
```
GET    /api/maintenance/tasks          # List maintenance tasks
POST   /api/maintenance/tasks          # Create a task
GET    /api/maintenance/tasks/{id}     # Get task details
PUT    /api/maintenance/tasks/{id}     # Update a task
DELETE /api/maintenance/tasks/{id}     # Delete a task
```

### Telecom Module
```
GET    /api/telecom/assets             # List telecom assets
POST   /api/telecom/assets             # Register an asset
GET    /api/telecom/assets/{id}        # Asset details
```

### Fintech / Wallet
```
GET    /api/wallet/balance             # Get wallet balance (TTM)
POST   /api/wallet/transfer            # Transfer TTM tokens
GET    /api/wallet/transactions        # Transaction history
POST   /api/wallet/mint                # Mint TTM (Admin only)
```

### Blockchain
```
GET    /api/blockchain/status          # Contract status
POST   /api/blockchain/record          # Record event on-chain
GET    /api/blockchain/gttm/balance    # GTTM carbon credit balance
```

---

## 🧪 Running Tests

```bash
dotnet test tests/TT.Backend.Tests
```

---

## 🔒 Security Audit

```bash
chmod +x audit-security.sh
./audit-security.sh
```

This script checks for common vulnerabilities, exposed secrets, and dependency issues.

---

## 🤝 Contributing

Contributions are welcome! Here's how to get started:

1. **Fork** the repository
2. **Create** a feature branch: `git checkout -b feature/your-feature`
3. **Commit** your changes: `git commit -m 'feat: add your feature'`
4. **Push** to the branch: `git push origin feature/your-feature`
5. **Open** a Pull Request

### Contribution Guidelines

- Follow existing code style (C# conventions, clean architecture)
- Add unit tests for new features
- Update documentation if needed
- Use conventional commits (`feat:`, `fix:`, `docs:`, `refactor:`)

### Areas Open for Contribution

- [ ] REST API frontend (React / Vue)
- [ ] Mobile app (React Native)
- [ ] Additional database providers (PostgreSQL, MySQL)
- [ ] Notification service (email, SMS)
- [ ] Advanced reporting module
- [ ] Kubernetes deployment manifests
- [ ] CI/CD GitHub Actions pipeline

---

## 📦 Roadmap

| Version | Status | Features |
|---|---|---|
| v0.1.0 | ✅ Done | Core backend, JWT, Maintenance module |
| v0.2.0 | ✅ Done | Blockchain integration, Wallet, Telecom |
| v0.3.0 | 🔄 In Progress | Frontend dashboard, notifications |
| v0.4.0 | 📋 Planned | Mobile app, offline mode |
| v1.0.0 | 📋 Planned | Production release, full documentation |

---

## 🌍 About T&T Technologia

**T&T Technologia** is a Niger-based tech startup building sovereign digital infrastructure for Africa. Our mission is to create technology solutions that address real African operational needs — from maintenance management to fintech and digital security.

- 🌐 Based in Niamey, Niger
- 💡 Focus: Digital sovereignty, Fintech, Defense Tech
- 🔗 [GitHub](https://github.com/Techenologia)

---

## 📄 License

This project is licensed under the **MIT License** — see the [LICENSE](LICENSE) file for details.

```
MIT License — Copyright (c) 2026 T&T Technologia
```

---

## 🙏 Acknowledgments

Built with ❤️ by the T&T Technologia team.  
Special thanks to the open source community for the amazing tools that made this possible.

---

<div align="center">

**⭐ If this project helps you, give it a star!**

[![GitHub stars](https://img.shields.io/github/stars/Techenologia/tt-maintenance?style=social)](https://github.com/Techenologia/tt-maintenance/stargazers)

</div>
