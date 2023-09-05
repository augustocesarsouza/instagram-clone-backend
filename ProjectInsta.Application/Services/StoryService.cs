using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ProjectInsta.Application.CloudinaryAAA;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.Validations.StoryValidator;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;

namespace ProjectInsta.Application.Services
{
    public class StoryService : IStoryService
    {
        private readonly IStoryRepository _storyRepository;
        private readonly IStoryVisualizedService _storyVisualizedService;
        private readonly IPropertyTextService _propertyTextService;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        private readonly Account _account = new Account(
            CloudinaryConfig.AccountName,
            CloudinaryConfig.ApiKey,
            CloudinaryConfig.ApiSecret
            );

        public StoryService(IStoryRepository storyRepository, IMapper mapper, IUnitOfWork unitOfWork, IStoryVisualizedService storyVisualizedService, IPropertyTextService propertyTextService)
        {
            _storyRepository = storyRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _storyVisualizedService = storyVisualizedService;
            _propertyTextService = propertyTextService;
        }

        public async Task<ResultService<ICollection<StoryDTO>>> GetAllStory()
        {
            var storys = await _storyRepository.GetAllStory();

            return ResultService.Ok(_mapper.Map<ICollection<StoryDTO>>(storys));
        }

        public async Task<ResultService<List<StoryDTO>>> GetByUserIdAsync(int userCreatePost)
        {
            var storys = await _storyRepository.GetByUserIdAsync(userCreatePost);

            foreach (var item in storys)
            {
                if (item.CreatedAt.Day < DateTime.Now.Day && item.CreatedAt.TimeOfDay < DateTime.Now.TimeOfDay)
                {
                    var storysVisu = await _storyVisualizedService.GetByStoryIdVisualized(item.Id);
                    foreach (var sto in storysVisu.Data)
                    {
                        await _storyVisualizedService.DeleteAsync(sto);
                    }
                    await DeleteAsync(item.Id);
                    storys.Remove(item);
                }
            }

            return ResultService.Ok(_mapper.Map<List<StoryDTO>>(storys));
        }

        public async Task<ResultService<StoryDTO>> UpdateUserVisualizedStory(int storyId, int idUserView)
        {
            if (storyId <= 0 || idUserView <= 0)
                return ResultService.Fail<StoryDTO>("verifique os id passados nao pode ser menor que 1");

            var story = await _storyRepository.GetIdStoryToUpdate(storyId);

            if (story == null)
                return ResultService.Fail<StoryDTO>("Não foi possivel encontrar esse story");

            //story.AddViewStoryList(idUserView);

            return ResultService.Ok(_mapper.Map<StoryDTO>(story));
        }

        public async Task<ResultService<StoryDTO>> CreateAsync(StoryDTO story)
        {
            if (story == null)
                return ResultService.Fail<StoryDTO>("não pode ser nulo");

            var validation = new StoryDTOValidator().Validate(story);
            if (!validation.IsValid)
                return ResultService.RequestError<StoryDTO>("Erro de validação dto verifica", validation);

            var cloudinary = new Cloudinary(_account);

            story.CreatedAt = DateTime.Now;

            if (story.IsImagem == 1)
            {
                var destroyParams = new DeletionParams(story.PublicId) { ResourceType = ResourceType.Image };
                await cloudinary.DestroyAsync(destroyParams);

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(story.Url),
                    Transformation = new Transformation().Width(480).Height(750).Crop("fill") // vr isso aqui amanha
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                string publicId = uploadResult.PublicId;
                var imageUrl = cloudinary.Api.UrlImgUp.BuildUrl(publicId);

                story.PublicId = publicId;
                story.Url = imageUrl;
                story.IsImagem = 1;

                try
                {
                    await _unitOfWork.BeginTransaction();
                    var data = await _storyRepository.CreateAsync(_mapper.Map<Story>(story));
                    await _unitOfWork.Commit();
                    return ResultService.Ok(_mapper.Map<StoryDTO>(data));
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return ResultService.Fail<StoryDTO>($"{ex.Message}");
                }
            }
            else if (story.Url.StartsWith("data:video/"))
            {
                var uploadParams = new VideoUploadParams()
                {
                    File = new FileDescription(story.Url),
                    Transformation = new Transformation().VideoCodec("auto").Quality(70).Crop("fill"),
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                string publicId = uploadResult.PublicId;
                var imageUrl = cloudinary.Api.UrlVideoUp.BuildUrl(publicId);

                story.PublicId = publicId;
                story.Url = imageUrl;
                story.IsImagem = 0;

                try
                {
                    await _unitOfWork.BeginTransaction();
                    var data = await _storyRepository.CreateAsync(_mapper.Map<Story>(story));
                    await _unitOfWork.Commit();
                    return ResultService.Ok(_mapper.Map<StoryDTO>(data));
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    return ResultService.Fail<StoryDTO>($"{ex.Message}");
                }
            }
            else
            {
                return ResultService.Fail<StoryDTO>("Tipo de conteúdo desconhecido");
            }

        }

        public async Task<ResultService<StoryDTO>> UpdateAsync(StoryDTO story)
        {
            if (story == null)
                return ResultService.Fail<StoryDTO>("não pode ser nulo");

            //var validation = new StoryDTOValidator().Validate(story);
            //if (!validation.IsValid)
            //    return ResultService.RequestError<StoryDTO>("Erro de validação dto verifica", validation);

            try
            {
                await _unitOfWork.BeginTransaction();
                var data = await _storyRepository.UpdateAsync(_mapper.Map<Story>(story));
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<StoryDTO>(data));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<StoryDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<StoryDTO>> DeleteAsync(int id)
        {
            var story = await _storyRepository.GetByIdAsync(id);

            if (story == null)
                return ResultService.Fail<StoryDTO>("Objeto não encontrado");

            var storysVisu = await _storyVisualizedService.GetByStoryIdVisualized(story.Id);

            foreach (var item in storysVisu.Data)
            {
                await _storyVisualizedService.DeleteAsync(item);
            }

            var cloudinary = new Cloudinary(_account);

            await _propertyTextService.DeleteAsync(story.Id);

            try
            {
                await _unitOfWork.BeginTransaction();
                var data = await _storyRepository.DeleteAsync(story);
                if (data.IsImagem == 1)
                {
                    var destroyParams = new DeletionParams(data.PublicId) { ResourceType = ResourceType.Image };
                    await cloudinary.DestroyAsync(destroyParams);
                    await _unitOfWork.Commit();
                    return ResultService.Ok(_mapper.Map<StoryDTO>(data));
                }
                else
                {
                    var destroyParams = new DeletionParams(data.PublicId) { ResourceType = ResourceType.Video };
                    await cloudinary.DestroyAsync(destroyParams);
                    await _unitOfWork.Commit();
                    return ResultService.Ok(_mapper.Map<StoryDTO>(data));
                }
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<StoryDTO>($"{ex.Message}");
            }
        }
    }
}
