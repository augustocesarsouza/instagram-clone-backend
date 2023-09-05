using ProjectInsta.Application.DTOs;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface ICreateImgStoryProcess
    {
        public Task<ResultService<ProcessImgDTO>> ProcessImg(ProcessImgDTO processImgDTO);
        public Task<ResultService<ProcessImgDTO>> DeleteImgCloudinary(ProcessImgDTO processImgDTO);
    }
}
