using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dto;
using SocialNetwork.Nucleus.Engine.Activity;
using System;
using System.Threading;
using SocialNetwork.WebUtil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using SocialNetwork.Infrastructure;
using SocialNetwork.Nucleus.Engine.Photo;

namespace SocialNetwork.API.Controllers
{
    public class PhotoController : BaseController
    {
        #region Queries Action Methods
        #endregion


        #region Command Action Methods
        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] Add.Command request, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }


        [HttpDelete("{activityId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(ValidateActivityExistsFilter))]
        [Authorize(Policy = InfrastrctureConstants.ACTIVITY_HOST_POLICY_NAME)]
        public async Task<IActionResult> Delete(Guid activityId, CancellationToken cancellationToken)
        {
            await Mediator.Send(new Delete.Command { ActivityId = activityId }, cancellationToken);
            return NoContent();
        }
        #endregion
    }
}