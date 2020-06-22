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
using SocialNetwork.Nucleus.Engine.User;

namespace SocialNetwork.API.Controllers
{
    public class ProfileController : BaseController
    {
        #region Queries Action Methods
        /// <summary>
        /// Fetch User Profile
        /// </summary>
        [HttpGet("{appUserId:guid}")]
        [ProducesResponseType(typeof(ProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(Guid appUserId, CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new Details.Query { AppUserId = appUserId }, cancellationToken);
            if (result != null)
                return Ok(result);
            else
                return BadRequest();
        }
        #endregion


        //#region Command Action Methods
        //[HttpPost]
        //[ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //public async Task<IActionResult> Post([FromBody] Create.Command request, CancellationToken cancellationToken)
        //{
        //    Guid guid = await Mediator.Send(request, cancellationToken);
        //    return CreatedAtAction(nameof(Get), guid);
        //}


        //[HttpPut("{activityId:guid}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ServiceFilter(typeof(ValidateActivityExistsFilter))]
        //[Authorize(Policy = InfrastrctureConstants.ACTIVITY_HOST_POLICY_NAME)]
        //public async Task<IActionResult> Put(Guid activityId, [FromBody] Edit.Command request,
        //    CancellationToken cancellationToken)
        //{
        //    request.ActivityId = activityId;
        //    await Mediator.Send(request, cancellationToken);
        //    return NoContent();
        //}

        //[HttpDelete("{activityId:guid}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ServiceFilter(typeof(ValidateActivityExistsFilter))]
        //[Authorize(Policy = InfrastrctureConstants.ACTIVITY_HOST_POLICY_NAME)]
        //public async Task<IActionResult> Delete(Guid activityId, CancellationToken cancellationToken)
        //{
        //    await Mediator.Send(new Delete.Command { ActivityId = activityId }, cancellationToken);
        //    return NoContent();
        //}

        //[HttpPost("{activityId:guid}/attend")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ServiceFilter(typeof(ValidateActivityExistsFilter))]
        //[ServiceFilter(typeof(ValidateAttendanceFilter))]
        //public async Task<IActionResult> Attend(Guid activityId, CancellationToken cancellationToken)
        //{
        //    await Mediator.Send(new Attend.Command { ActivityId = activityId }, cancellationToken);
        //    return NoContent();
        //}


        //[HttpPost("{activityId:guid}/unattend")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(StatusCodes.Status403Forbidden)]
        //[ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ServiceFilter(typeof(ValidateActivityExistsFilter))]
        //[ServiceFilter(typeof(ValidateUnAttendanceFilter))]
        //public async Task<IActionResult> Unattend(Guid activityId, CancellationToken cancellationToken)
        //{
        //    await Mediator.Send(new Unattend.Command { ActivityId = activityId }, cancellationToken);
        //    return NoContent();
        //}
        //#endregion
    }
}