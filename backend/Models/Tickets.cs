namespace backend.Models;

public class Ticket
{
    public int Id { get; set; }
    public string Number { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = "New";
    public string Type { get; set; } = "Scheduled";
    public int AtmId { get; set; }
    public int? AssigneeUserId { get; set; } // null, если не назначен
    public DateTime CreatedAt { get; set; }
}