# Архитектура

## Схема

```mermaid
flowchart LR

    VUE["Vue 3 + Vite<br/>localhost:5173<br/><br/>Реестр банкоматов<br/>Заявки на обслуживание<br/>Графики ТО"]

    API["ASP.NET Core Web API<br/>localhost:5000<br/><br/>REST API<br/>Бизнес-правила<br/>Валидация<br/>Права ролей<br/>Назначение инженеров"]

    DB[("PostgreSQL<br/>localhost:5432<br/><br/>Atm<br/>ServiceRequest<br/>ServiceZone<br/>User")]

    VUE -->|"HTTP / REST<br/>POST /api/atms<br/>POST /api/tickets<br/>GET /api/atms"| API

    API -->|"Entity Framework Core / Npgsql<br/>SQL"| DB
```

## Подпись

архитектура web-ИС семестра

## Пояснение

Клиент Vue рисует экран реестра банкоматов и графиков ТО и отправляет
HTTP-запросы на ASP.NET Core. Сервер проверяет бизнес-правила
(сроки ТО, допустимые статусы, права ролей) и обращается к PostgreSQL.
Прямой стрелки от браузера к базе нет: иначе клиент получил бы доступ
к данным и обошёл серверные правила (например, назначение инженера
или смену статуса заявки).
