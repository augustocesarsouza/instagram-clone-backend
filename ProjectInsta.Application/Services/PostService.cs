using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Hosting;
using ProjectInsta.Application.CloudinaryAAA;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.Validations.PostValidator;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Repositories;

namespace ProjectInsta.Application.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepostitory _userRespository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPostLikeService _postLikeService;
        private readonly ICommentService _commentService;

        private readonly Account _account = new Account(
            CloudinaryConfig.AccountName,
            CloudinaryConfig.ApiKey,
            CloudinaryConfig.ApiSecret
            );

        public PostService(IPostRepository postRepository, IUserRepostitory userRepostitory, IMapper mapper, IUnitOfWork unitOfWork, IPostLikeService postLikeService, ICommentService commentService)
        {
            _postRepository = postRepository;
            _userRespository = userRepostitory;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _postLikeService = postLikeService;
            _commentService = commentService;
        }

        public async Task<ResultService<PostDTO>> GetOnlyNameAndImgUserByPostIdToMessage(int postId) 
        {
            var user = await _postRepository.GetOnlyNameAndImgUserByPostIdToMessage(postId);

            if (user == null)
                return ResultService.Fail<PostDTO>("Usuario não Localizado");

            return ResultService.Ok(_mapper.Map<PostDTO>(user));

        }

        public async Task<ResultService<ICollection<PostDTO>>> GetAllPostsAsync()
        {
            var post = await _postRepository.GetAllPostAsync();
            return ResultService.Ok(_mapper.Map<ICollection<PostDTO>>(post));
        }

        public async Task<ResultService<ICollection<PostDTO>>> GetPostByAuthorId(int authorId)
        {
            var user = await _postRepository.GetCheckUserPost(authorId);
            if (user == null)
                return ResultService.Fail<ICollection<PostDTO>>("Usuario não tem postagem criada");

            var posts = await _postRepository.GetPostByAythorIdAsync(authorId);
            return ResultService.Ok(_mapper.Map<ICollection<PostDTO>>(posts));
        }

        public async Task<ResultService<ICollection<PostDTO>>> GetThreeLastPostAsync(int userId)
        {
            var thereePost = await _postRepository.GetThreeLastPostAsync(userId);
            return ResultService.Ok(_mapper.Map<ICollection<PostDTO>>(thereePost));
        }

        public async Task<ResultService<ICollection<PostDTO>>> GetVideosForReels()
        {
            var videosReels = await _postRepository.GetVideosForReels();
            return ResultService.Ok(_mapper.Map<ICollection<PostDTO>>(videosReels));
        }

        public async Task<ResultService<PostDTO>> GetVideoToReelInfo(int reelId)
        {
            var reelInfo = await _postRepository.GetVideoToReelInfo(reelId);

            if (reelInfo == null)
                return ResultService.Fail<PostDTO>("Erro em obter");

            return ResultService.Ok(_mapper.Map<PostDTO>(reelInfo));
        }

        public async Task<ResultService<PostDTO>> CreatePostAsync(PostDTO postDTO) // os tipo da imagens que podem entrar só do tamanho que eu quero
        {
            if (postDTO == null)
                return ResultService.Fail<PostDTO>("Objeto não deve ser null");

            var validator = new PostDTOValidator().Validate(postDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<PostDTO>("Erro de validação verifique as informações necessarias", validator);


            var cloudinary = new Cloudinary(_account);

            if (postDTO.Url.StartsWith("data:image"))
            //if (postDTO.IsImagem == 1) 
            {
                var uploadparams = new ImageUploadParams()
                {
                    File = new FileDescription(postDTO.Url),
                    Transformation = new Transformation().Width(1080).Height(1080).Crop("fill").Quality(100),
                };

                var uploadresult = await cloudinary.UploadAsync(uploadparams);
                string publicid = uploadresult.PublicId;
                var imagemurl = cloudinary.Api.UrlImgUp.BuildUrl(publicid);

                postDTO.PublicId = publicid;
                postDTO.Url = imagemurl;
                postDTO.IsImagem = 1;

                try
                {
                    await _unitOfWork.BeginTransaction();
                    var post = await _postRepository.CreatePostAsync(_mapper.Map<Post>(postDTO));
                    await _unitOfWork.Commit();
                    return ResultService.Ok(_mapper.Map<PostDTO>(post));
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    var destroyParams = new DeletionParams(postDTO.PublicId) { ResourceType = ResourceType.Image };
                    await cloudinary.DestroyAsync(destroyParams);
                    return ResultService.Fail<PostDTO>($"{ex.Message}");
                }
            }
            else
            {
                return ResultService.Fail<PostDTO>("Tipo de conteúdo desconhecido.");
            }
        }

        public async Task<ResultService<PostDTO>> CreatePostVideoAsync(PostDTO postDTO, int positionY) // os tipo da imagens que podem entrar só do tamanho que eu quero
        {
            if (postDTO == null)
                return ResultService.Fail<PostDTO>("Objeto não deve ser null");

            var validator = new PostDTOValidator().Validate(postDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<PostDTO>("Erro de validação verifique as informações necessarias", validator);


            var cloudinary = new Cloudinary(_account);

            if (postDTO.Url.StartsWith("data:video/"))
            {
                var resultCalc = 0; // ver isso depois
                if (positionY < 0)
                {
                    var valuePositiveY = Math.Abs(positionY);
                    resultCalc = valuePositiveY + 442;
                }else if(positionY >= 0)
                {
                    resultCalc = Math.Abs((positionY + positionY) - 442);
                }

                var uploadParams = new VideoUploadParams()
                {
                    File = new FileDescription(postDTO.Url),
                    Transformation = new Transformation()
                    .Width(883)
                    .Height(883)
                    .VideoCodec("auto")
                    .Crop("fill")
                    .Quality(90)
                    .X(0).Y(resultCalc)
                };
                // se for negativo fica potivo tipo -143 pega esse valor e faz 143 + 442 vai dar 584,5 no caso ele vai ir pra baixo porque
                // valor positivo se vier +138, faz assim 138 + 138 = 276 esse valor menos 441,5 = 165,5
                // 0 e mais pra cima, e 883 e o mais pra baixo

                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                string publicId = uploadResult.PublicId;
                var videoUrl = cloudinary.Api.UrlVideoUp.BuildUrl(publicId);

                postDTO.PublicId = publicId;
                postDTO.Url = videoUrl;
                postDTO.IsImagem = 0;

                try
                {
                    await _unitOfWork.BeginTransaction();
                    var post = await _postRepository.CreatePostAsync(_mapper.Map<Post>(postDTO));
                    await _unitOfWork.Commit();
                    return ResultService.Ok(_mapper.Map<PostDTO>(post));
                }
                catch (Exception ex)
                {
                    await _unitOfWork.Rollback();
                    var destroyParams = new DeletionParams(postDTO.PublicId) { ResourceType = ResourceType.Video };
                    await cloudinary.DestroyAsync(destroyParams);
                    return ResultService.Fail<PostDTO>($"{ex.Message}");
                }
            }
            else
            {
                return ResultService.Fail<PostDTO>("Só permitido tipo video");
            }
        }

        public async Task<ResultService<PostDTO>> DeletePostAsync(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);

            if (post == null)
                return ResultService.Fail<PostDTO>("Objeto não pode ser null");


            await _postLikeService.DeleteAsyncNotReturn(post.Id);

            await _commentService.DeleteAsyncNotReturn(post.Id);

            var cloudinary = new Cloudinary(_account);

            try
            {
                await _unitOfWork.BeginTransaction();
                var postDeleted = await _postRepository.DeletePostAsync(post);

                if (postDeleted.PublicId != null)
                {
                    if (post.IsImagem == 1)
                    {
                        var destroyParams = new DeletionParams(postDeleted.PublicId) { ResourceType = ResourceType.Image };
                        await cloudinary.DestroyAsync(destroyParams);
                    }
                    else
                    {
                        var destroyParams = new DeletionParams(postDeleted.PublicId) { ResourceType = ResourceType.Video };
                        await cloudinary.DestroyAsync(destroyParams);
                    }
                }

                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<PostDTO>(postDeleted));

            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<PostDTO>($"{ex.Message}");
            }
        }
    }
}
