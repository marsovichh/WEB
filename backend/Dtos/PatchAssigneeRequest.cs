using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public class PatchAssigneeRequest
{
    [Required]
    [Range(1, int.MaxValue)]
    public int AssigneeUserId { get; set; }
}