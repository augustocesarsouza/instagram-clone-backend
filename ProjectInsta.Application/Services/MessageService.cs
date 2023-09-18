using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ProjectInsta.Application.CloudinaryAAA;
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

        private readonly Account _account = new Account(
           CloudinaryConfig.AccountName,
           CloudinaryConfig.ApiKey,
           CloudinaryConfig.ApiSecret
           );

        public MessageService(IMessageRepository messageRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _messageRepository = messageRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
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

            messageDTO.AlreadySeeThisMessage = 0;

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

        public async Task<ResultService<MessageDTO>> UpdateAsync(int messageId)
        {
            if (messageId <= 0)
                return ResultService.Fail<MessageDTO>("Id do commentario deve ser maior que 0");

            var message = await _messageRepository.GetById(messageId);
            if (message == null)
                return ResultService.Fail<MessageDTO>("Não encontrado Message");

            message.AlreadySeeThisMessageMethodo();

            try
            {
                await _unitOfWork.BeginTransaction();
                var data = await _messageRepository.UpdateAsync(_mapper.Map<Message>(message));
                await _unitOfWork.Rollback();
                return ResultService.Ok(_mapper.Map<MessageDTO>(data));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<MessageDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<MessageDTO>> DeleteAsync(int idMessage)
        {
            if (idMessage <= 0)
                return ResultService.Fail<MessageDTO>("Id deve ser maior que zero");

            var messageDelete = await _messageRepository.GetById(idMessage);

            if (messageDelete == null)
                return ResultService.Fail<MessageDTO>("Não existe essa message");

            try
            {
                await _unitOfWork.BeginTransaction();

                var data = await _messageRepository.DeleteAsync(_mapper.Map<Message>(messageDelete));
                await _unitOfWork.Commit();

                if (data.UrlFrameReel != null)
                {
                    var cloudinary = new Cloudinary(_account);

                    //var destroyParams = new DeletionParams(data.PublicIdFrameReel) { ResourceType = ResourceType.Image };
                    //await cloudinary.DestroyAsync(destroyParams);
                }

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
