using AutoMapper;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.DTOs.UserDTOsReturn;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.Mappings
{
    public class DomainToDtoMapping : Profile
    {
        public DomainToDtoMapping()
        {
            CreateMap<User, UserDTO>();
            CreateMap<User, UserDTO>()
                .ForMember(x => x.PublicId, opt => opt.Ignore())
                .ForMember(x => x.PasswordHash, opt => opt.Ignore())
                .ForPath(x => x.ImagePerfil, opt => opt.Ignore())
                .ForPath(x => x.Password, opt => opt.Ignore())
                .ConstructUsing((model, context) =>
                {
                    var dto = new UserDTO
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Email = model.Email,
                        ImagePerfil = model.ImagePerfil,
                        BirthDate = model.BirthDate,
                        LastDisconnectedTime = model.LastDisconnectedTime,
                    };

                    return dto;
                });

            CreateMap<User, UserFollowsDTOs>()
                .ForPath(x => x.Follower, opt => opt.Ignore())
                .ForPath(x => x.Following, opt => opt.Ignore())
                .ConstructUsing((model, context) =>
                {
                    var dto = new UserFollowsDTOs
                    {
                        Id = model.Id,
                        Name = model.Name,
                        ImagePerfil = model.ImagePerfil,
                        Email = model.Email,
                        Follower = model.Followers != null ? model.Followers.Select(x => new UserDTO
                        {
                            Id = x.Follower.Id,
                            Name = x.Follower.Name,
                            ImagePerfil = x.Follower.ImagePerfil,
                            Email = x.Follower.Email
                        }).ToList()
                        : null,
                        Following = model.Following != null ? model.Following.Select(x => new UserDTO
                        {
                            Id = x.Following.Id,
                            Name = x.Following.Name,
                            ImagePerfil = x.Following.ImagePerfil,
                            Email = x.Following.Email,
                            LastDisconnectedTime = x.Following.LastDisconnectedTime,

                        }).ToList()
                        : null
                    };

                    return dto;
                });

            CreateMap<User, UserFollowersDTOs>()
                .ForPath(x => x.Follower, opt => opt.Ignore())
                .ForPath(x => x.Following, opt => opt.Ignore())
                .ForMember(x => x.Follower, opt => opt.Ignore())
                .ConstructUsing((model, context) =>
                {
                    var dto = new UserFollowersDTOs
                    {
                        Id = model.Id,
                        Name = model.Name,
                        ImagePerfil = model.ImagePerfil,
                        Follower = null,
                        Following = null,
                    };

                    return dto;
                });

            CreateMap<User, UserFollowingDTOs>()
               .ForPath(x => x.Following, opt => opt.Ignore())
               .ConstructUsing((model, context) =>
               {
                   var dto = new UserFollowingDTOs
                   {
                       Id = model.Id,
                       Name = model.Name,
                       ImagePerfil = model.ImagePerfil,
                       Following = null,
                   };

                   return dto;
               });

            CreateMap<User, UserCreateDetailDTOs>()
                .ConstructUsing((model, context) =>
                {
                    var dto = new UserCreateDetailDTOs
                    {
                        Id = model.Id,
                        Name = model.Name,
                        Email = model.Email,
                        Password = model.Password,
                        ImagePerfil = model.ImagePerfil,
                        BirthDate = model.BirthDate,
                    };

                    return dto;
                });

            CreateMap<User, UserLoginDTO>()
                .ConstructUsing((model, context) =>
                {
                    var dto = new UserLoginDTO
                    {
                        UserId = model.Id,
                        Email = model.Email,
                        Token = model.Token
                    };

                    return dto;
                });

            CreateMap<Post, PostDTO>()
                .ForMember(x => x.User, opt => opt.Ignore())
                .ForMember(x => x.PublicId, opt => opt.Ignore())
                .ForMember(x => x.AuthorId, opt => opt.Ignore())
                .ForMember(x => x.PostLikesCounts, opt => opt.Ignore())
                .ForMember(x => x.IsImagem, opt => opt.Ignore())
                .ForPath(x => x.Comments, opt => opt.Ignore())
                .ForPath(x => x.PostLikes, opt => opt.Ignore())
                .ConstructUsing((model, context) =>
                {
                    var dto = new PostDTO
                    {
                        Id = model.Id,
                        Title = model.Title,
                        Url = model.Url,
                        IsImagem = model.IsImagem,
                        User = model.User != null ? new UserDTO
                        {
                            Id = model.User.Id,
                            Name = model.User.Name,
                            ImagePerfil = model.User.ImagePerfil,
                        } : null,
                        PostLikesCounts = model.PostLikesCounts != null ? model.PostLikesCounts : null,
                        CommentsLikes = model.CommentsLikes != null ? model.CommentsLikes : null,
                        PostLikes = model.PostLikes != null ? model.PostLikes.Select(x => new PostLikeDTO
                        {
                            Id = x.Id,
                            AuthorId = x.AuthorId,
                            PostId = x.PostId,
                        }).ToList() : null,
                        AuthorId = model.AuthorId != null ? model.AuthorId : null,
                        Comments = model.Comments != null ? model.Comments.Select(x => new CommentDTO
                        {
                            Id = x.Id,
                            Text = x.Text,
                            CreatedAt = x.CreatedAt,
                        }).ToList() : null,
                        PublicId = model.PublicId != null ? model.PublicId : null,
                    };

                    return dto;
                });

            CreateMap<Comment, CommentDTO>()
                .ForMember(x => x.UpdatedAt, opt => opt.Ignore())
                .ForMember(x => x.UserId, opt => opt.Ignore())
                .ForMember(x => x.PostId, opt => opt.Ignore())
                .ForPath(x => x.User, opt => opt.Ignore())
                .ConstructUsing((model, context) =>
                {
                    var dto = new CommentDTO
                    {
                        Id = model.Id,
                        Text = model.Text,
                        CreatedAt = model.CreatedAt,
                        PostId = model.PostId,
                        User = model.User != null ? new UserDTO
                        {
                            Id = model.User.Id,
                            Name = model.User.Name,
                            ImagePerfil = model.User.ImagePerfil,
                        } : null,
                        SubCommentsCounts = model.SubCommentsCounts,
                        LikeCommentsCounts = model.LikeCommentsCounts,
                        LikeComments = model.LikeComments.Select(x => new LikeCommentDTO
                        {
                            CommentId = x.CommentId,
                            AuthorId = x.AuthorId,
                        }).ToList(),
                    };
                    return dto;
                });

            CreateMap<SubComment, SubCommentDTO>()
                .ForMember(x => x.UserId, opt => opt.Ignore())
                .ForPath(x => x.User, opt => opt.Ignore())
                .ConstructUsing((model, context) =>
                {

                    var dto = new SubCommentDTO
                    {
                        Id = model.Id,
                        Text = model.Text,
                        CreatedAt = model.CreatedAt,
                        User = model.User != null ? new UserDTO
                        {
                            Id = model.User.Id,
                            Name = model.User.Name,
                            ImagePerfil = model.User.ImagePerfil,
                        } : null
                    };
                    return dto;
                });

            CreateMap<PostLike, PostLikeDTO>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForPath(x => x.Id, opt => opt.Ignore())
                .ConstructUsing((model, context) =>
                {
                    var dto = new PostLikeDTO
                    {
                        Id = model.Id > 0 ? model.Id : null,
                        AuthorId = model.AuthorId,
                        PostId = model.PostId
                    };
                    return dto;
                });

            CreateMap<Follow, FollowDTO>()
                .ConstructUsing((model, context) =>
                {
                    var dto = new FollowDTO
                    {
                        Id = model.Id,
                        FollowerId = model.FollowerId,
                        //Followings = model.Following.Following.Select(x => new UserDTO
                        //{
                        //    Id = x.Follower.Id,
                        //    Name = x.Follower.Name,
                        //    ImagePerfil = x.Follower.ImagePerfil,
                        //}).ToList()
                    };
                    return dto;
                });

            CreateMap<Message, MessageDTO>();

            CreateMap<LikeComment, LikeCommentDTO>();

            CreateMap<UserPermission, UserPermissionDTO>()
                .ConstructUsing((model, context) =>
                {
                    var dto = new UserPermissionDTO
                    {
                        Id = model.Id,
                        UserId = model.UserId,
                        Permission = new PermissionDTO
                        {
                            PermissionName = model.Permission.PermissionName,
                            VisualName = model.Permission.VisualName
                        },
                    };

                    return dto;
                });

            CreateMap<Permission, PermissionDTO>();

            CreateMap<FriendRequest, FriendRequestDTO>()
                .ConstructUsing((model, context) =>
                {
                    var dto = new FriendRequestDTO
                    {
                        Id = model.Id,
                        Status = model.Status,
                    };

                    return dto;
                });

            CreateMap<Story, StoryDTO>()
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ForMember(x => x.AuthorId, opt => opt.Ignore())
                .ConstructUsing((model, context) =>
                {
                    var dto = new StoryDTO
                    {
                        Id = model.Id,
                        Url = model.Url,
                        IsImagem = model.IsImagem,
                        PropertyText = model.PropertyText != null ? new PropertyTextDTO
                        {
                            Id = model.PropertyText.Id,
                            Top = model.PropertyText.Top,
                            Left = model.PropertyText.Left,
                            Text = model.PropertyText.Text,
                            Width = model.PropertyText.Width,
                            Height = model.PropertyText.Height,
                            Background = model.PropertyText.Background,
                            FontFamily = model.PropertyText.FontFamily,
                            StoryId = model.PropertyText.StoryId,

                        } : null
                    };
                    return dto;
                });

            CreateMap<StoryVisualized, StoryVisualizedDTO>()
                .ForMember(x => x.Id, opt => opt.Ignore())
                .ForMember(x => x.CreatedAt, opt => opt.Ignore())
                .ConstructUsing((model, context) =>
                {
                    var dto = new StoryVisualizedDTO
                    {
                        Id = model.Id,
                        UserViewedId = model.UserViewedId,
                        UserCreatedPostId = model.UserCreatedPostId,
                        StoryId = model.StoryId,
                        CreatedAt = model.CreatedAt,

                    };

                    return dto;
                });

            CreateMap<PropertyText, PropertyTextDTO>();
        }
    }
}
