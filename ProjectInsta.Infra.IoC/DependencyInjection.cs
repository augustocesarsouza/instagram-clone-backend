using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectInsta.Application.Mappings;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Application.Services;
using ProjectInsta.Domain.Repositories;
using ProjectInsta.Infra.Data.Context;
using ProjectInsta.Infra.Data.Repositories;
using ProjectInsta.Domain.Authentication;
using ProjectInsta.Infra.Data.Authentication;

namespace ProjectInsta.Infra.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepostitory, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ICommentRepository, CommentRepository>();
            services.AddScoped<ISubCommentRepository, SubCommentRepository>();
            services.AddScoped<IPostLikeRepository, PostLikeRepository>();
            services.AddScoped<IFollowRepository, FollowRepository>();
            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<ILikeCommentRepository, LikeCommentRepository>();
            services.AddScoped<IUserPermissionRepository, UserPermissionRepository>();
            services.AddScoped<IFriendRequestRepository, FriendRequestRepository>();
            services.AddScoped<IStoryRepository, StoryRepository>();
            services.AddScoped<IStoryVisualizedRepository, StoryVisualizedRepository>();
            services.AddScoped<IPropertyTextRepository, PropertyTextRepository>();
            services.AddScoped<ITokenGenerator, TokenGenerator>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(typeof(DomainToDtoMapping));
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ISubCommentService, SubCommentService>();
            services.AddScoped<IPostLikeService, PostLikeService>();
            services.AddScoped<IFollowService, FollowService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ILikeCommentService, LikeCommentService>();
            services.AddScoped<IStoryService, StoryService>();
            services.AddScoped<IStoryVisualizedService, StoryVisualizedService>();
            services.AddScoped<IFriendRequestService, FriendRequestService>();
            services.AddScoped<IPropertyTextService, PropertyTextService>();
            services.AddScoped<ICreateImgProcess, CreateImgProcessService>();
            services.AddScoped<IUserPermissionService, UserPermissionService>();
            return services;
        }
    }
}
