using ProjectInsta.Application.DTOs;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface ISubCommentService
    {
        public Task<ResultService<ICollection<SubCommentDTO>>> GetByCommentIdAsync(int commentId, int pagina, int registroPorPagina);
        public Task<ResultService<SubCommentDTO>> CreateAsync(SubCommentDTO subCommentDTO);
        public Task DeleteAsync(int commentId);
    }
}
