using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Vonage.ContactCenter.Common;
using Vonage.ContactCenter.Dtos;
using Vonage.ContactCenter.Models;
using Vonage.ContactCenter.Settings;

namespace Vonage.ContactCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InteractionsController : ControllerBase
    {
        private readonly IContactCenterServices _contactCenterServices;
        private readonly ServiceSettings _serviceSettings;
        public InteractionsController(IContactCenterServices contactCenterServices, IOptions<ServiceSettings> serviceSettings)
        {
            _contactCenterServices = contactCenterServices;
            _serviceSettings = serviceSettings.Value;
        }
        // POST /api/interactions/
        [HttpPost]
        public async Task<ActionResult<HandleInteractionResponseDto>> PostAsync(HandleInteractionDto interactionDto)
        {
            Console.WriteLine($"--> Try Handling a new {interactionDto.Type}....");
            var completesAfterMilliseconds = interactionDto.Type == InteractionTypeEnum.Voice ?
                                _serviceSettings.AverageVoiceInteractionInMilliseconds :
                                _serviceSettings.AverageNonVoiceInteractionInMilliseconds;
            var result = await _contactCenterServices.AllocateInteraction(new Interaction(interactionDto.Type, completesAfterMilliseconds));
            Console.WriteLine(result.Response);
            return Ok(result);
        }
    }
}
