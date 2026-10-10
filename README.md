# HotelAIAgent

**Hotel Management & Intelligent Guest Communication Platform**

A modular, multi-tenant hotel management backend built with **ASP.NET Core 9, C#, Entity Framework Core, and SQL Server**.

HotelAIAgent is an evolving B2B SaaS project designed to automate hotel guest interactions, manage conversations, evaluate pricing rules, and generate personalized room offers.

> **Project Status:** MVP — Active Development

## 🎯 Project Overview

HotelAIAgent aims to help hotels improve guest communication and streamline room offer management through automated workflows and configurable business rules.

The platform combines conversation state management, customer information, hotel inventory, and rule-based pricing to support personalized guest experiences.

The long-term vision is to integrate Large Language Models (LLMs) and hotel management systems to provide a more intelligent and scalable hospitality solution.

## 🚀 Current Features

- **Multi-Tenant Architecture:** Hotel-specific request context and data models designed for multiple hotel organizations.
- **Conversation Management:** Tracks customer conversations and their current states.
- **Event Detection:** Identifies guest intentions using a keyword-based detection mechanism.
- **State Machine:** Controls conversation transitions, including room inquiries, offers, acceptance, and rejection.
- **Rule-Based Pricing:** Evaluates configurable discount rules based on customer visit history.
- **Price Protection:** Applies minimum room price constraints to prevent excessive discounts.
- **Offer Management:** Creates and stores personalized room offers.
- **Negotiation Workflow:** Supports basic price negotiation and escalation decisions.
- **Audit Logging:** Records important conversation and offer-related events.
- **Provider Abstraction:** Interfaces and mock implementations for PMS and inventory integrations.
- **Database Initialization:** Automatic EF Core migrations and initial seed data for local development.

## 🛠️ Technology Stack

| Technology | Purpose |
|---|---|
| C# | Primary programming language |
| .NET 9 | Application framework |
| ASP.NET Core Web API | REST API development |
| Entity Framework Core | Object-relational mapping |
| SQL Server | Relational database |
| Swagger / OpenAPI | API documentation and exploration |
| Dependency Injection | Service and dependency management |
| Git & GitHub | Version control |

## 🏗️ Architecture

The solution follows a layered architecture inspired by Clean Architecture and SOLID principles.

### Project Structure

```text
HotelAIAgent/
│
├── src/
│   ├── HotelAIAgent.API/
│   │   ├── Controllers/
│   │   ├── Middleware/
│   │   ├── Properties/
│   │   └── Program.cs
│   │
│   ├── HotelAIAgent.Application/
│   │   ├── Dtos/
│   │   ├── Interfaces/
│   │   └── Services/
│   │
│   ├── HotelAIAgent.Domain/
│   │   ├── Common/
│   │   ├── Entities/
│   │   ├── Enums/
│   │   └── Repositories/
│   │
│   ├── HotelAIAgent.Infrastructure/
│   │   ├── Integrations/
│   │   ├── Migrations/
│   │   └── Persistence/
│   │
│   └── HotelAIAgent.Shared/
│
├── HotelAIAgent.sln
├── .gitignore
└── README.md
```

### Layer Responsibilities

**API Layer**

Handles incoming HTTP requests, routing, tenant context resolution, and API responses.

**Application Layer**

Contains application services, conversation orchestration, event detection, state management, and business workflow coordination.

**Domain Layer**

Defines core entities, business concepts, enumerations, and repository contracts.

**Infrastructure Layer**

Implements database persistence, Entity Framework Core repositories, migrations, and external provider adapters.

**Shared Layer**

Provides a separate project for future reusable components and shared abstractions.

## 🧠 Core Business Workflow

The current MVP processes guest messages through the following workflow:

1. **Tenant Resolution:** Identify the hotel from the incoming request.
2. **Customer Identification:** Retrieve or create the customer record.
3. **Conversation Management:** Retrieve or initialize the active conversation.
4. **Event Detection:** Identify the customer's intention.
5. **State Transition:** Update the conversation state.
6. **Inventory Lookup:** Retrieve available rooms using the configured inventory provider.
7. **Rule Evaluation:** Calculate eligible discounts based on active hotel rules.
8. **Offer Generation:** Create and store the personalized room offer.
9. **Audit Logging:** Record important actions and state changes.
10. **Response Generation:** Return the appropriate message and offer details.

### Rule Engine

The Rule Engine evaluates hotel-specific business rules and calculates discounted offers.

The current implementation supports repeat-visit-based discounts and minimum price protection.

For example:

- A returning customer may qualify for a configured discount.
- Active rules are evaluated according to their conditions.
- The resulting room price cannot fall below the configured minimum price.
- Negotiations reaching pricing limits can trigger an escalation decision.

Additional pricing conditions and rule types are planned for future development.

## 🔌 External Integrations

The application uses provider interfaces to separate hotel-specific integrations from the core business logic.

**PMS Providers**
- Mock PMS provider — available for development and testing.
- Adonis PMS adapter — integration placeholder.

**Inventory Providers**
- Mock inventory provider — available for development and testing.
- HotelRunner adapter — integration placeholder.

Live Adonis and HotelRunner API integrations are not yet implemented.

## 📡 API Endpoints

| Method | Endpoint | Description |
|---|---|---|
| POST | `/api/agent/message` | Process an incoming guest message |
| GET | `/api/hotels/tenant/{tenantId}` | Retrieve hotels belonging to a tenant |
| GET | `/api/rules/hotel/{hotelId}` | Retrieve active pricing rules for a hotel |

### Example Message Request

**POST** `/api/agent/message`

Header:

```http
X-Hotel-Id: {hotel-guid}
Content-Type: application/json
```

Request body:

```json
{
  "conversationId": null,
  "customerPhone": "+905551234567",
  "customerName": "Demo Customer",
  "messageText": "Merhaba, müsait odanız var mı?"
}
```

The provided hotel ID must correspond to an existing hotel record in the database.

## ⚙️ Getting Started

### Prerequisites

- .NET 9 SDK
- SQL Server or SQL Server Express
- Visual Studio 2022 with .NET 9 support, or another compatible IDE
- Git

### Installation

**1. Clone the repository**

```bash
git clone https://github.com/basolatakan/HotelAIAgent.git
cd HotelAIAgent
```

**2. Restore dependencies**

```bash
dotnet restore
```

**3. Configure the database**

Update the `DefaultConnection` value in:

`src/HotelAIAgent.API/appsettings.json`

Configure a valid SQL Server connection string for your local environment.

Do not commit real passwords or production secrets. Use environment variables or user secrets when necessary.

**4. Run the application**

```bash
dotnet run --project src/HotelAIAgent.API
```

**5. Explore the API**

When running in the Development environment, open:

`https://localhost:<port>/swagger`

Replace `<port>` with the HTTPS port displayed in the application output.

The application currently applies EF Core migrations and initializes development seed data during startup.

## 🗺️ Roadmap

The project is actively evolving. Planned improvements include:

- [x] Layered solution architecture
- [x] Entity Framework Core persistence and migrations
- [x] Initial database seed data
- [x] Conversation state management
- [x] Basic event detection
- [x] Rule-based discount calculation
- [x] Mock PMS and inventory providers
- [x] Offer creation and negotiation workflow
- [ ] Advanced pricing rules and discount strategies
- [ ] LLM-based natural language understanding
- [ ] Live Adonis PMS integration
- [ ] Live HotelRunner inventory integration
- [ ] Authentication and role-based authorization
- [ ] Stronger tenant isolation and security controls
- [ ] Automated unit and integration tests
- [ ] Enhanced logging and monitoring
- [ ] Cloud deployment and CI/CD workflows

## 🔒 Security & Development Notes

This repository represents an early-stage MVP and is not production-ready.

The current tenant resolution mechanism is based on a request header and should not be treated as a complete authentication or authorization solution.

Development seed data and mock providers are intended for local testing. Production credentials and sensitive configuration must be managed securely.

## 👨‍💻 Author

**Atakan Başol**

Software Engineer | .NET Backend Development

[GitHub Profile](https://github.com/basolatakan)

---

*HotelAIAgent is an evolving software project focused on hospitality automation, backend architecture, and intelligent business workflows.*
