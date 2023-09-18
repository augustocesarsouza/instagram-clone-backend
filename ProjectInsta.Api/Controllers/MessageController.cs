using Microsoft.AspNetCore.Mvc;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Api.Controllers
{
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpGet("v1/message/pagination/{senderUserId}/{recipientUserId}/{pagina}/{registroPorPagina}")]
        public async Task<IActionResult> GetAllMessageSenderUserForRecipientUserAsyncPagaginada
            ([FromRoute] int senderUserId, [FromRoute] int recipientUserId, [FromRoute] int pagina, [FromRoute] int registroPorPagina)
        {
            var result = await _messageService.GetAllMessageSenderUserForRecipientUserAsyncPagaginada(senderUserId, recipientUserId, pagina, registroPorPagina);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("v1/message")]
        public async Task<IActionResult> CreateAsync([FromBody] MessageDTO messageDTO)
        {
            var result = await _messageService.CreateAsync(messageDTO);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut("v1/message/update/{messageId}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int messageId)
        {
            var result = await _messageService.UpdateAsync(messageId);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("v1/message/{idMessage}")]
        public async Task<ActionResult> DeleteAsync([FromRoute] int idMessage)
        {
            var result = await _messageService.DeleteAsync(idMessage);
            if(result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
