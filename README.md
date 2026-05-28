# BookRight – Klinik & Wellness Bookingsystem

BookRight er en prototype på et digitalt bookingsystem til **BookRight Klinik & Wellness ApS**, udviklet som en 2. semesters tværfaglig eksamensprojekt på Datamatikeruddannelsen (DMVE251, Forår 2026).

Systemet er bygget med **C# / .NET 10** med **Blazor Server** som brugergrænseflade og **Clean Architecture** (med et Facade-lag) som arkitekturprincip.

---

## Indholdsfortegnelse

- [Teknologier](#teknologier)
- [Opsætning og kørsel](#opsætning-og-kørsel)
- [Tests](#tests)
- [Arkitektur – kort overblik](#arkitektur--kort-overblik)

---

## Teknologier

| Teknologi

- .NET , 10.0 |
- Blazor , Server-side (Interactive Server) |
- EntityFramework Core , 10.0.8 |
- SQL Server , LocalDB |
- xUnit , Seneste stabile |
- Moq , Seneste stabile |

---

## Opsætning og kørsel

### 1. Klon repositoriet

```
git clone https://github.com/Sheephurrdurr/BookRight.git
cd BookRight
```

### 2. Konfigurér connection string

Åbn `BookRight.BlazorUI/appsettings.Development.json` og udfyld din connection string:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BookRightDb;Trusted_Connection=True;"
  }
}
```

### 4. Kør applikationen

Via Visual Studio: Sæt `BookRight.BlazorUI` som startup-projekt og tryk **F5**.

Via terminalen:

```
cd BookRight.BlazorUI
dotnet run
```
---

## Database og migrationer

Projektet bruger **EF Core Code First**. Databaseskemaet genereres ud fra domænemodellen.

### Anvend migrationer (opret/opdatér databasen)

Migrationen `InitialCreate` er allerede inkluderet i repositoriet. Kør følgende for at oprette databasen:

```
cd BookRight.Infrastructure
dotnet ef database update
```

### Seed-data

Applikationen indeholder en `DbSeeder`, som automatisk kører ved opstart i udviklingsmiljøet og fylder databasen med testdata (klinikker, behandlere, behandlingstyper m.m.). 
Seederen aktiveres i `Program.cs`, i BlazorUI projektet.

---

## Tests

Projektet indeholder tre testprojekter:
BookRight.Domain.Test
BookRight.UseCase.Test
BookRight.Performance.Test

### Kør alle tests

```
dotnet test
```
---
*Eksamensprojekt – DMVE251, 2. semester, Datamatikeruddannelsen, UCL Erhvervsakademi og Professionshøjskole, Forår 2026.*
