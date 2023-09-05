using AutoMapper;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.Validations.PostLikeValidator;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Repositories;

namespace ProjectInsta.Application.Services
{
    public class PostLikeService : IPostLikeService
    {
        private readonly IPostLikeRepository _postLikeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PostLikeService(IPostLikeRepository postLikeRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _postLikeRepository = postLikeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<ICollection<PostLikeDTO>>> GetAllAsync()
        {
            var postLikes = await _postLikeRepository.GetAllAsync();
            return ResultService.Ok(_mapper.Map<ICollection<PostLikeDTO>>(postLikes));
        }

        public async Task<ResultService<PostLikeDTO>> GetByUserIdAndPostId(int userId, int postId)
        {
            var postLikes = await _postLikeRepository.GetByUserIdAndPostId(userId, postId);
            return ResultService.Ok(_mapper.Map<PostLikeDTO>(postLikes));

        }

        public async Task<ResultService<ICollection<PostLikeDTO>>> GetByPostIdAsync(int postId)
        {
            var postLikes = await _postLikeRepository.GetByPostId(postId);
            return ResultService.Ok(_mapper.Map<ICollection<PostLikeDTO>>(postLikes));
        }

        public async Task<ResultService<PostLikeDTO>> CreateAsync(PostLikeDTO postLikeDTO)
        {
            if (postLikeDTO == null)
                return ResultService.Fail<PostLikeDTO>("Error Objeto não pode ser null");

            var validator = new PostLikeDTOValidator().Validate(postLikeDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<PostLikeDTO>("Erro de validação verifique as informações", validator);

            try
            {
                await _unitOfWork.BeginTransaction();

                var postLike = new PostLike(postLikeDTO.PostId, postLikeDTO.AuthorId);

                var data = await _postLikeRepository.CreateAsync(postLike);

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<PostLikeDTO>(data));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<PostLikeDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<PostLikeDTO>> DeleteAsync(int userId, int postId)
        {
            var postLike = await _postLikeRepository.GetByUserIdAndPostId(userId, postId);
            if (postLike == null)
                return ResultService.Fail<PostLikeDTO>("Erro Não encontrado");

            try
            {
                await _unitOfWork.BeginTransaction();

                var data = await _postLikeRepository.DeleteAsync(postLike);

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<PostLikeDTO>(data));

            }
            catch(Exception ex)
            {
                await  _unitOfWork.Rollback();
                return ResultService.Fail<PostLikeDTO>($"{ex.Message}");
            }
        }

        public async Task DeleteAsyncNotReturn(int postId)
        {
            var postLike = await _postLikeRepository.GetByPostIdAll(postId);
            if (postLike == null)
                Console.WriteLine("Erro não encontrado");

            try
            {
                await _unitOfWork.BeginTransaction();

                foreach (var item in postLike)
                {
                    await _postLikeRepository.DeleteAsync(item);
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
