# API-контракт

## Таблица методов

| Метод и путь | Тело | Успех | Возможная ошибка |
|---|---|---|---|
| GET /api/atms | — | 200, массив или [] | — |
| GET /api/atms/{id} | — | 200, объект | 404 |
| POST /api/atms | number, model, address, zoneId | 201, id, status: Active | 400 |
| GET /api/service-requests | — | 200, массив или [] | — |
| POST /api/service-requests | title, description, type, dueDate, atmId | 201, id, number, status: New | 400 |
| PATCH /api/service-requests/{id}/assignee | assigneeUserId | 200 | 404 |
| PATCH /api/service-requests/{id}/status | status | 200 | 404, 409 |

Клиент не присылает при создании id, number, status и исполнителя —
их определяет сервер.

## Примеры JSON

POST /api/atms — запрос
```json
{
  "number": "ATM-2026-001",
  "model": "NCR SelfServ 82",
  "address": "г. Москва, ул. Тверская, 12",
  "zoneId": 3
}
```
POST /api/atms — ответ 201
```json
{
  "id": 12,
  "number": "ATM-2026-001",
  "model": "NCR SelfServ 82",
  "address": "г. Москва, ул. Тверская, 12",
  "status": "Active",
  "zoneId": 3
}
```
POST /api/service-requests — запрос
```json
{
  "title": "Плановое ТО",
  "description": "Замена картриджей, чистка диспенсера",
  "type": "Scheduled",
  "dueDate": "2026-10-05T09:00:00",
  "atmId": 12
}
```
POST /api/service-requests — ответ 201
```json
{
  "id": 87,
  "number": "SR-2026-087",
  "title": "Плановое ТО",
  "status": "New",
  "type": "Scheduled",
  "dueDate": "2026-10-05T09:00:00",
  "atmId": 12,
  "createdByUserId": 1,
  "assigneeUserId": null
}
```
PATCH /api/service-requests/87/assignee — запрос
```json
{ "assigneeUserId": 5 }
```
PATCH /api/service-requests/87/status — запрос
```json
{ "status": "InProgress" }
```
Ошибка 409
```json
{ "error": "Недопустимый переход статуса: Closed → InProgress" }
```

