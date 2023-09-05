using AutoMapper;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.Validations.CommentValidator;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Repositories;

namespace ProjectInsta.Application.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly ILikeCommentService _likeCommentService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISubCommentService _subCommentService;

        public CommentService(ICommentRepository commentRepository, IMapper mapper, IUnitOfWork unitOfWork, ISubCommentService subCommentService, ILikeCommentService likeCommentService)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _subCommentService = subCommentService;
            _likeCommentService = likeCommentService;
        }

        public async Task<ResultService<ICollection<CommentDTO>>> GetAllCommentsAsync()
        {
            var comments = await _commentRepository.GetAllCommentsAsync();
            return ResultService.Ok(_mapper.Map<ICollection<CommentDTO>>(comments));
        }

        public async Task<ResultService<ICollection<CommentDTO>>> GetCommentsByPostIdAsync(int postId)
        {
            var comment = await _commentRepository.GetByPostIdAsync(postId);
            if (comment == null)
                return ResultService.Fail<ICollection<CommentDTO>>("Comentario não encontrado");

            return ResultService.Ok(_mapper.Map<ICollection<CommentDTO>>(comment));
        }

        public async Task<ResultService<ICollection<CommentDTO>>> GetLikeCommentsInfo(int postId)
        {
            var comment = await _commentRepository.GetLikeCommentsInfo(postId);
            if (comment == null)
                return ResultService.Fail<ICollection<CommentDTO>>("Comentario não encontrado");

            return ResultService.Ok(_mapper.Map<ICollection<CommentDTO>>(comment));
        }

        public async Task<ResultService<ICollection<CommentDTO>>> GetByPostIdAsyncForReels(int postId)
        {
            var comment = await _commentRepository.GetByPostIdAsyncForReels(postId);
            if (comment == null)
                return ResultService.Fail<ICollection<CommentDTO>>("Comentario não encontrado");

            return ResultService.Ok(_mapper.Map<ICollection<CommentDTO>>(comment));
        }

        public async Task<ResultService<CommentDTO>> CreateCommentAsync(CommentDTO commentDTO)
        {
            if (commentDTO == null)
                return ResultService.Fail<CommentDTO>("Objeto nao pode ser null");

            var validate = new CommentDTOValidator().Validate(commentDTO);
            if (!validate.IsValid)
                return ResultService.RequestError<CommentDTO>("Erro de validação", validate);

            commentDTO.CreatedAt = DateTime.Now;
            commentDTO.UpdatedAt = DateTime.Now;

            var data = await _commentRepository.CreateCommentAsync(_mapper.Map<Comment>(commentDTO));
            return ResultService.Ok(_mapper.Map<CommentDTO>(data));
        }

        public async Task<ResultService<CommentDTO>> DeleteAsync(int? userId, int? postId)
        {
            var comment = await _commentRepository.GetByUserIdAndPostId(userId, postId); // tentar diminuir aqui o get do SubCOmment para pegar só UserId e CommentId
            if (comment == null)
                return ResultService.Fail<CommentDTO>("Não encontramos o objeto");

            await _subCommentService.DeleteAsync(comment.Id);

            await _likeCommentService.RemoveAsyncNoReturn(comment.Id);

            try
            {
                await _unitOfWork.BeginTransaction();

                var data = await _commentRepository.DeleteAsync(comment);

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<CommentDTO>(data));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<CommentDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<CommentDTO>> DeleteByCommentIdAsync(int commentId)
        {
            var comment = await _commentRepository.GetByCommentIdAsync(commentId); // tentar diminuir aqui o get do SubCOmment para pegar só UserId e CommentId
            if (comment == null)
                return ResultService.Fail<CommentDTO>("Não encontramos o objeto");

            await _subCommentService.DeleteAsync(comment.Id);

            await _likeCommentService.RemoveAsyncNoReturn(comment.Id);

            try
            {
                await _unitOfWork.BeginTransaction();

                var data = await _commentRepository.DeleteAsync(comment);

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<CommentDTO>(data));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<CommentDTO>($"{ex.Message}");
            }
        }

        public async Task DeleteAsyncNotReturn(int? postId)
        {
            var comment = await _commentRepository.GetByPostIdAll(postId); 
            if (comment == null)
                Console.WriteLine("Não encontrado!");

            var commentId = comment.FirstOrDefault();
            if (commentId != null)
            {
                await _subCommentService.DeleteAsync(commentId.Id);//Aqui ta errado tem que mandar todos os comments

                await _likeCommentService.RemoveAsyncNoReturnAll(_mapper.Map<ICollection<CommentDTO>>(comment));
            }

            try
            {
                await _unitOfWork.BeginTransaction();

                foreach (var item in comment)
                {
                    var data = await _commentRepository.DeleteAsync(item);
                }
                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                Console.WriteLine($"{ex.Message}");
            }
        }

    }
}
