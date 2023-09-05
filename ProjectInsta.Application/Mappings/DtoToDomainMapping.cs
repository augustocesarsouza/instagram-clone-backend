using AutoMapper;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.UserDTOsReturn;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.Mappings
{
    public class DtoToDomainMapping : Profile
    {
        public DtoToDomainMapping()
        {
            CreateMap<UserDTO, User>();
            CreateMap<PostDTO, Post>();
            CreateMap<UserDTO, UserDetailDTO>();
            CreateMap<CommentDTO, Comment>();
            CreateMap<SubCommentDTO, SubComment>();
            CreateMap<PostLikeDTO, PostLike>();
            CreateMap<FollowDTO, Follow>();
            CreateMap<MessageDTO, Message>();
            CreateMap<LikeCommentDTO, LikeComment>();
            CreateMap<FriendRequestDTO, FriendRequest>();
            CreateMap<StoryDTO, Story>();
            CreateMap<StoryVisualizedDTO, StoryVisualized>();
            CreateMap<StoryVisualizedDTO, StoryVisualized>();
            CreateMap<PropertyTextDTO, PropertyText>();

            CreateMap<UserPermissionDTO, UserPermission>()
                .ConstructUsing((model, context) =>
                {
                    var dto = new UserPermission(model.Id, model.UserId, new Permission(model.Permission.VisualName, model.Permission.PermissionName));
                    return dto;
                });

            CreateMap<PermissionDTO, Permission>();
        }
    }
}
