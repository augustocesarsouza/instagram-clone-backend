using AutoMapper;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.Validations.MessageValidator;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;

namespace ProjectInsta.Application.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public MessageService(IMessageRepository messageRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<ICollection<MessageDTO>>> GetAllMessageSenderUserForRecipientUserAsync(int senderUserId, int recipientUserId)
        {
            var message = await _messageRepository.GetAllMessageSenderUserForRecipientUserAsync(senderUserId, recipientUserId);
            if (message.Count <= 0)
                return ResultService.Fail<ICollection<MessageDTO>>("Não tem mensagens entre eses usuarios");

            return ResultService.Ok(_mapper.Map<ICollection<MessageDTO>>(message));
        }

        public async Task<ResultService<ICollection<MessageDTO>>> GetAllMessageSenderUserForRecipientUserAsyncPagaginada(int senderUserId, int recipientUserId, int pagina, int registroPorPagina)
        {
            var message = await _messageRepository.GetAllMessageSenderUserForRecipientUserAsyncPagaginada(senderUserId, recipientUserId, pagina, registroPorPagina);
            if (message.Count <= 0)
                return ResultService.Fail<ICollection<MessageDTO>>("Não tem mensagens entre eses usuarios");

            return ResultService.Ok(_mapper.Map<ICollection<MessageDTO>>(message));
        }

        public async Task<ResultService<MessageDTO>> CreateAsync(MessageDTO messageDTO)
        {
            if (messageDTO == null)
                return ResultService.Fail<MessageDTO>("Não pode ser null");

            var validator = new MessageDTOValidator().Validate(messageDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<MessageDTO>("Erro de validação", validator);

            try
            {
                await _unitOfWork.BeginTransaction();

                var data = await _messageRepository.CreateAsync(_mapper.Map<Message>(messageDTO));
                await _unitOfWork.Commit();

                return ResultService.Ok(_mapper.Map<MessageDTO>(data));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<MessageDTO>($"{ex.Message}");
            }
        }
    }
}
