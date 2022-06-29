using System.ComponentModel.DataAnnotations;
using Vonage.ContactCenter.Models;

namespace Vonage.ContactCenter.Dtos
{
    public record HandleInteractionResponseDto(string Response, InteractionStatusEnum Status, EmployeeTypeEnum? handledBy);
    public record HandleInteractionDto([Required] InteractionTypeEnum Type);
}
