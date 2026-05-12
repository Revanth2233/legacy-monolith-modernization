# Legacy Monolith Modernization

> **ASP.NET Core · C# · React · TanStack Router · TanStack Query · Material UI · Zustand · JWT · PowerShell · SQL Server**

Complete rewrite of a tightly-coupled legacy application into a clean layered ASP.NET Core architecture — eliminating recurring production bugs, making each layer independently testable, and cutting deployment time by 82%.

**Outcome:** API response 4–6s → <500ms | Deployments 45 min → 8 min | 2 recurring prod bugs eliminated

---

## 🏗️ Before vs. After

| Aspect | Before (Legacy) | After (Modernized) |
|--------|----------------|-------------------|
| Architecture | Monolithic, tightly coupled | Layered: Controller → BAL → DAL |
| Data access | Stored procedure-heavy, unindexed | Repository Pattern + targeted indexing |
| DTO mapping | Manual, error-prone | AutoMapper |
| Dependency injection | None | Full DI container |
| Testability | Untestable monolith | Each layer independently testable |
| API response time | 4–6 seconds | Under 500ms |
| Deployment | 12-step manual process | Automated PowerShell (8 min) |
| State management | Prop-drilling | Zustand |
| Server state | Redundant API calls | TanStack Query caching |

---

## ✨ Key Decisions

### 1. Repository Pattern + Layered Architecture
Replaced stored procedure-heavy data access with a clean Repository Pattern. Controller → Business Access Layer → Data Access Layer — each independently testable and deployable.

### 2. Dependency Injection + AutoMapper
Eliminated manual DTO mapping that had caused two recurring production bugs. AutoMapper profiles enforced consistent object mapping across all service layers.

### 3. Performance — Stored Procedure Rewrites
Identified full table scans in unindexed stored procedures under concurrent load. Rewrote with targeted indexing and parameter sniffing — cutting response times from 4–6s to under 500ms.

### 4. React Frontend Modernization
Built reusable component library with Material UI across 8 screens. Introduced TanStack Query for server-state caching (eliminated redundant API calls) and replaced prop-drilling with Zustand global state — resolved client-flagged UI performance issues in a single sprint.

### 5. Automated Deployments
Replaced a 12-step manual deployment process (frequent config rollbacks) with PowerShell scripts with step-level validation. Deploy time: 45 min → 8 min.

---

## 🗂️ Project Structure

```
legacy-monolith-modernization/
├── backend/
│   ├── Controllers/
│   │   └── [Feature]Controller.cs
│   ├── BAL/                         # Business Access Layer
│   │   ├── Interfaces/
│   │   └── Services/
│   ├── DAL/                         # Data Access Layer
│   │   ├── Interfaces/
│   │   └── Repositories/
│   ├── Models/
│   │   ├── DTOs/
│   │   └── Entities/
│   ├── Mappings/
│   │   └── AutoMapperProfile.cs
│   └── Program.cs
├── frontend/
│   ├── src/
│   │   ├── components/              # Reusable MUI component library
│   │   ├── hooks/                   # TanStack Query hooks
│   │   ├── store/                   # Zustand state
│   │   └── routes/                  # TanStack Router with JWT guards
│   └── package.json
├── scripts/
│   └── deploy.ps1                   # Multi-environment deploy automation
└── README.md
```

---

## 🛠️ Tech Stack

| Layer | Technology |
|-------|-----------|
| Backend | ASP.NET Core Web API (C#) |
| Architecture | Layered (Controller/BAL/DAL), Repository Pattern |
| ORM/Mapping | Entity Framework Core, AutoMapper |
| Auth | JWT Bearer + TanStack Router guards |
| Frontend | React, TypeScript, Material UI |
| State | Zustand (global), TanStack Query (server) |
| Database | MS SQL Server |
| Deployment | PowerShell CI/CD scripts |

---

*Built at Trinal (June 2024 – December 2025).*
