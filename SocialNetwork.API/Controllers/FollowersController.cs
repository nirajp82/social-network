using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dto;
using System;
using System.Threading;
using SocialNetwork.WebUtil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SocialNetwork.Infrastructure;
using SocialNetwork.Nucleus.Engine.Photo;
using SocialNetwork.Nucleus.Followers;

namespace SocialNetwork.API.Controllers
{
    [Route("api/profiles")]
    public class FollowersController : BaseController
    {
        #region Command Action Methods
        [HttpPost("{followingUserId:guid}/follow")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Follow(Guid followingUserId, CancellationToken cancellationToken)
        {
            await Mediator.Send(new Follow.Command { FollowingUserId = followingUserId }, cancellationToken);
            return NoContent();
        }

        [HttpPost("{followingUserId:guid}/unfollow")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Unfollow(Guid followingUserId, CancellationToken cancellationToken)
        {
            await Mediator.Send(new Unfollow.Command { FollowingUserId = followingUserId }, cancellationToken);
            return NoContent();
        }
        #endregion
    }
}