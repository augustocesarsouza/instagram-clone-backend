using AutoMapper;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.Validations.SubCommentValidator;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Entities;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Repositories;

namespace ProjectInsta.Application.Services
{
    public class SubCommentService : ISubCommentService
    {
        private readonly ISubCommentRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SubCommentService(ISubCommentRepository repository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultService<ICollection<SubCommentDTO>>> GetByCommentIdAsync(int commentId, int pagina, int registroPorPagina)
        {
            var subsComments = await _repository.GetByCommentIdAsync(commentId, pagina, registroPorPagina);
            return ResultService.Ok(_mapper.Map<ICollection<SubCommentDTO>>(subsComments));
        }

        public async Task<ResultService<SubCommentDTO>> CreateAsync(SubCommentDTO subCommentDTO)
        {
            if (subCommentDTO == null)
                return ResultService.Fail<SubCommentDTO>("Objeto não pode ser null");

            var validateDto = new SubCommentDTOValidator().Validate(subCommentDTO);
            if (!validateDto.IsValid)
                return ResultService.RequestError<SubCommentDTO>("Erro de validação", validateDto);

            subCommentDTO.CreatedAt = DateTime.Now;
            subCommentDTO.UpdatedAt = DateTime.Now;

            try
            {
                await _unitOfWork.BeginTransaction();

                var subComment = await _repository.CreateAsync(_mapper.Map<SubComment>(subCommentDTO));

                await _unitOfWork.Commit();

                var subCommentsRetorn = await _repository.GetByIdCreateAsync(subComment.Id);

                return ResultService.Ok(_mapper.Map<SubCommentDTO>(subCommentsRetorn));
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();

                return ResultService.Fail<SubCommentDTO>($"{ex.Message}");
            }
        }


        public async Task DeleteAsync(int commentId)
        {
            var subComment = await _repository.GetByCommentId(commentId);
            if (subComment == null)
                Console.WriteLine("não Encontrado");

            try
            {
                await _unitOfWork.BeginTransaction();

                foreach (var item in subComment)
                {
                    await _repository.DeleteAsync(item);
                }

                await _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                await _unitOfWork.Rollback();
                Console.WriteLine($"{ex.Message}");
            }
        }
    }
}
