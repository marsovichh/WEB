using backend.Models;

namespace backend.Services;

public class TicketService
{
    // Хранилище в памяти (до ЛР9)
    private readonly List<Ticket> _tickets = new();
    private int _nextId = 1;

    public TicketService()
    {
        // Добавим одну тестовую запись для наглядности
        _tickets.Add(new Ticket
        {
            Id = _nextId++,
            Number = $"SR-{DateTime.UtcNow.Year}-{_nextId:D3}",
            Title = "Плановое ТО",
            Description = "Тестовая заявка",
            Status = "New",
            Type = "Scheduled",
            AtmId = 1,
            CreatedAt = DateTime.UtcNow
        });
    }

    public IEnumerable<Ticket> GetAll() => _tickets;

    public Ticket? GetById(int id) => _tickets.FirstOrDefault(t => t.Id == id);

    public Ticket Create(string title, int atmId, string description, string type)
    {
        var newId = _nextId++;
        var ticket = new Ticket
        {
            Id = newId,
            Number = $"SR-{DateTime.UtcNow.Year}-{newId:D3}",
            Title = title,
            AtmId = atmId,
            Description = description,
            Type = type,
            Status = "New", // Сервер всегда ставит New
            CreatedAt = DateTime.UtcNow
        };
        _tickets.Add(ticket);
        return ticket;
    }

    // Возвращает true, если назначение прошло успешно
    public bool Assign(int id, int assigneeUserId)
    {
        var ticket = GetById(id);
        if (ticket == null) return false;
        
        ticket.AssigneeUserId = assigneeUserId;
        return true;
    }

    // Возвращает статус ответа: (успех, требуется_назначение)
    public (bool success, bool needAssignee) ChangeStatus(int id, string newStatus)
    {
        var ticket = GetById(id);
        if (ticket == null) return (false, false);

        // Правило 409: нельзя в работу без исполнителя
        if (newStatus == "InProgress" && ticket.AssigneeUserId == null)
        {
            return (false, true); 
        }

        // Проверка допустимых статусов
        var allowed = new[] { "New", "InProgress", "Closed", "Cancelled" };
        if (!allowed.Contains(newStatus)) return (false, false);

        ticket.Status = newStatus;
        return (true, false);
    }
}