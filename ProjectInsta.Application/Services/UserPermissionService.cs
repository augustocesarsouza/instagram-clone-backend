using AutoMapper;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Repositories;

namespace ProjectInsta.Application.Services
{
    public class UserPermissionService : IUserPermissionService
    {
        private readonly IUserPermissionRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UserPermissionService(IUserPermissionRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<ICollection<UserPermissionDTO>>> GetAllPermissionUser(int idUser)
        {
            var permissions = await _repository.GetAllPermissionUser(idUser);

            return ResultService.Ok(_mapper.Map<ICollection<UserPermissionDTO>>(permissions));
        }
    }
}



