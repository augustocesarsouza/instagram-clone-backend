using AutoMapper;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.Validations.StoryVisualizedValidator;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;

namespace ProjectInsta.Application.Services.Interfaces
{
    public class StoryVisualizedService : IStoryVisualizedService
    {
        private readonly IStoryVisualizedRepository _storyVisualizedRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StoryVisualizedService(IStoryVisualizedRepository storyVisualizedRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _storyVisualizedRepository = storyVisualizedRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<StoryVisualizedDTO>> GetById(int id)
        {
            var story = await _storyVisualizedRepository.GetById(id);
            return ResultService.Ok(_mapper.Map<StoryVisualizedDTO>(story));
        }

        public async Task<ResultService<StoryVisualizedDTO>> GetByUserIdAndStoryId(int? userViewed, int? userCreatePost)
        {
            var story = await _storyVisualizedRepository.GetByUserIdAndStoryId(userViewed, userCreatePost);
            return ResultService.Ok(_mapper.Map<StoryVisualizedDTO>(story));
        }

        public async Task<ResultService<ICollection<StoryVisualizedDTO>>> GetByStoryIdVisualized(int? storyId)
        {
            var story = await _storyVisualizedRepository.GetByStoryIdVisualized(storyId);
            return ResultService.Ok(_mapper.Map<ICollection<StoryVisualizedDTO>>(story));
        }

        public async Task<ResultService<StoryVisualizedDTO>> CreateAsync(StoryVisualizedDTO storyVisualized)
        {
            if (storyVisualized == null)
                return ResultService.Fail<StoryVisualizedDTO>("Objeto null");

            var validator = new StoryVisualizedDTOValidator().Validate(storyVisualized);
            if (!validator.IsValid)
                return ResultService.RequestError<StoryVisualizedDTO>("Erro validar o objeto verifique", validator);

            var checkAlreadyExists = await _storyVisualizedRepository.GetCheckAlreadyExists(storyVisualized.UserViewedId, storyVisualized.StoryId);

            if (checkAlreadyExists != null)
                return ResultService.Fail<StoryVisualizedDTO>("Erro já existe");

            storyVisualized.CreatedAt = DateTime.Now;

            try
            {
                await _unitOfWork.BeginTransaction();
                var data = await _storyVisualizedRepository.CreateAsync(_mapper.Map<StoryVisualized>(storyVisualized));
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<StoryVisualizedDTO>(data));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<StoryVisualizedDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<StoryVisualizedDTO>> UpdateAsync(StoryVisualizedDTO storyVisualized)
        {
            if (storyVisualized == null)
                return ResultService.Fail<StoryVisualizedDTO>("Objeto null");

            var validator = new StoryVisualizedDTOValidator().Validate(storyVisualized); // depois ver isso aqui porque validação aqui para criação da entidade
            if (!validator.IsValid)
                return ResultService.RequestError<StoryVisualizedDTO>("Erro validar o objeto verifique", validator);

            try
            {
                await _unitOfWork.BeginTransaction();
                var data = _storyVisualizedRepository.UpdateAsync(_mapper.Map<StoryVisualized>(storyVisualized));
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<StoryVisualizedDTO>(data));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<StoryVisualizedDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<StoryVisualizedDTO>> DeleteAsync(StoryVisualizedDTO storyVisualized)
        {
            if (storyVisualized == null)
                return ResultService.Fail<StoryVisualizedDTO>("Objeto null");

            //var validator = new StoryVisualizedDTOValidator().Validate(storyVisualized);
            //if (!validator.IsValid)
            //    return ResultService.RequestError<StoryVisualizedDTO>("Erro validar o objeto verifique", validator);

            //var storyVisu = await _storyVisualizedRepository.GetCheckAlreadyExists(storyVisualized.UserViewedId, storyVisualized.StoryId);

            try
            {
                var existingEntity = await _storyVisualizedRepository.GetById(storyVisualized.Id);
                if (existingEntity == null)
                {
                    return ResultService.Fail<StoryVisualizedDTO>("Entidade não encontrada no banco de dados.");
                }

                await _unitOfWork.BeginTransaction();
                var data = await _storyVisualizedRepository.DeleteAsync(_mapper.Map<StoryVisualized>(existingEntity));
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<StoryVisualizedDTO>(data));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<StoryVisualizedDTO>($"{ex.Message}");
            }
        }
    }
}
