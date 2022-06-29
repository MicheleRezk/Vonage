using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using Vonage.ContactCenter.Dtos;
using Vonage.ContactCenter.Models;
using Xunit;

namespace Vonage.ContactCenter.IntegrationTests
{
    public class InteractionsControllerTests : IntegrationTest
    {
        [Fact]
        public async void HandlingInteractionPostAsync_ShouldReturnResult()
        {
            //Arrange
            var handleInteractionDto = new HandleInteractionDto(InteractionTypeEnum.Voice);

            //Act
            var response = await _testClient.PostAsJsonAsync(ApiRoutes.Interactions.Handle, handleInteractionDto);

            //Assert
            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            Assert.NotNull(content);
            var resultDto = JsonSerializer.Deserialize<HandleInteractionResponseDto>(content, _jsonSerializerOptions);
            Assert.NotNull(resultDto);
            Assert.Equal(InteractionStatusEnum.Running, resultDto.Status);
            Assert.Equal(EmployeeTypeEnum.Agent, resultDto.handledBy);
        }
    }
}