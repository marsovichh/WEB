using System.ComponentModel.DataAnnotations;

namespace backend.Dtos;

public class CreateTicketRequest
{
    [Required(ErrorMessage = "Тема обязательна")]
    [StringLength(80, MinimumLength = 5, ErrorMessage = "Тема должна быть от 5 до 80 символов")]
    public string Title { get; set; } = string.Empty;

    [Required(ErrorMessage = "ID банкомата обязателен")]
    [Range(1, int.MaxValue, ErrorMessage = "ID банкомата должен быть больше нуля")]
    public int AtmId { get; set; } // В методичке siteId, у нас atmId

    [Required(ErrorMessage = "Описание обязательно")]
    [StringLength(500, MinimumLength = 10, ErrorMessage = "Описание должно быть от 10 до 500 символов")]
    public string Description { get; set; } = string.Empty;

    public string Type { get; set; } = "Scheduled"; // По умолчанию ТО
}