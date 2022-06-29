using Vonage.ContactCenter.Common;
using Vonage.ContactCenter.Dtos;
using Vonage.ContactCenter.Models;

namespace Vonage.ContactCenter.Services
{
    /// <summary>
    /// This service responsible for allocating the new interactions
    /// </summary>
    public class ContactCenterServices : IContactCenterServices
    {
        private readonly IEmployeesRepo _employeesRepo;
        private readonly ICollection<IEmployee> _employees;

        public ContactCenterServices(IEmployeesRepo employeesRepo)
        {
            _employeesRepo = employeesRepo;
            _employees = _employeesRepo.GetAvailableContactEmployeesOrdered();
        }

        public async Task<HandleInteractionResponseDto> AllocateInteraction(Interaction interaction)
        {
            //allocate the interaction to the first available employee, employees here sorted by level (Agent, Supervisor, GeneralManager)
            IEmployee? employee = null;
            if(interaction.Type == InteractionTypeEnum.Voice)
            {
                employee = _employees.Where(e => e.IsFree).FirstOrDefault();
            }
            else
            {
                employee = _employees.Where(e => e.IsFree || e.CanHandleOnlyNonVoice).FirstOrDefault();
            }
            
            if (employee == null)
            {
                var msg = $"There is no free employee can handle this {interaction.Type} interaction now";
                return new HandleInteractionResponseDto(msg, InteractionStatusEnum.Rejected, null);
            }
            employee.HandleInteraction(interaction);
            var responseMsg = $"{employee.Name} is handling the {interaction.Type} interaction";
            return new HandleInteractionResponseDto(responseMsg, InteractionStatusEnum.Running, employee.Type);

        }
    }
}
