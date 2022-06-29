using Microsoft.Extensions.Options;
using Vonage.ContactCenter.Settings;

namespace Vonage.ContactCenter.Tests.MockingData
{
    internal class ContactCenterMockingData
    {
        public static IOptions<ServiceSettings> GetServiceSettingsOptions(
            int AgentsNumber, 
            int supervisorsNumber, 
            int averageVoiceInteractionInMilliseconds = 100, 
            int averageNonVoiceInteractionInMilliseconds = 100)
        {
            return Options.Create<ServiceSettings>(new ServiceSettings
            {
               AgentsNumber = AgentsNumber,
               SupervisorsNumber = supervisorsNumber,
               AverageVoiceInteractionInMilliseconds = averageVoiceInteractionInMilliseconds,
               AverageNonVoiceInteractionInMilliseconds = averageNonVoiceInteractionInMilliseconds
            });
        }
    }
}
