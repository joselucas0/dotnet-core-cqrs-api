# 🏗️ .NET Core REST API — CQRS + DDD + Clean Architecture

> A .NET Core REST API implementing **CQRS** (Command Query Responsibility Segregation) with **Domain-Driven Design** and **Clean Architecture** principles.

![Build](https://github.com/kgrzybek/sample-dotnet-core-cqrs-api/workflows/Build%20Pipeline/badge.svg)

---

## 🇺🇸 English

### 📖 About

This project is a study and reference implementation of a **.NET Core REST API** applying real-world architectural patterns:

- **CQRS** — separates read and write models for optimized data access
- **Domain-Driven Design (DDD)** — rich domain model with encapsulated business rules
- **Clean Architecture** — clear separation of concerns across layers
- **Outbox Pattern** — reliable domain event publishing for integration scenarios

The API manages **Customers** and **Orders**, demonstrating how these patterns work together in a cohesive application.

### 🏛️ Architecture

The solution follows [Clean Architecture](http://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) with four well-defined layers:

![Clean Architecture](docs/clean_architecture.jpg)

| Layer | Responsibility |
|---|---|
| **API** | HTTP controllers, middleware, Swagger documentation |
| **Application** | Commands, queries, handlers, validation, DTOs |
| **Domain** | Entities, value objects, domain events, business rules |
| **Infrastructure** | EF Core persistence, Dapper queries, caching, email |

### 📂 Project Structure

```
src/
├── SampleProject.API/              # REST API entry point
│   ├── Customers/                   # Customer endpoints
│   ├── Orders/                      # Order endpoints
│   ├── Configuration/               # Middleware, Swagger, DI
│   └── SeedWork/                    # ProblemDetails mappings
├── SampleProject.Application/       # Use cases (CQRS handlers)
│   ├── Customers/                   # Register, query, integration events
│   ├── Orders/                      # Place, change, remove, query orders
│   └── Configuration/               # Commands, queries, validation abstractions
├── SampleProject.Domain/            # Core business logic
│   ├── Customers/                   # Customer aggregate
│   ├── Products/                    # Product entity
│   ├── Payments/                    # Payment value objects
│   ├── ForeignExchange/             # Exchange rate logic
│   └── SeedWork/                    # Base classes (Entity, ValueObject, etc.)
├── SampleProject.Infrastructure/    # External concerns
│   ├── Database/                    # EF Core DbContext, migrations
│   ├── Caching/                     # In-memory cache (Cache-Aside)
│   ├── Processing/                  # Outbox pattern (Quartz.NET)
│   └── Domain/                      # Repository implementations
└── Tests/
    └── SampleProject.IntegrationTests/
```

### ⚙️ Tech Stack

| Technology | Purpose |
|---|---|
| **.NET Core 3.1** | Runtime framework |
| **MediatR** | Command/Query/Event dispatching |
| **Entity Framework Core** | Write model (ORM) |
| **Dapper** | Read model (raw SQL on database views) |
| **FluentValidation** | Input validation pipeline |
| **Quartz.NET** | Outbox pattern scheduling |
| **Swagger / OpenAPI** | Interactive API documentation |
| **Serilog** | Structured logging |
| **ProblemDetails** | RFC 7807 error responses |

### 🔀 CQRS Flow

```
                    ┌──────────────┐
   HTTP Request ──► │  Controller  │
                    └──────┬───────┘
                           │
              ┌────────────┴────────────┐
              ▼                         ▼
      ┌──────────────┐         ┌──────────────┐
      │   Command    │         │    Query     │
      │  (MediatR)   │         │  (MediatR)   │
      └──────┬───────┘         └──────┬───────┘
             │                        │
             ▼                        ▼
      ┌──────────────┐         ┌──────────────┐
      │  EF Core     │         │   Dapper     │
      │ (Write Model)│         │ (Read Model) │
      └──────────────┘         └──────────────┘
```

### 🔑 Domain Model

![Domain Model](docs/domain_model_diagram.png)

### 🧩 Key Patterns Demonstrated

- **Aggregate Root** — `Customer` and `Order` as consistency boundaries
- **Value Objects** — `MoneyValue`, `OrderProduct`, immutable domain primitives
- **Domain Events** — `OrderPlacedEvent`, `CustomerRegisteredEvent` for cross-aggregate communication
- **Business Rule Validation** — encapsulated rules checked before state changes
- **Cache-Aside Pattern** — in-memory caching for read performance
- **Outbox Pattern** — guaranteed delivery of integration events via Quartz.NET

### 🚀 How to Run

1. Create an empty SQL Server database
2. Execute `src/InitializeDatabase.sql`
3. Configure the connection string in `appsettings.json` (or via User Secrets):
   ```json
   {
     "OrdersConnectionString": "Server=...;Database=...;Trusted_Connection=True;"
   }
   ```
4. Run the application:
   ```bash
   cd src/SampleProject.API
   dotnet run
   ```
5. Access Swagger UI at `https://localhost:5001/swagger`

### 🧪 Integration Tests

```bash
export ASPNETCORE_SampleProject_IntegrationTests_ConnectionString="<your-connection-string>"
cd src/Tests/SampleProject.IntegrationTests
dotnet test
```

### 📡 API Endpoints

| Method | Route | Description |
|---|---|---|
| `POST` | `/api/customers` | Register a new customer |
| `GET` | `/api/customers/{id}/orders` | List customer orders |
| `GET` | `/api/customers/{id}/orders/{orderId}` | Get order details |
| `POST` | `/api/customers/{id}/orders` | Place a new order |
| `PUT` | `/api/customers/{id}/orders/{orderId}` | Change an existing order |
| `DELETE` | `/api/customers/{id}/orders/{orderId}` | Remove an order |

---

## 🇧🇷 Português

### 📖 Sobre

Este projeto é uma implementação de estudo e referência de uma **API REST em .NET Core** aplicando padrões arquiteturais do mundo real:

- **CQRS** — separa modelos de leitura e escrita para acesso otimizado aos dados
- **Domain-Driven Design (DDD)** — modelo de domínio rico com regras de negócio encapsuladas
- **Clean Architecture** — separação clara de responsabilidades entre camadas
- **Outbox Pattern** — publicação confiável de eventos de domínio para cenários de integração

A API gerencia **Clientes** e **Pedidos**, demonstrando como esses padrões trabalham juntos em uma aplicação coesa.

### 🏛️ Arquitetura

A solução segue a [Clean Architecture](http://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) com quatro camadas bem definidas:

| Camada | Responsabilidade |
|---|---|
| **API** | Controllers HTTP, middleware, documentação Swagger |
| **Application** | Commands, queries, handlers, validação, DTOs |
| **Domain** | Entidades, value objects, eventos de domínio, regras de negócio |
| **Infrastructure** | Persistência com EF Core, consultas com Dapper, cache, e-mail |

### ⚙️ Stack Tecnológica

| Tecnologia | Uso |
|---|---|
| **.NET Core 3.1** | Framework de execução |
| **MediatR** | Despacho de commands/queries/eventos |
| **Entity Framework Core** | Modelo de escrita (ORM) |
| **Dapper** | Modelo de leitura (SQL puro em views) |
| **FluentValidation** | Pipeline de validação de entrada |
| **Quartz.NET** | Agendamento do padrão Outbox |
| **Swagger / OpenAPI** | Documentação interativa da API |
| **Serilog** | Logging estruturado |

### 🧩 Padrões Demonstrados

- **Aggregate Root** — `Customer` e `Order` como limites de consistência
- **Value Objects** — `MoneyValue`, `OrderProduct`, primitivos imutáveis do domínio
- **Eventos de Domínio** — `OrderPlacedEvent`, `CustomerRegisteredEvent` para comunicação entre agregados
- **Validação de Regras de Negócio** — regras encapsuladas verificadas antes de mudanças de estado
- **Cache-Aside** — cache em memória para performance de leitura
- **Outbox Pattern** — entrega garantida de eventos de integração via Quartz.NET

### 🚀 Como Executar

1. Crie um banco de dados SQL Server vazio
2. Execute o script `src/InitializeDatabase.sql`
3. Configure a connection string em `appsettings.json` (ou via User Secrets):
   ```json
   {
     "OrdersConnectionString": "Server=...;Database=...;Trusted_Connection=True;"
   }
   ```
4. Execute a aplicação:
   ```bash
   cd src/SampleProject.API
   dotnet run
   ```
5. Acesse o Swagger UI em `https://localhost:5001/swagger`

### 🧪 Testes de Integração

```bash
export ASPNETCORE_SampleProject_IntegrationTests_ConnectionString="<sua-connection-string>"
cd src/Tests/SampleProject.IntegrationTests
dotnet test
```

---

## 📝 Credits / Créditos

> Originally created by [Kamil Grzybek](https://github.com/kgrzybek) — [Original Repository](https://github.com/kgrzybek/sample-dotnet-core-cqrs-api)
>
> Forked and customized for portfolio and learning purposes.

## 📄 License

This project is licensed under the [MIT License](LICENSE).
