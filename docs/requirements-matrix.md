# Матрица требований

| Требование ЛР1 | Поле или сущность | Запрос | Критерий |
|---|---|---|---|
| Создать объект | Atm: number, model, address, zoneId | POST /api/atms | 1 |
| Назначить исполнителя | ServiceRequest.assigneeUserId | PATCH /api/service-requests/{id}/assignee | 2 |
| Перевести в работу | ServiceRequest.status | PATCH /api/service-requests/{id}/status | 3 |
