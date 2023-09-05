using AutoMapper;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.Validations.PropertyText;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;

namespace ProjectInsta.Application.Services
{
    public class PropertyTextService : IPropertyTextService
    {
        private readonly IPropertyTextRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public PropertyTextService(IPropertyTextRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<PropertyTextDTO>> GetByStoryId(int storyId)
        {
            var property = await _repository.GetByStoryId(storyId);
            if (property == null)
                return ResultService.Fail<PropertyTextDTO>("Erro não encontrado!");

            return ResultService.Ok(_mapper.Map<PropertyTextDTO>(property));
        }

        public async Task<ResultService<PropertyTextDTO>> CreateAsync(PropertyTextDTO propertyTextDTO, int storyId)
        {
            if(propertyTextDTO == null)
                return ResultService.Fail<PropertyTextDTO>("Erro objeto null!");

            propertyTextDTO.StoryId = storyId;

            var validate = new PropertyTextDTOValidator().Validate(propertyTextDTO);
            if (!validate.IsValid)
                return ResultService.RequestError<PropertyTextDTO>("error em validar seu objeto verifique", validate);

            try
            {
                await _unitOfWork.BeginTransaction();
                var data = await _repository.CreateAsync(_mapper.Map<PropertyText>(propertyTextDTO));

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<PropertyTextDTO>(data));

            }catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<PropertyTextDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<PropertyTextDTO>> DeleteAsync(int storyId)
        {
            var storyDelete = await _repository.GetByStoryId(storyId);
            if (storyDelete == null)
                return ResultService.Fail<PropertyTextDTO>("Erro não encontrado");

            try
            {
                await _unitOfWork.BeginTransaction();
                var data = await _repository.DeleteAsync(storyDelete);
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<PropertyTextDTO>(data));


            }catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<PropertyTextDTO>($"{ex.Message}");
            }
        }
    }
}
