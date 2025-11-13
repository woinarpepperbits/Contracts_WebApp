# Quickstart Guide - Contracts WebApp

## ✅ Was wurde erstellt?

### Backend (.NET 9.0)

#### Projektstruktur
```
src/
├── ContractsApp.Core/           # Domain Models, Entities
│   ├── Models/
│   │   ├── BaseEntity.cs
│   │   ├── Contract.cs          # ✅ Hauptentität Vertrag
│   │   ├── Customer.cs          # ✅ Kunde/EVU
│   │   ├── Mandant.cs           # ✅ Mandant
│   │   ├── ContractGroup.cs     # ✅ Vertragsgruppe
│   │   ├── Currency.cs          # ✅ Währung
│   │   ├── ContractPrice.cs     # ✅ Vertragspreise
│   │   ├── PriceType.cs         # ✅ Preisart
│   │   └── ContractCustomer.cs  # ✅ Vertragskunde
│   └── Models/Enums/
│       ├── ContractStatus.cs    # ✅ Status-Enum
│       ├── ContractType.cs      # ✅ Vertragsart-Enum
│       └── ContractCustomerRole.cs # ✅ Kundenrolle-Enum
│
├── ContractsApp.Infrastructure/ # EF Core, DbContext
│   └── Data/
│       └── ApplicationDbContext.cs # ✅ DbContext mit Seed-Daten
│
└── ContractsApp.Api/            # ASP.NET Core Web API
    ├── Controllers/
    │   ├── ContractsController.cs   # ✅ CRUD für Verträge
    │   └── LookupsController.cs     # ✅ Lookup-Daten
    ├── DTOs/
    │   ├── ContractDto.cs           # ✅ Read DTO
    │   ├── CreateContractDto.cs     # ✅ Create DTO
    │   └── UpdateContractDto.cs     # ✅ Update DTO
    └── Program.cs                   # ✅ Konfiguration

tests/
└── ContractsApp.Tests/          # Unit Tests (bereit für Tests)
```

#### ✅ Implementierte Features

1. **Vollständiges Domain Model**
   - 8 Entities mit Beziehungen
   - Enums für Status, Type, Role
   - BaseEntity für Audit-Felder

2. **Entity Framework Core**
   - In-Memory Database für MVP
   - Seed-Daten für Testing
   - Fluent API Konfiguration

3. **REST API - ContractsController**
   - `GET /api/contracts` - Liste mit Filter, Suche, Pagination
   - `GET /api/contracts/{id}` - Einzelner Vertrag
   - `POST /api/contracts` - Neuen Vertrag erstellen
   - `PUT /api/contracts/{id}` - Vertrag aktualisieren
   - `DELETE /api/contracts/{id}` - Vertrag löschen

4. **REST API - LookupsController**
   - `GET /api/lookups/customers` - Kunden für Dropdown
   - `GET /api/lookups/mandants` - Mandanten
   - `GET /api/lookups/contract-groups` - Vertragsgruppen
   - `GET /api/lookups/currencies` - Währungen
   - `GET /api/lookups/price-types` - Preisarten
   - `GET /api/lookups/contract-statuses` - Status-Werte
   - `GET /api/lookups/contract-types` - Vertragsart-Werte

5. **Swagger/OpenAPI**
   - Swagger UI unter `/swagger`
   - Vollständige API-Dokumentation
   - Interaktives Testing

6. **Logging**
   - Serilog-Integration
   - Console-Logging
   - Request-Logging

7. **CORS**
   - Frontend-freundliche CORS-Policy

---

## 🚀 Backend starten

### Option 1: Visual Studio / Rider
1. Öffne `ContractsApp.sln`
2. Setze `ContractsApp.Api` als Startup-Projekt
3. Drücke F5

### Option 2: PowerShell
```powershell
cd f:\source\Rita_Neuentwicklung_KI\Contracts_WebApp\src\ContractsApp.Api
dotnet run
```

### Ergebnis:
```
[INF] Starting Contracts WebApp API
[INF] Database initialized with seed data
[INF] API is running
[INF] Now listening on: http://localhost:5166
```

---

## 📊 API testen

### Swagger UI
Öffne: **http://localhost:5166/swagger**

### cURL-Beispiele

#### 1. Alle Verträge abrufen
```powershell
curl http://localhost:5166/api/contracts
```

#### 2. Verträge mit Filter
```powershell
# Nach Status filtern
curl "http://localhost:5166/api/contracts?status=1"

# Suche
curl "http://localhost:5166/api/contracts?search=EVU"

# Pagination
curl "http://localhost:5166/api/contracts?page=1&pageSize=10"
```

#### 3. Einzelnen Vertrag abrufen
```powershell
# Zuerst ID aus Liste holen, dann:
curl http://localhost:5166/api/contracts/{GUID}
```

#### 4. Neuen Vertrag erstellen
```powershell
$body = @{
    contractNumber = "V-TEST-001"
    customerId = "55555555-5555-5555-5555-555555555555"
    mandantId = "22222222-2222-2222-2222-222222222222"
    contractGroupId = "33333333-3333-3333-3333-333333333333"
    contractType = 0
    status = 1
    startDate = "2024-01-01T00:00:00Z"
    isUnlimited = $true
    noticePeriodMonths = 3
    autoRenew = $true
    billingStartDate = "2024-01-01T00:00:00Z"
    responsibleSales = "Max Mustermann"
    currencyId = "11111111-1111-1111-1111-111111111111"
} | ConvertTo-Json

Invoke-RestMethod -Uri "http://localhost:5166/api/contracts" `
    -Method Post `
    -ContentType "application/json" `
    -Body $body
```

#### 5. Lookup-Daten für Dropdowns
```powershell
# Kunden
curl http://localhost:5166/api/lookups/customers

# Mandanten
curl http://localhost:5166/api/lookups/mandants

# Vertragsgruppen
curl http://localhost:5166/api/lookups/contract-groups

# Währungen
curl http://localhost:5166/api/lookups/currencies

# Status-Werte
curl http://localhost:5166/api/lookups/contract-statuses
```

---

## 📦 Seed-Daten

Die Datenbank wird automatisch mit folgenden Test-Daten gefüllt:

### Kunden
- `K-12345`: EVU Musterkunde GmbH
- `K-67890`: Stadtwerke Beispielstadt AG

### Mandanten
- `M1`: Mandant 1

### Vertragsgruppen
- `SK`: Sonderkunden

### Währungen
- `EUR`: Euro (€)

### Preisarten
- `AP`: Arbeitspreis (kWh)
- `GP`: Grundpreis (Monat)

---

## 🔧 Entwicklung

### Neue Entity hinzufügen

1. **Model erstellen** (`ContractsApp.Core/Models/`)
```csharp
public class MyEntity : BaseEntity
{
    public string Name { get; set; } = string.Empty;
}
```

2. **DbSet hinzufügen** (`ApplicationDbContext.cs`)
```csharp
public DbSet<MyEntity> MyEntities => Set<MyEntity>();
```

3. **Konfiguration** (in `OnModelCreating`)
```csharp
modelBuilder.Entity<MyEntity>(entity =>
{
    entity.HasKey(e => e.Id);
    entity.Property(e => e.Name).HasMaxLength(200);
});
```

4. **Controller erstellen**
```csharp
[ApiController]
[Route("api/[controller]")]
public class MyEntitiesController : ControllerBase
{
    // CRUD Operations
}
```

### DTOs erstellen

Für jede API-Operation eigene DTOs:
- `MyEntityDto` - für GET (Read)
- `CreateMyEntityDto` - für POST (Create)
- `UpdateMyEntityDto` - für PUT (Update)

---

## 📋 Nächste Schritte

### Backend
- [ ] AutoMapper für DTO-Mapping einrichten
- [ ] FluentValidation Validators implementieren
- [ ] Unit Tests schreiben
- [ ] Integration Tests schreiben
- [ ] Authentication/Authorization hinzufügen
- [ ] SQL Server Migration vorbereiten

### Frontend
- [ ] React-Projekt mit Vite aufsetzen
- [ ] Axios/Fetch Services für API-Calls
- [ ] Vertrags-Liste Komponente
- [ ] Vertrags-Detail Komponente
- [ ] Vertrags-Erstellungs-Wizard
- [ ] Routing mit React Router
- [ ] State Management (React Query + Zustand)

---

## 🐛 Troubleshooting

### Port bereits belegt
Falls Port 5166 belegt ist, ändere in `launchSettings.json`:
```json
"applicationUrl": "http://localhost:DEIN_PORT"
```

### Build-Fehler
```powershell
# Clean und Rebuild
dotnet clean
dotnet build
```

### Seed-Daten neu laden
Die In-Memory-DB wird bei jedem Start neu erstellt und mit Seed-Daten gefüllt.

---

## 📚 Ressourcen

- **PRD**: `./PRD.md` - Vollständige Produkt-Anforderungen
- **README**: `./README.md` - Projekt-Übersicht
- **Swagger UI**: http://localhost:5166/swagger
- **VB6-Referenz**: `../VB6_Migration/VB6_Source_Reference/Verträge/`

---

**Status**: ✅ Backend MVP komplett funktionsfähig!  
**Nächster Schritt**: Frontend mit React implementieren
