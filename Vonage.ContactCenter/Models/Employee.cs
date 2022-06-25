using Vonage.ContactCenter.Models;

namespace Vonage.ContactCenter.Common
{
    public class Employee : IEmployee
    {
        public string Name { get; init; }
        public EmployeeTypeEnum Type { get; init; }
        public IEnumerable<Interaction> RunningInteractions { get; set; }
        public bool IsBusy { get; set; } = false;// ByDefault

        public async Task HandleInteraction(Interaction interaction)
        {
            throw new NotImplementedException();
        }
    }
}
