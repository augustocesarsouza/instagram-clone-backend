using ProjectInsta.Application.DTOs;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface IMessageService
    {
        public Task<ResultService<ICollection<MessageDTO>>> GetAllMessageSenderUserForRecipientUserAsyncPagaginada(int senderUserId, int recipientUserId, int pagina, int registroPorPagina);
        public Task<ResultService<MessageDTO>> CreateAsync(MessageDTO messageDTO);
        public Task<ResultService<MessageDTO>> UpdateAsync(int messageId);
        public Task<ResultService<MessageDTO>> DeleteAsync(int idMessage);
    }
}
