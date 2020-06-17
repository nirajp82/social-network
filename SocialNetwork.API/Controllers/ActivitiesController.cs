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

namespace SocialNetwork.API.Controllers
{
    public class ActivitiesController : BaseController
    {
        #region Queries Action Methods
        /// <summary>
        /// Fetch list of all activities
        /// </summary>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ActivityDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new List.Query(), cancellationToken);
            if (result?.Any() == true)
                return Ok(result);
            else
                return NoContent();
        }


        [HttpGet("{activityId:guid}")]
        [ProducesResponseType(typeof(ActivityDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(ValidateActivityExistsFilter))]
        public async Task<IActionResult> Get(Guid activityId, CancellationToken cancellationToken)
        {
            ActivityDto entity = await Mediator.Send(new Details.Query { ActivityId = activityId }, cancellationToken);
            if (entity != null)
                return Ok(entity);
            else
                return NotFound();
        }
        #endregion


        #region Command Action Methods
        [HttpPost]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] Create.Command request, CancellationToken cancellationToken)
        {
            Guid guid = await Mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(Get), guid);
        }


        [HttpPut("{activityId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(ValidateActivityExistsFilter))]
        public async Task<IActionResult> Put(Guid activityId, [FromBody] Edit.Command request,
            CancellationToken cancellationToken)
        {
            request.ActivityId = activityId;
            await Mediator.Send(request, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{activityId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(ValidateActivityExistsFilter))]
        public async Task<IActionResult> Delete(Guid activityId, CancellationToken cancellationToken)
        {
            await Mediator.Send(new Delete.Command { ActivityId = activityId }, cancellationToken);
            return NoContent();
        }

        [HttpPost("{activityId:guid}/attend")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(ValidateActivityExistsFilter))]
        [ServiceFilter(typeof(ValidateAttendanceFilter))]
        public async Task<IActionResult> Attend(Guid activityId, CancellationToken cancellationToken)
        {
            await Mediator.Send(new Attend.Command { ActivityId = activityId }, cancellationToken);
            return NoContent();
        }


        [HttpPost("{activityId:guid}/unattend")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(ValidateActivityExistsFilter))]
        [ServiceFilter(typeof(ValidateUnAttendanceFilter))]
        public async Task<IActionResult> Unattend(Guid activityId, CancellationToken cancellationToken)
        {
            await Mediator.Send(new Unattend.Command { ActivityId = activityId }, cancellationToken);
            return NoContent();
        }
        #endregion
    }
}