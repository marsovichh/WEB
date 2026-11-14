using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public class PatchStatusRequest
{
    [Required]
    public string Status { get; set; } = string.Empty;
}