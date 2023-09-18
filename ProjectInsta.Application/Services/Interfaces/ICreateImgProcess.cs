using ProjectInsta.Application.DTOs;

namespace ProjectInsta.Application.Services.Interfaces
{
    public interface ICreateImgProcess
    {
        public Task<ResultService<ProcessImgDTO>> ProcessImgCreateToProfileVideos(ProcessImgDTO processImgDTO);
        public Task<ResultService<ProcessImgDTO>> ProcessImgStory(ProcessImgDTO processImgDTO);
        public Task<ResultService<ProcessImgDTO>> DeleteImgCloudinary(ProcessImgDTO processImgDTO);
    }
}
