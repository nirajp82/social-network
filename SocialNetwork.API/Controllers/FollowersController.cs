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
    [Route("api/Profile")]
    public class FollowersController : BaseController
    {
        #region Queries Action Methods
        [HttpGet("{appUserId:guid}/{predicate}")]
        [ProducesResponseType(typeof(IEnumerable<ProfileDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetFollowings(Guid appUserId, string predicate)
        {
            var result = await Mediator.Send(new List.Query {  AppUserId = appUserId, Predicate = predicate });
            if (result?.Any() == true)
                return Ok(result);
            else
                return NoContent();
        }
        #endregion


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