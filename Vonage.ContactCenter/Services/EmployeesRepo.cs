using Microsoft.Extensions.Options;
using Vonage.ContactCenter.Common;
using Vonage.ContactCenter.Models;
using Vonage.ContactCenter.Settings;

namespace Vonage.ContactCenter.Services
{
    public class EmployeesRepo : IEmployeesRepo
    {
        private readonly ServiceSettings _serviceSettings;
        public EmployeesRepo(IOptions<ServiceSettings> serviceSettings)
        {
            _serviceSettings = serviceSettings.Value;
        }
        public ICollection<IEmployee> GetAvailableContactEmployeesOrdered()
        {
            if(_serviceSettings.SupervisorsNumber > _serviceSettings.AgentsNumber)
            {
                throw new ArgumentException("Supervisors number must not be more than Agents number");
            }
            var employees = new List<IEmployee>();
            for (int i =1; i<= _serviceSettings.AgentsNumber; i++)
            {
                employees.Add(new Employee($"Agent_{i}", EmployeeTypeEnum.Agent, 2));
            }
            for (int i = 1; i <= _serviceSettings.SupervisorsNumber; i++)
            {
                employees.Add(new Employee($"Supervisor_{i}", EmployeeTypeEnum.Supervisor, 2));
            }

            employees.Add(new Employee($"General Manager", EmployeeTypeEnum.GeneralManager, 1));

            employees.Sort();

            return employees;
        }
    }
}
