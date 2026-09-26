# Модель данных

## Словарь проекта

| Термин курса | Термин моей темы |
|---|---|
| Ticket | ServiceRequest (заявка на обслуживание) |
| Site   | ServiceZone (участок обслуживания) |
| User   | User (диспетчер / инженер) |

## Сущности

### Atm (банкомат) — главный объект

| Поле | Тип | Ключ | Описание |
|---|---|---|---|
| id | int | PK | машинный идентификатор |
| number | string | | человеческий номер (ATM-2026-001) |
| model | string | | модель устройства |
| address | string | | адрес установки |
| status | enum | | New / InProgress / Closed / Cancelled |
| zoneId | int | FK → ServiceZone.id | участок обслуживания |
| createdAt | datetime | | дата регистрации |

### ServiceRequest (заявка на обслуживание)

| Поле | Тип | Ключ | Описание |
|---|---|---|---|
| id | int | PK | машинный идентификатор |
| number | string | | человеческий номер (SR-2026-001) |
| title | string | | краткое описание работы |
| description | text | | подробности |
| status | enum | | New / InProgress / Closed / Cancelled |
| type | enum | | Scheduled / Repair / CashCollection |
| dueDate | datetime | | плановый срок выполнения |
| atmId | int | FK → Atm.id | какой банкомат |
| createdByUserId | int | FK → User.id | кто создал |
| assigneeUserId | int, nullable | FK → User.id | инженер (может отсутствовать) |

### ServiceZone (участок обслуживания) — справочник

| Поле | Тип | Ключ |
|---|---|---|
| id | int | PK |
| name | string | |

### User (пользователь)

| Поле | Тип | Ключ |
|---|---|---|
| id | int | PK |
| login | string | |
| fullName | string | |
| role | enum | Dispatcher / Engineer |

## Связи

- Один ServiceZone — много Atm (1:N).
- Один Atm — много ServiceRequest (1:N).
- Один User создаёт много ServiceRequest (1:N).
- Один User (инженер) может быть назначен на много ServiceRequest (1:N).
- У новой заявки исполнитель может отсутствовать (assigneeUserId = NULL).

## ER-диаграмма (текстом)
<img width="1807" height="870" alt="ChatGPT Image 26 сент  2026 г , 12_24_13" src="https://github.com/user-attachments/assets/e1900c33-57e8-4e21-942c-867323c422d2" />

