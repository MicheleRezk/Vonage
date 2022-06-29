using System.Collections.Concurrent;
using Vonage.ContactCenter.Models;

namespace Vonage.ContactCenter.Common
{
    public interface IEmployee: IComparable<IEmployee>
    {
        string Name { get; init; }
        EmployeeTypeEnum Type { get; init; }
        ICollection<Interaction> RunningInteractions { get; init; }
        int MaximumInteractionAtATime { get; init; }
        bool IsFree { get; }
        bool CanHandleOnlyNonVoice { get; }
        Task HandleInteraction(Interaction interaction);
    }
}