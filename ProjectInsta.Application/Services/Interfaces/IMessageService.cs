using ProjectInsta.Application.DTOs;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<ResultService<ICollection<MessageDTO>>> GetAllMessageSenderUserForRecipientUserAsync(int senderUserId, int recipientUserId);
        public Task<ResultService<ICollection<MessageDTO>>> GetAllMessageSenderUserForRecipientUserAsyncPagaginada(int senderUserId, int recipientUserId, int pagina, int registroPorPagina);
        public Task<ResultService<MessageDTO>> CreateAsync(MessageDTO messageDTO);
    }
}
