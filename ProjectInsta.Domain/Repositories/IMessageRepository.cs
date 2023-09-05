using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface IMessageRepository
    {
        public Task<ICollection<Message>> GetAllMessageSenderUserForRecipientUserAsync(int senderUserId, int recipientUserId);
        public Task<ICollection<Message>> GetAllMessageSenderUserForRecipientUserAsyncPagaginada(int senderUserId, int recipientUserId, int pagina, int registroPorPagina);
        public Task<Message> CreateAsync(Message message);
    }
}
