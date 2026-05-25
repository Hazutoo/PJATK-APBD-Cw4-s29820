# PJATK-APBD-Cw4-s29820

REST API do zarządzania komputerami i komponentami z użyciem ASP.NET Core, EF Core Code First i SQL Server LocalDB.

## Uruchomienie

dotnet restore
dotnet build
dotnet ef database update
dotnet run

Swagger:
http://localhost:5107/swagger

## Endpointy

GET /api/pcs
GET /api/pcs/{id}/components
POST /api/pcs
PUT /api/pcs/{id}
DELETE /api/pcs/{id}