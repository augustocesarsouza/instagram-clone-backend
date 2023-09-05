using ProjectInsta.Application.DTOs;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface ISubCommentService
    {
        public Task<ResultService<SubCommentDTO>> GetByIdAsync(int id);
        public Task<ResultService<ICollection<SubCommentDTO>>> GetAllAsync();
        public Task<ResultService<ICollection<SubCommentDTO>>> GetByCommentIdAsync(int commentId, int pagina, int registroPorPagina);
        public Task<ResultService<SubCommentDTO>> CreateAsync(SubCommentDTO subCommentDTO);
        public Task DeleteAsync(int commentId);
        //public Task<ResultService<SubCommentDTO>> DeleteAsync(int commentId);
    }
}
