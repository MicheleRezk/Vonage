using Vonage.ContactCenter.Models;

namespace Vonage.ContactCenter.Common
{
    public interface IEmployee
    {
        string Name { get; init; }
        EmployeeTypeEnum Type { get; init; }
        IEnumerable<Interaction> RunningInteractions { get; set; }
        bool IsBusy { get; set; }
        Task HandleInteraction(Interaction interaction);
    }
}