using MediatR;
using Microsoft.AspNetCore.SignalR;
using SocialNetwork.Nucleus.Engine.Comment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetwork.API
{
    public class ActivityChatHub : Hub
    {
        #region Members
        private readonly IMediator _mediator;
        private const string _groupNotification = "GroupNotification";
        private const string _receiveComment = "ReceiveComment";
        #endregion


        #region Constructor
        public ActivityChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion


        #region Methods
        public async Task SendComment(Create.Command command)
        {
            command.UserName = GetUserName();
            var comment = await _mediator.Send(command);
            await Clients.Group(command.ActivityId.ToString()).SendAsync(_receiveComment, comment);
        }

        public async Task AddToGroup(string groupName) 
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            string userName = GetUserName();
            await Clients.Group(groupName).SendAsync(_groupNotification, $"{userName} has joined the group");
        }

        public async Task RemoveFromGroup(string groupName) 
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
            string userName = GetUserName();
            await Clients.Group(groupName).SendAsync(_groupNotification, $"{userName} has left the group");
        }
        #endregion

        #region Private Methods
        private string GetUserName()
        {
            return Context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
        }
        #endregion
    }
}
