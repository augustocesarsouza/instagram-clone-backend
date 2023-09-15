using Microsoft.EntityFrameworkCore;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;

namespace ProjectInsta.Infra.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ApplicationDbContext _context;

        public MessageRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Message?> GetById(int idMessagfe)
        {
            var message = await _context.Messages.Where(m => m.Id == idMessagfe).FirstOrDefaultAsync();

            return message;
        }

        public async Task<ICollection<Message>> GetAllMessageSenderUserForRecipientUserAsyncPagaginada(int senderUserId, int recipientUserId, int pagina, int registroPorPagina)
        {
            var messages = await _context // tem que verificar se a mensagem tem reel associado
                .Messages
                .Where(x => x.SenderId == senderUserId && x.RecipientId == recipientUserId)
                .Concat(_context.Messages.Where(x => x.SenderId == recipientUserId && x.RecipientId == senderUserId))
                .OrderByDescending(x => x.Id)
                .Skip((pagina - 1) * registroPorPagina)
                .Take(registroPorPagina)
                .ToListAsync();

            return messages;
        }

        public async Task<Message> CreateAsync(Message message)
        {
            await _context.Messages.AddAsync(message);
            await _context.SaveChangesAsync();

            return message;
        }

        public async Task<Message> DeleteAsync(Message message)
        {
            _context.Messages.Remove(message);
            await _context.SaveChangesAsync();

            return message;
        }
    }
}
