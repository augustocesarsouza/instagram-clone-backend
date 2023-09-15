using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using ProjectInsta.Application.DTOs;
using ProjectInsta.Application.Services.Interfaces;
using ProjectInsta.Domain.Entities;

namespace ProjectInsta.Application.MyHubs
{
    public class MessageHub : Hub
    {
        private readonly IMessageService _messageService;
        private readonly static ConnectionMapping _connectionMapping = new();
        private readonly IMapper _mapper;
        private readonly IPostService _postService;
        private readonly IUserService _userService;
        private readonly ICommentService _commentService;
        private readonly ISubCommentService _subCommentService;
        private readonly ICreateImgProcess _createImgStoryProcess;

        public MessageHub(
            IMessageService messageService,
            IMapper mapper,
            IUserService userService,
            ICommentService commentService,
            ISubCommentService subCommentService,
            IPostService postService,
            ICreateImgProcess createImgProcess)
        {
            _messageService = messageService;
            _mapper = mapper;
            _userService = userService;
            _commentService = commentService;
            _subCommentService = subCommentService;
            _postService = postService;
            _createImgStoryProcess = createImgProcess;
        }

        public async Task OnConnectedAsync(string myEmail, string[] emailUsers)
        {
            _connectionMapping.Add(myEmail, Context.ConnectionId);

            foreach (var email in emailUsers)
            {
                var senderConnection = _connectionMapping.GetConnection(email);
                await Clients.Client(senderConnection).SendAsync("UserConnection", myEmail);
            }
        }

        public async Task Verficar(string[] emailUsers)
        {
            var listUser = new List<UserIsOnline>();

            foreach (var email in emailUsers)
            {
                var senderConnection = _connectionMapping.GetConnection(email);

                listUser.Add(new UserIsOnline
                {
                    Email = email,
                    IsOnline = string.IsNullOrEmpty(senderConnection) ? false : true,
                });
            }
            await Clients.Client(Context.ConnectionId).SendAsync("IsOnline", listUser);
        }

        public async Task OnDisconnectedAsync(string myEmail, string[] emailUsers)
        {
            var user = await _userService.UpdateLastDisconnectedTimeUserForMessageHub(myEmail);

            foreach (var email in emailUsers)
            {
                var senderConnection = _connectionMapping.GetConnection(email);
                await Clients.Client(senderConnection).SendAsync("UserDisconnected", myEmail, user.Data.LastDisconnectedTime);
            }

            _connectionMapping.Remove(myEmail);
        }

        public async Task SendMessage(SignalRMessageDTO signalRMessageDTO)
        {
            var message = new Message(signalRMessageDTO.Content, signalRMessageDTO.SenderId, signalRMessageDTO.RecipientId);

            var data = await _messageService.CreateAsync(_mapper.Map<MessageDTO>(message));

            var recipientConnection = _connectionMapping.GetConnection(signalRMessageDTO.RecipientEmail);

            await Clients.Client(recipientConnection).SendAsync("ReceiveMessage", data.Data, signalRMessageDTO.RecipientEmail);
        }

        public async Task SendMessageReels(SignalRMessageDTO signalRMessageDTO)
        {
            
            var recipientConnection = _connectionMapping.GetConnection(signalRMessageDTO.RecipientEmail);

            await Clients.Client(recipientConnection).SendAsync("ReceiveReels", signalRMessageDTO, signalRMessageDTO.RecipientEmail);

        }

        public async Task SendComment(CommentDTO commentDTO)
        {
            var commentCreate = await _commentService.CreateCommentAsync(commentDTO);

            await Clients.All.SendAsync("ReceiveComment", commentCreate.Data);
        }

        public async Task SendSubComment(SubCommentDTO subCommentDTO)
        {
            var commentCreate = await _subCommentService.CreateAsync(subCommentDTO);

            await Clients.All.SendAsync("ReceiveSubComments", commentCreate.Data);
        }



        public async Task TypingOrnNot(string userEmail, bool isTyping)
        {
            var senderConnection = _connectionMapping.GetConnection(userEmail);

            await Clients.Client(senderConnection).SendAsync("TypeOrnNot", isTyping);
        }
    }
}
