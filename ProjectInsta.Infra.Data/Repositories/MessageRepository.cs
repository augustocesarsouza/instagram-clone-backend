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

        public async Task<ICollection<Message>> GetAllMessageSenderUserForRecipientUserAsync(int senderUserId, int recipientUserId)
        {
            var messages = await _context
                .Messages
                .Where(x => x.SenderId == senderUserId && x.RecipientId == recipientUserId)
                .Concat(_context.Messages.Where(x => x.SenderId == recipientUserId && x.RecipientId == senderUserId))
                .OrderByDescending(x => x.Timestamp)
                .ToListAsync();

            return messages;
        }

        public async Task<ICollection<Message>> GetAllMessageSenderUserForRecipientUserAsyncPagaginada(int senderUserId, int recipientUserId, int pagina, int registroPorPagina)
        {
            var messages = await _context
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

        
    }
}
