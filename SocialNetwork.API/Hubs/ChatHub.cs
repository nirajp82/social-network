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
    public class ChatHub : Hub
    {
        #region Members
        private readonly IMediator _mediator;
        #endregion


        #region Constructor
        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }
        #endregion


        #region Methods
        public async Task SendComment(Create.Command command)
        {
            command.UserName = Context.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            var comment = await _mediator.Send(command);
            await Clients.All.SendAsync("ReceiveComment",comment);
        }
        #endregion
    }
}
