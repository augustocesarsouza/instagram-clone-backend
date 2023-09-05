using AutoMapper;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.Validations.LikeCommentValidator;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;

namespace ProjectInsta.Application.Services
{
    public class LikeCommentService : ILikeCommentService
    {
        private readonly ILikeCommentRepository _likeCommentRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public LikeCommentService(ILikeCommentRepository likeCommentRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _likeCommentRepository = likeCommentRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;

        }

        public async Task<ResultService<ICollection<LikeCommentDTO>>> GetAsync()
        {
            var likes = await _likeCommentRepository.GetAsync();
            if (likes.Count <= 0)
                return ResultService.Fail<ICollection<LikeCommentDTO>>("Error não foi encontrado nada");

            return ResultService.Ok(_mapper.Map<ICollection<LikeCommentDTO>>(likes));
        }

        public async Task<ResultService<ICollection<LikeCommentDTO>>> GetAllLikeCommentId(int commentId)
        {
            var likes = await _likeCommentRepository.GetAllLikeCommentId(commentId);
            //if (likes.Count <= 0)
            //    return ResultService.Fail<ICollection<LikeCommentDTO>>("Error não foi encontrado nada");

            return ResultService.Ok(_mapper.Map<ICollection<LikeCommentDTO>>(likes));
        }

        public async Task<ResultService<LikeCommentDTO>> CreateAsync(LikeCommentDTO likeCommentDTO)
        {
            if (likeCommentDTO == null)
                return ResultService.Fail<LikeCommentDTO>("Erro DTO Não poder ser NULL");

            var validator = new LikeCommentDTOValidator().Validate(likeCommentDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<LikeCommentDTO>("Erro de validação", validator);

            var likesComment = await _likeCommentRepository.GetAuthorIdCommentId(likeCommentDTO.AuthorId, likeCommentDTO.CommentId);
            if (likesComment != null)
            {
                var likeDelete = await _likeCommentRepository.RemoveAsync(likesComment);
                var test = ResultService.Ok(_mapper.Map<LikeCommentDTO>(likeDelete));
                test.IsSucess = false;
                test.IsSucessDeleteLike = true;

                return test;
            }

            try
            {
                await _unitOfWork.BeginTransaction();
                var likeComment = await _likeCommentRepository.CreateAsync(_mapper.Map<LikeComment>(likeCommentDTO));
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<LikeCommentDTO>(likeComment));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<LikeCommentDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<LikeCommentDTO>> RemoveAsync(int authorId, int commentId)
        {
            var likeComment = await _likeCommentRepository.GetByCommentIdAndAuthorId(commentId, authorId);
            if (likeComment == null)
                return ResultService.Fail<LikeCommentDTO>("Não existe este registro");

            try
            {
                await _unitOfWork.BeginTransaction();
                var data = await _likeCommentRepository.RemoveAsync(likeComment);
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<LikeCommentDTO>(data));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<LikeCommentDTO>($"{ex.Message}");
            }
        }

        public async Task RemoveAsyncNoReturn(int commentId)
        {
            var likeComment = await _likeCommentRepository.GetByCommentId(commentId);
            if (likeComment == null)
                Console.WriteLine("Não existe");

            try
            {
                await _unitOfWork.BeginTransaction();

                foreach (var lc in likeComment)
                {
                    var data = await _likeCommentRepository.RemoveAsync(lc);
                }

                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                Console.WriteLine($"{ex.Message}");
            }
        }

        public async Task RemoveAsyncNoReturnAll(ICollection<CommentDTO> commentDTOs)
        {
            try
            {
                await _unitOfWork.BeginTransaction();

                foreach (var comment in commentDTOs)
                {
                    var likeComment = await _likeCommentRepository.GetByCommentId(comment.Id);
                    if (likeComment.Count == 0)
                    {

                        Console.WriteLine("Não existe");
                    }
                    else
                    {
                        foreach (var lc in likeComment)
                        {
                            var data = await _likeCommentRepository.RemoveAsync(lc);
                        }
                    }
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
