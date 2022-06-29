using System.Threading.Tasks;
using Vonage.ContactCenter.Models;
using Vonage.ContactCenter.Services;
using Vonage.ContactCenter.Tests.MockingData;
using Xunit;

namespace Vonage.ContactCenter.Tests.Services
{
    public class ContactCenterServicesTests
    {
        [Fact]
        public async Task AllocateInteraction_ShouldHandledByAgent()
        {
            //Arrange
            var serviceSettingsOptions = ContactCenterMockingData.GetServiceSettingsOptions(2, 1);
            var serviceSettings = serviceSettingsOptions.Value;
            var interaction = new Interaction(InteractionTypeEnum.Voice, serviceSettings.AverageVoiceInteractionInMilliseconds);
            var employeesRepo = new EmployeesRepo(serviceSettingsOptions);

            //Act
            var sut = new ContactCenterServices(employeesRepo);
            var responseDto = await sut.AllocateInteraction(interaction);

            //Assert
            Assert.NotNull(responseDto);
            Assert.Equal(EmployeeTypeEnum.Agent, responseDto.handledBy);
            Assert.Equal(InteractionStatusEnum.Running, responseDto.Status);
        }
        [Fact]
        public async Task AllocateInteraction_WhenAgentsBusy_ShouldHandledBySupervisor()
        {
            //Arrange
            var serviceSettingsOptions = ContactCenterMockingData.GetServiceSettingsOptions(1, 1, 240000, 120000);
            var serviceSettings = serviceSettingsOptions.Value;
            var textInteraction = new Interaction(InteractionTypeEnum.NonVoice, serviceSettings.AverageNonVoiceInteractionInMilliseconds);
            var voiceInteraction = new Interaction(InteractionTypeEnum.Voice, serviceSettings.AverageVoiceInteractionInMilliseconds);
            var employeesRepo = new EmployeesRepo(serviceSettingsOptions);

            //Act
            var sut = new ContactCenterServices(employeesRepo);
            var textResponseDto = await sut.AllocateInteraction(textInteraction);
            var voiceResponseDto = await sut.AllocateInteraction(voiceInteraction);

            //Assert
            Assert.NotNull(textResponseDto);
            Assert.Equal(EmployeeTypeEnum.Agent, textResponseDto.handledBy);
            Assert.Equal(InteractionStatusEnum.Running, textResponseDto.Status);
            Assert.NotNull(voiceResponseDto);
            Assert.Equal(EmployeeTypeEnum.Supervisor, voiceResponseDto.handledBy);
            Assert.Equal(InteractionStatusEnum.Running, voiceResponseDto.Status);
        }

        [Fact]
        public async Task AllocateInteraction_WhenOnlyManagerFree_ShouldHandledByManager()
        {
            //Arrange
            var serviceSettingsOptions = ContactCenterMockingData.GetServiceSettingsOptions(1, 1, 240000, 120000);
            var serviceSettings = serviceSettingsOptions.Value;
            var textInteraction = new Interaction(InteractionTypeEnum.NonVoice, serviceSettings.AverageNonVoiceInteractionInMilliseconds);
            var voiceInteraction = new Interaction(InteractionTypeEnum.Voice, serviceSettings.AverageVoiceInteractionInMilliseconds);
            var voiceInteraction2 = new Interaction(InteractionTypeEnum.Voice, serviceSettings.AverageVoiceInteractionInMilliseconds);
            var employeesRepo = new EmployeesRepo(serviceSettingsOptions);

            //Act
            var sut = new ContactCenterServices(employeesRepo);
            var textResponseDto = await sut.AllocateInteraction(textInteraction);
            var voiceResponseDto = await sut.AllocateInteraction(voiceInteraction);
            var voiceResponseDto2 = await sut.AllocateInteraction(voiceInteraction2);

            //Assert
            Assert.NotNull(textResponseDto);
            Assert.Equal(EmployeeTypeEnum.Agent, textResponseDto.handledBy);
            Assert.NotNull(voiceResponseDto);
            Assert.Equal(EmployeeTypeEnum.Supervisor, voiceResponseDto.handledBy);
            Assert.NotNull(voiceResponseDto2);
            Assert.Equal(EmployeeTypeEnum.GeneralManager, voiceResponseDto2.handledBy);
        }
        [Fact]
        public async Task AllocateInteraction_WhenNoOneFree_ShouldBeRejected()
        {
            //Arrange
            var serviceSettingsOptions = ContactCenterMockingData.GetServiceSettingsOptions(0, 0, 240000, 120000);
            var serviceSettings = serviceSettingsOptions.Value;
            var textInteraction = new Interaction(InteractionTypeEnum.NonVoice, serviceSettings.AverageNonVoiceInteractionInMilliseconds);
            var voiceInteraction = new Interaction(InteractionTypeEnum.Voice, serviceSettings.AverageVoiceInteractionInMilliseconds);
            var employeesRepo = new EmployeesRepo(serviceSettingsOptions);

            //Act
            var sut = new ContactCenterServices(employeesRepo);
            var textResponseDto = await sut.AllocateInteraction(textInteraction);
            var voiceResponseDto = await sut.AllocateInteraction(voiceInteraction);

            //Assert
            Assert.NotNull(textResponseDto);
            Assert.Equal(EmployeeTypeEnum.GeneralManager, textResponseDto.handledBy);
            Assert.Equal(InteractionStatusEnum.Running, textResponseDto.Status);
            Assert.NotNull(voiceResponseDto);
            Assert.Equal(InteractionStatusEnum.Rejected, voiceResponseDto.Status);
        }
    }
}
