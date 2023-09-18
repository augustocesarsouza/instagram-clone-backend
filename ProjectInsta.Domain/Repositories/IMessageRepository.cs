using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface IMessageRepository
    {
        public Task<Message?> GetById(int idMessage);
        public Task<ICollection<Message>> GetAllMessageSenderUserForRecipientUserAsyncPagaginada(int senderUserId, int recipientUserId, int pagina, int registroPorPagina);
        public Task<Message> CreateAsync(Message message);
        public Task<Message> UpdateAsync(Message message);
        public Task<Message> DeleteAsync(Message message);
    }
}
