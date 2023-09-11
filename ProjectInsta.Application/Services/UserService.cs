using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ProjectInsta.Application.CloudinaryAAA;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.UserDTOsReturn;
using ProjectInsta.Application.DTOs.Validations.ImagemBase64ProfileUserValidator;
using ProjectInsta.Application.DTOs.Validations.UserValidator;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Authentication;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using SecureIdentity.Password;
using System.Globalization;

namespace ProjectInsta.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepostitory _userRepostitory;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly IMapper _mapper;
        private readonly Account _account = new Account(
            CloudinaryConfig.AccountName,
            CloudinaryConfig.ApiKey,
            CloudinaryConfig.ApiSecret
            );
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserPermissionService _userPermissionService;

        public UserService(IUserRepostitory userRepostitory, IMapper mapper, ITokenGenerator tokenGenerator, IUnitOfWork unitOfWork, IUserPermissionService userPermissionService)
        {
            _userRepostitory = userRepostitory;
            _mapper = mapper;
            _tokenGenerator = tokenGenerator;
            _unitOfWork = unitOfWork;
            _userPermissionService = userPermissionService;
        }

        public async Task<ResultService<UserDTO>> GetUserDataOnly(int idUser)
        {
            var user = await _userRepostitory.GetUserDataOnly(idUser);
            if (user == null)
                return ResultService.Fail<UserDTO>("Usuario não Localizado");
            return ResultService.Ok(_mapper.Map<UserDTO>(user));
        }

        public async Task<ResultService<List<UserFollowersDTOs>>> GetFollowersUser(int userId)
        {
            var userFollowings = await _userRepostitory.GetFollowersUser(userId);
            if (userFollowings == null)
                return ResultService.Fail<List<UserFollowersDTOs>>("Usuario não encontrado");

            return ResultService.Ok(_mapper.Map<List<UserFollowersDTOs>>(userFollowings));
        }

        public async Task<ResultService<HashSet<UserFollowersDTOs>>> GetSuggestionForYouProfile(int userId, int idUser, bool isProfile)
        {
            var userFollowings = await _userRepostitory.GetSuggestionForYouProfile(userId, idUser, isProfile);
            if (userFollowings == null)
                return ResultService.Fail<HashSet<UserFollowersDTOs>>("Usuario não encontrado");

            return ResultService.Ok(_mapper.Map<HashSet<UserFollowersDTOs>>(userFollowings));
        }

        public async Task<ResultService<List<UserFollowersDTOs>>> GetSuggestionToShareReels(int idUser)
        {
            var listSuggestion = await _userRepostitory.GetSuggestionToShareReels(idUser);

            return ResultService.Ok(_mapper.Map<List<UserFollowersDTOs>>(listSuggestion));
        }

        public async Task<ResultService<List<UserFollowingDTOs>>> GetUsersFollowignByIdAsync(int idUser)
        {
            var user = await _userRepostitory.GetUsersFollowignByIdAsync(idUser);
            if (user == null)
                return ResultService.Fail<List<UserFollowingDTOs>>("Usuario não Localizado");

            return ResultService.Ok(_mapper.Map<List<UserFollowingDTOs>>(user));
        }

        public async Task<ResultService<UserCreateDetailDTOs>> CreateAsync(UserDTO userDTO)
        {
            if (userDTO == null)
                return ResultService.Fail<UserCreateDetailDTOs>("Objeto deve ser inforado");

            var validation = new UserCreateDTOValidator().Validate(userDTO);
            if (!validation.IsValid)
                return ResultService.RequestError<UserCreateDetailDTOs>("Erro de validação verifique as informações", validation);

            var userValidate = await _userRepostitory.GetByEmail(userDTO.Email);
            if (userValidate != null)
                return ResultService.Fail<UserCreateDetailDTOs>("Email já registrado");

            var cloudinary = new Cloudinary(_account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(userDTO.ImagePerfil)
            };

            var uploadResul = await cloudinary.UploadAsync(uploadParams);
            var publicId = uploadResul.PublicId;
            var imageUrl = cloudinary.Api.UrlImgUp.BuildUrl(publicId);

            try
            {
                await _unitOfWork.BeginTransaction();


                var password = PasswordGenerator.Generate(25, false);
                userDTO.ImagePerfil = imageUrl;
                userDTO.PublicId = publicId;
                userDTO.PasswordHash = PasswordHasher.Hash(password);
                userDTO.Password = password;

                DateTime birthDate = DateTime.ParseExact(userDTO.BirthDateString, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                userDTO.BirthDate = birthDate;

                var user = _mapper.Map<User>(userDTO);
                var data = await _userRepostitory.CreateAsync(user);
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<UserCreateDetailDTOs>(data));
            }
            catch (Exception ex)
            {
                await cloudinary.DestroyAsync(new DeletionParams(publicId));
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserCreateDetailDTOs>($"{ex.Message}");
            }
        }

        public async Task<ResultService<UserDTO>> UpdateAsync(UserDTO userDTO)
        {
            if (userDTO == null)
                return ResultService.Fail<UserDTO>("Objeto nao pode ser null");

            var validator = new UserUpdateDTOValidator().Validate(userDTO);
            if (!validator.IsValid)
                return ResultService.RequestError<UserDTO>("Erro de validação", validator);

            var user = await _userRepostitory.GetByEmail(userDTO.Email);

            if (user == null)
                return ResultService.Fail<UserDTO>("Usuario não encontrado");

            var cloudinary = new Cloudinary(_account);

            var destroyParams = new DeletionParams(user.PublicId);
            await cloudinary.DestroyAsync(destroyParams);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(userDTO.ImagePerfil)
            };

            var uploadResul = await cloudinary.UploadAsync(uploadParams);
            var publicId = uploadResul.PublicId;
            var imageUrl = cloudinary.Api.UrlImgUp.BuildUrl(publicId);
            user.ValidateNewImg(imageUrl, publicId);

            try
            {
                await _unitOfWork.BeginTransaction();
                var userUpdate = await _userRepostitory.UpdateAsync(user);
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<UserDTO>(userUpdate));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserDTO>($"{ex.Message}");
            }

        }

        public async Task<ResultService<UserDTO>> UpdateImgPerfilUser(string email, ImagemBase64ProfileUserDTO imagemBase64ProfileUserDTO)
        {
            var userCheck = await _userRepostitory.GetByEmailCheckUser(email);
            if (userCheck == null)
                return ResultService.Fail<UserDTO>("User não existe");

            var validatorBase64 = new ImagemBase64ProfileUserDTOValidator().Validate(imagemBase64ProfileUserDTO);
            if (!validatorBase64.IsValid)
                return ResultService.RequestError<UserDTO>("Erro em validar seu DTO", validatorBase64);

            if (!imagemBase64ProfileUserDTO.Base64.StartsWith("data:image"))
                return ResultService.Fail<UserDTO>("Erro não é uma Imagem");

            var publicIdFromClodunaryServer = userCheck.PublicId;

            var cloudinary = new Cloudinary(_account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(imagemBase64ProfileUserDTO.Base64),
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            var publicId = uploadResult.PublicId;
            var imgUrl = cloudinary.Api.UrlImgUp.BuildUrl(publicId);

            var validatorCheck = userCheck.ValidatorCloudinary(publicId, imgUrl);
            if (!validatorCheck)
                return ResultService.Fail<UserDTO>("Erro ao criar publicId ou ImgUrl");

            await cloudinary.DestroyAsync(new DeletionParams(publicIdFromClodunaryServer) { ResourceType = ResourceType.Image });

            try
            {
                await _unitOfWork.BeginTransaction();
                var data = await _userRepostitory.UpdateAsync(userCheck);
                await _unitOfWork.Commit();
                return ResultService.Ok(_mapper.Map<UserDTO>(data));

            }
            catch (Exception ex)
            {
                await cloudinary.DestroyAsync(new DeletionParams(publicId) { ResourceType = ResourceType.Image });
                await _unitOfWork.Rollback();
                return ResultService.Fail<UserDTO>($"{ex.Message}");
            }
        }

        public async Task<ResultService<UserLoginDTO>> Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                return ResultService.Fail<UserLoginDTO>("Email ou Senha não foi informado Verifique");

            var user = await _userRepostitory.GetByEmail(email);
            if (user == null)
                return ResultService.Fail<UserLoginDTO>("Usuario Não Encontrado");

            if (!PasswordHasher.Verify(user.PasswordHash, password))
                return ResultService.Fail<UserLoginDTO>("Usuário ou Senha Invalidos");

            var permission = await _userPermissionService.GetAllPermissionUser(user.Id);
            var userPermissions = _mapper.Map<ICollection<UserPermission>>(permission.Data);

            var token = _tokenGenerator.Generator(user, userPermissions);
            try
            {
                user.ValidatorToken(token.Acess_token);
                return ResultService.Ok(_mapper.Map<UserLoginDTO>(user));

            }
            catch (Exception ex)
            {
                return ResultService.Fail<UserLoginDTO>(ex.Message);
            }
        }

        public async Task<ResultService<UserDTO>> UpdateLastDisconnectedTimeUserForMessageHub(string email)
        {
            if (string.IsNullOrEmpty(email))
                return ResultService.Fail<UserDTO>("erro email não enviado");

            var user = await _userRepostitory.GetByEmailSignalR(email);
            if (user == null)
                return ResultService.Fail<UserDTO>("Usuario Não Encontrado");

            user.ValidateLastDisconnectedTime(DateTime.UtcNow);

            await _userRepostitory.UpdateAsync(user);

            var userDto = _mapper.Map<UserDTO>(user);

            return ResultService.Ok(userDto);
        }    }
}
