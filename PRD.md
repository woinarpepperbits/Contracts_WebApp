# Product Requirements Document (PRD)
## Vertrags-Sonderkunden Web-Anwendung MVP

**Version:** 1.0  
**Datum:** 13. November 2025  
**Autor:** KI-Assistent  
**Status:** Draft

---

## 1. Executive Summary

### 1.1 Projektziel
Entwicklung einer modernen Web-Anwendung zur Verwaltung von Vertrags-Sonderkunden für Energieversorgungsunternehmen (EVUs), die nicht über SAP abgerechnet werden können. Die Anwendung ersetzt die Legacy VB6-Bibliothek "Verträge" durch eine moderne, KI-unterstützt entwickelte Lösung.

### 1.2 Motivation
- **Legacy-Ablösung**: VB6-Code ist veraltet und schwer wartbar
- **Modernisierung**: Nutzung moderner Web-Technologien
- **KI-Testing**: Validierung des Ansatzes, komplette Domains neu zu entwickeln
- **Benutzererfahrung**: Moderne UI angelehnt an bestehendes Angular_FM System

### 1.3 Erfolgskriterien
- ✅ Funktionale Vertrags-Erstellung und -Verwaltung
- ✅ Intuitive UI konsistent mit Angular_FM
- ✅ Vollständige REST API
- ✅ Saubere Domain-Driven Design Architektur
- ✅ Dokumentierter Code
- ✅ Deploybare Lösung

---

## 2. Stakeholder & Zielgruppe

### 2.1 Primäre Benutzer
- **Sachbearbeiter Vertrieb**: Erstellen und verwalten Verträge
- **Sachbearbeiter Buchhaltung**: Prüfen Abrechnungsdaten
- **Sachbearbeiter Preise**: Pflegen Preismodelle

### 2.2 Sekundäre Benutzer
- **Administratoren**: System-Konfiguration
- **Management**: Reporting und Übersichten

---

## 3. Funktionale Anforderungen

### 3.1 Core Features (MVP)

#### F1: Vertrags-Übersicht (Must-Have)
- **Beschreibung**: Liste aller Vertrags-Sonderkunden
- **Details**:
  - Tabellarische Darstellung mit Sortierung
  - Filterung nach Status, Kunde, Zeitraum
  - Suche nach Vertragsnummer, Kundenname
  - Pagination (25/50/100 Einträge)
  - Quick-Actions (Bearbeiten, Details, Löschen)
- **Datenfelder**:
  - Vertragsnummer
  - Kundenname & Kundennummer
  - Vertragsbeginn & -ende
  - Status (Aktiv, In Verhandlung, Gekündigt, Beendet)
  - Vertragsart
  - Mandant
  - Sachbearbeiter

#### F2: Vertrag erstellen (Must-Have)
- **Beschreibung**: Wizard zum Anlegen neuer Vertrags-Sonderkunden
- **Schritte**:
  1. **Stammdaten**:
     - Vertragsnummer (Auto oder Manuell)
     - Kundenauswahl (Dropdown mit Suche)
     - Mandant
     - Vertragsgruppe
     - Vertragsart
     - Währung
  2. **Zeiträume**:
     - Vertragsbeginn
     - Vertragsende (oder unbefristet)
     - Abrechnungsbeginn
     - Kündigungsfrist (Monate)
     - Kündigungsstichtag
     - Verlängerung (automatisch/manuell)
  3. **Zuständigkeiten**:
     - Sachbearbeiter Vertrieb
     - Sachbearbeiter Buchhaltung
     - Sachbearbeiter Preise
  4. **Rechnungseinstellungen**:
     - Rechnungsdefinition
     - Abrechnungstermin
     - Abschlagszyklus
  5. **Zusammenfassung & Speichern**
- **Validierung**:
  - Pflichtfelder prüfen
  - Datumslogik validieren (Ende > Beginn)
  - Duplikatsprüfung Vertragsnummer

#### F3: Vertrag bearbeiten (Must-Have)
- **Beschreibung**: Bestehende Verträge anpassen
- **Features**:
  - Alle Felder aus Erstellung bearbeitbar
  - Änderungshistorie (wer, wann, was)
  - Versionierung bei Preisänderungen
  - Gültigkeitsdaten bei Änderungen

#### F4: Vertrag anzeigen (Must-Have)
- **Beschreibung**: Detailansicht eines Vertrags
- **Tabs**:
  - Stammdaten (Read-only Übersicht)
  - Zeiträume & Gültigkeit
  - Preise (Liste der Vertragspreise)
  - Kunden (Vertragskunden mit Abschlägen)
  - Abrechnung (Abrechnungsstellen)
  - Historie (Änderungen)
- **Actions**:
  - Bearbeiten
  - Duplizieren
  - PDF-Export
  - Löschen (mit Bestätigung)

#### F5: Preismodell (Must-Have)
- **Beschreibung**: Verwaltung der Vertragspreise
- **Features**:
  - Preise mit Gültigkeit anlegen
  - Preisarten zuordnen (Arbeits-/Grundpreis)
  - Formeln definieren (vereinfacht)
  - Mehrere Preise pro Vertrag
  - Preishistorie

#### F6: Vertragskunden (Must-Have)
- **Beschreibung**: Zuordnung von Kunden zu Verträgen
- **Features**:
  - Kunde als Vertragspartner
  - Abweichende Rechnungsempfänger
  - Abschlagsbeträge definieren
  - Zahlungskonditionen
  - Rechnungseinstellungen

### 3.2 Nice-to-Have Features (Post-MVP)
- **F7**: Dokumenten-Upload (Vertragsunterlagen)
- **F8**: E-Mail-Benachrichtigungen (Kündigungsfristen)
- **F9**: Dashboard mit KPIs
- **F10**: Erweiterte Berechtigungsverwaltung
- **F11**: Excel-Import/-Export
- **F12**: Massenänderungen

---

## 4. Nicht-Funktionale Anforderungen

### 4.1 Performance
- Liste laden: < 500ms (100 Einträge)
- Vertrag speichern: < 1s
- Suche: < 300ms

### 4.2 Sicherheit
- Authentifizierung via JWT
- Rollen-basierte Zugriffskontrolle (RBAC)
- HTTPS-only
- SQL-Injection Prevention (Entity Framework)
- XSS-Protection

### 4.3 Usability
- Responsive Design (Desktop, Tablet)
- Konsistente UI mit Angular_FM
- Inline-Validierung bei Formularen
- Hilfetexte und Tooltips
- Tastaturnavigation

### 4.4 Wartbarkeit
- Clean Code Prinzipien
- Domain-Driven Design (DDD)
- Unit Tests (>70% Coverage)
- API-Dokumentation (Swagger)
- Logging (Serilog)

### 4.5 Skalierbarkeit
- Stateless API (horizontal skalierbar)
- Datenbank-Indizes optimiert
- Caching-Strategie (Redis ready)

---

## 5. Technologie-Stack

### 5.1 Backend
- **Framework**: ASP.NET Core 8.0
- **ORM**: Entity Framework Core 8.0
- **Datenbank**: SQL Server (in-memory für MVP)
- **API**: REST mit Minimal APIs / Controllers
- **Validierung**: FluentValidation
- **Mapping**: AutoMapper
- **Logging**: Serilog
- **Dokumentation**: Swagger/OpenAPI

### 5.2 Frontend
- **Framework**: React 18 mit TypeScript
- **Build Tool**: Vite
- **UI Library**: Material-UI (MUI) oder Tailwind CSS + shadcn/ui
- **State Management**: TanStack Query (React Query) + Zustand
- **Forms**: React Hook Form + Zod
- **Routing**: React Router v6
- **HTTP Client**: Axios
- **Datepicker**: date-fns + MUI Date Picker

### 5.3 DevOps
- **Versionskontrolle**: Git
- **Package Manager**: NuGet (Backend), npm (Frontend)
- **Build**: .NET CLI, npm scripts
- **Testing**: xUnit (Backend), Vitest (Frontend)

---

## 6. Datenmodell (Vereinfacht für MVP)

### 6.1 Entities

#### Contract (Vertrag)
```csharp
- Id: Guid
- ContractNumber: string (unique)
- CustomerId: Guid
- MandantId: Guid
- ContractGroupId: Guid
- ContractType: enum
- Status: enum
- StartDate: DateTime
- EndDate: DateTime?
- IsUnlimited: bool
- NoticePeriodMonths: int
- NoticeDeadline: DateTime?
- AutoRenew: bool
- BillingStartDate: DateTime
- ResponsibleSales: string
- ResponsibleAccounting: string
- ResponsiblePricing: string
- CurrencyId: Guid
- Notes: string
- CreatedAt: DateTime
- CreatedBy: string
- UpdatedAt: DateTime
- UpdatedBy: string
```

#### ContractPrice (Vertragspreise)
```csharp
- Id: Guid
- ContractId: Guid (FK)
- ValidFrom: DateTime
- ValidTo: DateTime?
- PriceTypeId: Guid
- Amount: decimal
- Unit: string
- Description: string
```

#### ContractCustomer (Vertragskunde)
```csharp
- Id: Guid
- ContractId: Guid (FK)
- CustomerId: Guid (FK)
- CustomerNumber: string
- Role: enum (Vertragspartner, Rechnungsempfänger)
- AdvancePaymentAmount: decimal
- AdvancePaymentCycle: enum
- PaymentTerms: string
```

#### Customer (Kunde - vereinfacht)
```csharp
- Id: Guid
- CustomerNumber: string
- Name: string
- Address: string
- Email: string
```

#### Mandant
```csharp
- Id: Guid
- Name: string
- Code: string
```

#### ContractGroup (Vertragsgruppe)
```csharp
- Id: Guid
- Name: string
- Code: string
```

---

## 7. API-Endpunkte (MVP)

### 7.1 Contracts
```
GET    /api/contracts              - Liste alle Verträge
GET    /api/contracts/{id}         - Hole einen Vertrag
POST   /api/contracts              - Erstelle Vertrag
PUT    /api/contracts/{id}         - Update Vertrag
DELETE /api/contracts/{id}         - Lösche Vertrag
GET    /api/contracts/{id}/prices  - Hole Vertragspreise
POST   /api/contracts/{id}/prices  - Erstelle Preis
```

### 7.2 Lookup Data
```
GET /api/customers              - Kunden-Suche
GET /api/mandants               - Mandanten
GET /api/contract-groups        - Vertragsgruppen
GET /api/price-types            - Preisarten
GET /api/currencies             - Währungen
```

### 7.3 Users
```
POST /api/auth/login            - Login
GET  /api/users/me              - Aktueller User
```

---

## 8. UI-Mockups & Screens

### 8.1 Screen 1: Vertrags-Liste
```
┌─────────────────────────────────────────────────────────────┐
│ 🏠 Verträge | Sonderkunden                        👤 Admin │
├─────────────────────────────────────────────────────────────┤
│ [ + Neuer Vertrag ]  [ 🔍 Suche... ]  [ Filter ▼ ]        │
├──────┬──────────┬──────────────┬────────┬──────────┬────────┤
│ Nr   │ Kunde    │ Beginn       │ Ende   │ Status   │ Aktion │
├──────┼──────────┼──────────────┼────────┼──────────┼────────┤
│ V001 │ EVU AG   │ 01.01.2024   │ ∞      │ ✓ Aktiv  │ [⋮]    │
│ V002 │ Stadtw.  │ 15.03.2024   │ 31.12. │ ⚠ Verh.  │ [⋮]    │
│ V003 │ Regional │ 01.06.2023   │ 30.06. │ 🚫 Gekü. │ [⋮]    │
└──────┴──────────┴──────────────┴────────┴──────────┴────────┘
```

### 8.2 Screen 2: Vertrag erstellen (Wizard)
```
┌─────────────────────────────────────────────────────────────┐
│ Neuer Vertrag erstellen                              [X]    │
├─────────────────────────────────────────────────────────────┤
│ ● Stammdaten  ○ Zeiträume  ○ Zuständigkeit  ○ Abrechnung   │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│ Vertragsnummer*:  [____________] ☑ Auto-generieren         │
│                                                              │
│ Kunde*:           [EVU Musterkunde GmbH    ▼]              │
│                                                              │
│ Mandant*:         [Mandant 1                ▼]              │
│                                                              │
│ Vertragsgruppe*:  [Sonderkunden             ▼]              │
│                                                              │
│ Vertragsart*:     [○ Verkauf  ○ Lieferant]                 │
│                                                              │
│ Währung*:         [EUR                      ▼]              │
│                                                              │
│                                   [Abbrechen]  [Weiter →]   │
└─────────────────────────────────────────────────────────────┘
```

### 8.3 Screen 3: Vertrag Details
```
┌─────────────────────────────────────────────────────────────┐
│ ← Zurück  |  Vertrag V001 - EVU AG        [ Bearbeiten ]   │
├─────────────────────────────────────────────────────────────┤
│ [Stammdaten] [Preise] [Kunden] [Abrechnung] [Historie]     │
├─────────────────────────────────────────────────────────────┤
│                                                              │
│ Vertragsinformationen                                       │
│ ┌──────────────────────────────────────────────────────┐   │
│ │ Nummer:        V001                                   │   │
│ │ Status:        ✓ Aktiv                                │   │
│ │ Kunde:         EVU Musterkunde GmbH (K-12345)        │   │
│ │ Beginn:        01.01.2024                            │   │
│ │ Ende:          Unbefristet                           │   │
│ │ Kündigungsfr.: 3 Monate zum Jahresende              │   │
│ │ Sachbearb.:    Max Mustermann (Vertrieb)            │   │
│ └──────────────────────────────────────────────────────┘   │
│                                                              │
│ Preise (gültig ab 01.01.2024)                              │
│ ┌──────────────────────────────────────────────────────┐   │
│ │ Arbeitspreis     0,25 EUR/kWh                        │   │
│ │ Grundpreis      120,00 EUR/Jahr                      │   │
│ └──────────────────────────────────────────────────────┘   │
│                                                              │
└─────────────────────────────────────────────────────────────┘
```

---

## 9. Entwicklungs-Roadmap

### Phase 1: Foundation (Woche 1)
- ✅ PRD erstellen
- ✅ Projektstruktur aufsetzen
- ✅ Backend-Grundgerüst (ASP.NET Core)
- ✅ Frontend-Grundgerüst (React + Vite)
- ✅ Datenmodell implementieren
- ✅ Mock-Daten Generator

### Phase 2: Core Features (Woche 2)
- 📋 API-Endpunkte implementieren
- 📋 Contract CRUD Operations
- 📋 Validierung & Error Handling
- 📋 Frontend: Vertrags-Liste
- 📋 Frontend: Detail-Ansicht

### Phase 3: Wizard & Forms (Woche 3)
- 📋 Vertrag-Erstellungs-Wizard
- 📋 Formular-Validierung
- 📋 Preismodell UI
- 📋 Vertragskunden UI

### Phase 4: Polish & Testing (Woche 4)
- 📋 Unit Tests Backend
- 📋 Component Tests Frontend
- 📋 UI-Verfeinerung
- 📋 Dokumentation
- 📋 Deployment-Vorbereitung

---

## 10. Risiken & Mitigationen

| Risiko | Wahrscheinlichkeit | Impact | Mitigation |
|--------|-------------------|--------|------------|
| Komplexität VB6-Logik unterschätzt | Mittel | Hoch | Schrittweise Migration, MVP-Fokus |
| UI-Konsistenz mit Angular_FM schwierig | Niedrig | Mittel | Design-Tokens extrahieren |
| Performance bei großen Datenmengen | Niedrig | Mittel | Pagination, Indexierung |
| Scope Creep | Hoch | Hoch | Strikter MVP-Fokus, Backlog pflegen |

---

## 11. Erfolgs-Metriken

### Technische Metriken
- ✅ 100% API-Endpunkte dokumentiert
- ✅ > 70% Test-Coverage Backend
- ✅ 0 kritische Security-Issues
- ✅ < 500ms durchschnittliche Response-Zeit

### Business-Metriken
- ✅ Vertrag erstellen in < 3 Minuten
- ✅ User Feedback Score > 4/5
- ✅ Erfolgreiche Migration von 5 Pilot-Verträgen

---

## 12. Offene Fragen

1. ✅ **Authentifizierung**: Eigenes System oder Integration in bestehendes?
   - **MVP**: Einfaches Mock-Auth System
   
2. ✅ **Datenbank**: Produktiv-Datenbank oder Test-Umgebung?
   - **MVP**: In-Memory SQLite für Entwicklung

3. ⚠️ **Deployment**: Wo soll die Anwendung gehostet werden?
   - **Später klären**

4. ⚠️ **Integration**: Schnittstellen zu anderen Systemen nötig?
   - **Post-MVP**

---

## 13. Anhang

### 13.1 Glossar
- **EVU**: Energieversorgungsunternehmen
- **Sonderkunde**: Kunde mit individuellen Vertragskonditionen außerhalb Standard-SAP
- **Vertragsgruppe**: Kategorisierung von Verträgen
- **Abrechnungsstelle**: Ort/Punkt der Energieabnahme
- **Preisart**: Typ des Preises (Arbeitspreis, Grundpreis, Netznutzung, etc.)

### 13.2 Referenzen
- VB6 Source: `f:\source\Rita_Neuentwicklung_KI\VB6_Migration\VB6_Source_Reference\Verträge\`
- Angular FM: `f:\source\Angular_FM\src\app\groups\contracts\`
- Migration Docs: `f:\source\Rita_Neuentwicklung_KI\VB6_Migration\README.md`

---

**Änderungshistorie**

| Version | Datum | Autor | Änderung |
|---------|-------|-------|----------|
| 1.0 | 13.11.2025 | KI | Initial Draft |

