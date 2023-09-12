using AutoMapper;
using FluentValidation;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.Validations.MessageReelValidator;
using ProjectInsta.Application.DTOs.Validations.MessageValidator;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;

namespace ProjectInsta.Application.Services
{
    public class MessageReelService : IMessageReelService
    {
        private readonly IMessageReelRepository _messageReelRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MessageReelService(IMessageReelRepository messageReelRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _messageReelRepository = messageReelRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<MessageReelDTO>> CreateAsync(MessageReelDTO messageReelDTO)
        {
            if (messageReelDTO == null)
                return ResultService.Fail<MessageReelDTO>("Objeto null");

            var validate = new MessageReelDTOValidator().Validate(messageReelDTO);
            if (!validate.IsValid)
                return ResultService.RequestError<MessageReelDTO>("erro verifique seu objeto fornecido", validate);

            try
            {
                await _unitOfWork.BeginTransaction();
                var data = await _messageReelRepository.CreateAsync(_mapper.Map<MessageReel>(messageReelDTO));
                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<MessageReelDTO>(data));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<MessageReelDTO>($"{ex.Message}");
            }
        }
    }
}
