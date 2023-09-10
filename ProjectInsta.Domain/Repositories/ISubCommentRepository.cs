using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Domain.Repositories
{
    public interface ISubCommentRepository
    {
        public Task<SubComment> GetByIdCreateAsync(int id);
        public Task<ICollection<SubComment>> GetAllAsync();
        public Task<ICollection<SubComment>> GetByCommentIdAsync(int commentId, int pagina, int registroPorPagina);
        public Task<ICollection<SubComment>> GetByCommentId(int commentId);
        public Task<SubComment> CreateAsync(SubComment subComment);
        public Task<SubComment> DeleteAsync(SubComment subComment);
        
    }
}
