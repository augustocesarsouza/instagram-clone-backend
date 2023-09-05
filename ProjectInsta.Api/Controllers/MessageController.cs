﻿using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("v1/messages/{senderUserId}/{recipientUserId}")]
        public async Task<IActionResult> GetAllMessageSenderUserForRecipientUserAsync([FromRoute] int senderUserId, [FromRoute] int recipientUserId)
        {
            var result = await _messageService.GetAllMessageSenderUserForRecipientUserAsync(senderUserId, recipientUserId);
            if (result.IsSucess)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("v1/messages/{senderUserId}/{recipientUserId}/{pagina}/{registroPorPagina}")]
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
    }
}