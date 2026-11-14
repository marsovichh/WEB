using backend.Dtos;
using backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers;

[ApiController]
[Route("api/tickets")]
public class TicketsController : ControllerBase
{
    private readonly TicketService _service;

    // Dependency Injection: получаем сервис из контейнера
    public TicketsController(TicketService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll() => Ok(_service.GetAll());

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var ticket = _service.GetById(id);
        if (ticket == null) return NotFound(new { message = "Заявка не найдена" });
        return Ok(ticket);
    }

    [HttpPost]
    public IActionResult Create([FromBody] CreateTicketRequest request)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // ВРЕМЕННО: Искусственная ошибка для проверки 500
       // throw new InvalidOperationException($"CreateTicket failed; atmId={request.AtmId}"); 

        var created = _service.Create(request.Title, request.AtmId, request.Description, request.Type);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
    }

    [HttpPatch("{id}/assignee")]
    public IActionResult Assign(int id, [FromBody] PatchAssigneeRequest request)
    {
        var success = _service.Assign(id, request.AssigneeUserId);
        if (!success) return NotFound(new { message = "Заявка не найдена" });

        return Ok(_service.GetById(id));
    }

    [HttpPatch("{id}/status")]
    public IActionResult ChangeStatus(int id, [FromBody] PatchStatusRequest request)
    {
        var (success, needAssignee) = _service.ChangeStatus(id, request.Status);

        if (needAssignee) 
        {
            // 409 Conflict
            return Conflict(new { message = "Нельзя взять в работу заявку без назначенного инженера" });
        }
        
        if (!success) return NotFound(new { message = "Заявка не найдена или неверный статус" });

        return Ok(_service.GetById(id));
    }
}