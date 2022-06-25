using Vonage.ContactCenter.Models;

namespace Vonage.ContactCenter.Common
{
    public interface IContactCenterServices
    {
        Task AllocateInteraction(Interaction interaction);
    }
}
