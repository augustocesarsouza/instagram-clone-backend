using AutoMapper;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.Validations.FriendRequestValidator;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;

namespace ProjectInsta.Application.Services
{
    public class FriendRequestService : IFriendRequestService
    {
        private readonly IFriendRequestRepository _friendRequestRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public FriendRequestService(IFriendRequestRepository friendRequestRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _friendRequestRepository = friendRequestRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<FriendRequestDTO>> GetSenderAndRecipientAsync(int? senderId, int? recipientId)
        {
            var friend = await _friendRequestRepository.GetSenderAndRecipientAsync(senderId, recipientId);

            if (friend == null)
                return ResultService.Fail<FriendRequestDTO>("Não encontrado essa consulta");

            return ResultService.Ok(_mapper.Map<FriendRequestDTO>(friend));
        }

        public async Task<ResultService<List<FriendRequestDTO>>> GetCheckRequestsFriendship(int recipientId)
        {
            var friend = await _friendRequestRepository.GetCheckRequestsFriendship(recipientId);

            if (friend == null)
                return ResultService.Fail<List<FriendRequestDTO>>("Não encontrado essa consulta");

            return ResultService.Ok(_mapper.Map<List<FriendRequestDTO>>(friend));
        }

        public async Task<ResultService<FriendRequestDTO>> CreateAsync(FriendRequestDTO friendRequest)
        {
            if (friendRequest == null)
                return ResultService.Fail<FriendRequestDTO>("Objeto não pode ser null");

            var validator = new FriendRequestDTOValidator().Validate(friendRequest);
            if (!validator.IsValid)
                return ResultService.RequestError<FriendRequestDTO>("Erro de validação do objeto", validator);

            friendRequest.CreatedAt = DateTime.Now;
            friendRequest.Status = "Pending";

            try
            {
                await _unitOfWork.BeginTransaction();

                var data = await _friendRequestRepository.CreateAsync(_mapper.Map<FriendRequest>(friendRequest));

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<FriendRequestDTO>(data));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();

                return ResultService.Fail<FriendRequestDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<FriendRequestDTO>> UpdateAsync(FriendRequestDTO friendRequest)
        {
            if (friendRequest == null)
                return ResultService.Fail<FriendRequestDTO>("Objeto não pode ser null");

            var validator = new FriendRequestDTOValidator().Validate(friendRequest);
            if (!validator.IsValid)
                return ResultService.RequestError<FriendRequestDTO>("Erro de validação do objeto", validator);

            var attFriendship = await GetSenderAndRecipientAsync(friendRequest.SenderId, friendRequest.RecipientId);

            attFriendship.Data.Status = friendRequest.Status;

            try
            {
                await _unitOfWork.BeginTransaction();

                var data = await _friendRequestRepository.UpdateAsync(_mapper.Map<FriendRequest>(attFriendship.Data));

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<FriendRequestDTO>(data));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();

                return ResultService.Fail<FriendRequestDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<FriendRequestDTO>> DeleteAsync(int senderId, int recipientId)
        {
            var friendDelete = await GetSenderAndRecipientAsync(senderId, recipientId);

            try
            {
                await _unitOfWork.BeginTransaction();

                var data = await _friendRequestRepository.DeleteAsync(_mapper.Map<FriendRequest>(friendDelete.Data));

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<FriendRequestDTO>(data));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();

                return ResultService.Fail<FriendRequestDTO>($"{ex.Message}");
            }
        }
    }
}
