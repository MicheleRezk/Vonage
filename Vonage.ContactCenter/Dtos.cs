using Vonage.ContactCenter.Models;

namespace Vonage.ContactCenter.Dtos
{
    public record HandleInteractionResponseDto(string Response, InteractionStatusEnum Status);
}
