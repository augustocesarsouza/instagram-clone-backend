using CloudinaryDotNet; // FileDescription, Transformation, TextLayer
using CloudinaryDotNet.Actions; // VideoUploadParams, ImageUploadParams
using ProjectInsta.Application.CloudinaryAAA;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;

namespace ProjectInsta.Application.Services
{
    public class CreateImgProcessService : ICreateImgProcess
    {
        private readonly Account _account = new Account(
            CloudinaryConfig.AccountName,
            CloudinaryConfig.ApiKey,
            CloudinaryConfig.ApiSecret
            );

        public static List<ProcessImgDTO> ProcessImgDTOs = new();

        public async Task<ResultService<ProcessImgDTO>> ProcessImgCreateToProfileVideos(ProcessImgDTO processImgDTO) //Apagar depois
        {
            var cloudinary = new Cloudinary(_account);

            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(processImgDTO.Url),
                Transformation = new Transformation().Width(656).Height(656).Crop("fill").Quality(100),
            };

            var uploadResult = await cloudinary.UploadAsync(uploadParams);
            string publicId = uploadResult.PublicId;
            var imagemUrl = cloudinary.Api.UrlImgUp.BuildUrl(publicId);

            processImgDTO.PublicId = publicId;
            processImgDTO.Url = imagemUrl;

            return ResultService.Ok(processImgDTO);
        }

        public async Task<ResultService<ProcessImgDTO>> ProcessImgStory(ProcessImgDTO processImgDTO)
        {
            if (processImgDTO == null)
                return ResultService.Fail<ProcessImgDTO>("Erro nao pode ser nulo");

            var cloudinary = new Cloudinary(_account);

            if (processImgDTO.Url.StartsWith("data:image/") && processImgDTO.IsStory == true)
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(processImgDTO.Url),
                    Transformation = new Transformation().Width(542).Height(871).Crop("fill").Quality(100),
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                string publicId = uploadResult.PublicId;
                var imagemUrl = cloudinary.Api.UrlImgUp.BuildUrl(publicId);

                processImgDTO.PublicId = publicId;
                processImgDTO.Url = imagemUrl;
                processImgDTO.IsImagem = 1;

                //ProcessImgDTOs.Add(processImgDTO);
            }
            else
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(processImgDTO.Url),
                    //Transformation = new Transformation()
                    //.Width(480)
                    //.Height(750)
                    //.Crop("fill").Quality(100),
                    Transformation = new Transformation().Width(1080).Height(1080).Crop("fill").Quality(100),
                };

                var uploadResult = await cloudinary.UploadAsync(uploadParams);
                string publicId = uploadResult.PublicId;
                var imagemUrl = cloudinary.Api.UrlImgUp.BuildUrl(publicId);

                processImgDTO.PublicId = publicId;
                processImgDTO.Url = imagemUrl;
                processImgDTO.IsImagem = 1;
            }

            return ResultService.Ok(processImgDTO);
        }

        public async Task<ResultService<ProcessImgDTO>> DeleteImgCloudinary(ProcessImgDTO processImgDTO)
        {
            if (processImgDTO == null)
                return ResultService.Fail<ProcessImgDTO>("Erro não pode ser nulo");

            var cloudinary = new Cloudinary(_account);

            if (processImgDTO.IsImagem == 1)
            {
                var destroyParams = new DeletionParams(processImgDTO.PublicId) { ResourceType = ResourceType.Image };
                await cloudinary.DestroyAsync(destroyParams);
            }
            else if (processImgDTO.IsImagem == 0)
            {
                var destroyParams = new DeletionParams(processImgDTO.PublicId) { ResourceType = ResourceType.Video };
                await cloudinary.DestroyAsync(destroyParams);
            }
            return ResultService.Ok(processImgDTO);
        }
    }
}
