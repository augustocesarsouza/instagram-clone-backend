using AutoMapper;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.Validations.FollowValidator;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;

namespace ProjectInsta.Application.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FollowService(IFollowRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<ICollection<FollowDTO>>> GetAllFollowersFromUser(int userId)
        {
            var allFollowing = await _repository.GetAllFollowersFromUser(userId);

            return ResultService.Ok(_mapper.Map<ICollection<FollowDTO>>(allFollowing));
        }

        public async Task<ResultService<ICollection<FollowDTO>>> GetAllFollowingFromUser(int idUser)
        {
            var allFollowing = await _repository.GetAllFollowingFromUser(idUser);
            if (allFollowing.Count <= 0)
                return ResultService.Fail<ICollection<FollowDTO>>("Não encontrado resgistros");

            return ResultService.Ok(_mapper.Map<ICollection<FollowDTO>>(allFollowing));
        }

        public async Task<ResultService<FollowDTO>> CreateAsync(FollowDTO followDTO)
        {
            if (followDTO == null)
                return ResultService.Fail<FollowDTO>("Objeto não pode ser null");

            var validator = new FollowDTOValidator().Validate(followDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<FollowDTO>("Error de validação", validator);

            try
            {
                await _unitOfWork.BeginTransaction();

                var data = await _repository.CreateAsync(_mapper.Map<Follow>(followDTO));

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<FollowDTO>(data));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();

                return ResultService.Fail<FollowDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<FollowDTO>> DeleteAsync(FollowDTO followDTO)
        {
            if (followDTO == null)
                return ResultService.Fail<FollowDTO>("Objeto não pode ser null");

            var validator = new FollowDTOValidator().Validate(followDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<FollowDTO>("Error de validação", validator);

            var followDelete = await _repository.GetByFollowerAndFollowingAsync(followDTO.FollowerId, followDTO.FollowingId);

            try
            {
                await _unitOfWork.BeginTransaction();

                var data = await _repository.DeleteAsync(followDelete);

                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<FollowDTO>(data));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();

                return ResultService.Fail<FollowDTO>($"{ex.Message}");
            }
        }
    }
}
