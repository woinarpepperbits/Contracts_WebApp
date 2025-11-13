# Contracts WebApp - Vertrags-Sonderkunden Verwaltung

Moderne Web-Anwendung zur Verwaltung von Vertrags-Sonderkunden für EVUs, die nicht über SAP abgerechnet werden können.

## 📋 Projekt-Übersicht

Dieses Projekt demonstriert die KI-gestützte Neuentwicklung einer kompletten Domain aus dem Legacy VB6-System als moderne Web-Anwendung.

### Technologie-Stack

**Backend:**
- ASP.NET Core 9.0 Web API
- Entity Framework Core 9.0
- SQL Server / In-Memory Database
- AutoMapper, FluentValidation
- Serilog, Swagger/OpenAPI

**Frontend:**
- React 18 mit TypeScript
- Vite als Build Tool  
- Material-UI (MUI) oder Tailwind CSS
- React Query (TanStack Query)
- React Router v6
- Axios

## 🏗️ Projekt-Struktur

```
Contracts_WebApp/
├── PRD.md                          # Product Requirements Document
├── ContractsApp.sln                # .NET Solution
├── src/
│   ├── ContractsApp.Api/           # ASP.NET Core Web API
│   ├── ContractsApp.Core/          # Domain Models, Interfaces
│   └── ContractsApp.Infrastructure/# EF Core, Repositories
├── tests/
│   └── ContractsApp.Tests/         # Unit & Integration Tests
└── client/                         # React Frontend
    ├── src/
    │   ├── components/
    │   ├── pages/
    │   ├── services/
    │   └── types/
    └── package.json
```

## 🚀 Quick Start

### Backend

```powershell
# Solution builden
cd f:\source\Rita_Neuentwicklung_KI\Contracts_WebApp
dotnet build

# API starten
cd src/ContractsApp.Api
dotnet run

# Swagger UI: https://localhost:5001/swagger
```

### Frontend

```powershell
# Dependencies installieren
cd client
npm install

# Dev Server starten
npm run dev

# App öffnet auf: http://localhost:5173
```

## 📚 Dokumentation

- **[PRD.md](./PRD.md)** - Ausführliche Produktanforderungen
- **API-Dokumentation** - Swagger UI nach Start der API unter `/swagger`
- **VB6-Referenz** - `../VB6_Migration/VB6_Source_Reference/Verträge/`

## 🎯 MVP Features

- ✅ Vertrags-Liste mit Filterung & Suche
- ✅ Vertrag erstellen (Wizard)
- ✅ Vertrag bearbeiten
- ✅ Vertrag Details anzeigen
- ✅ Preismodell verwalten
- ✅ Vertragskunden zuordnen

## 🧪 Testing

```powershell
# Backend Tests
dotnet test

# Frontend Tests (wenn implementiert)
cd client
npm test
```

## 📦 Build & Deploy

```powershell
# Backend Release Build
dotnet publish src/ContractsApp.Api -c Release -o ./publish

# Frontend Production Build
cd client
npm run build
```

## 🔧 Entwicklung

### Backend: Neue Entity hinzufügen

1. Model in `ContractsApp.Core/Models/` erstellen
2. DbSet in `ApplicationDbContext` hinzufügen
3. Migration erstellen: `dotnet ef migrations add AddNewEntity`
4. Controller und DTOs erstellen

### Frontend: Neue Seite hinzufügen

1. Component in `client/src/pages/` erstellen
2. Route in `App.tsx` registrieren
3. Service-Methode für API-Call anlegen

## 📝 Hinweise

- **Mock-Daten**: API nutzt In-Memory-Database mit Seed-Daten
- **Auth**: MVP verwendet vereinfachtes Mock-Auth
- **UI-Design**: Orientiert sich an Angular_FM Design System

## 🐛 Bekannte Probleme

- [ ] Entity Framework Packages Version 10 nicht kompatibel mit .NET 9
  - ✅ Gelöst: EF Core 9.0 verwenden
  
## 📧 Kontakt

Entwickelt als Teil der VB6-Migration Initiative für Rita_Neuentwicklung_KI.

---

**Status**: 🚧 In Entwicklung (MVP Phase 1)
