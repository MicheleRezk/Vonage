using Vonage.ContactCenter.Dtos;
using Vonage.ContactCenter.Models;

namespace Vonage.ContactCenter.Common
{
    public interface IContactCenterServices
    {
        Task<HandleInteractionResponseDto> AllocateInteraction(Interaction interaction);
    }
}
