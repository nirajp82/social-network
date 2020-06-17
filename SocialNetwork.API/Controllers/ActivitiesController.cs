using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.DTO;
using SocialNetwork.Nucleus.Engine.Activity;
using System;
using System.Threading;
using SocialNetwork.WebUtil;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace SocialNetwork.API.Controllers
{
    public class ActivitiesController : BaseController
    {
        #region Queries Action Methods
        /// <summary>
        /// Fetch list of all activities
        /// </summary>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ActivityDTO>), StatusCodes.Status200OK)]
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


        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ActivityDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(ValidateActivityExistsFilter))]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            ActivityDTO entity = await Mediator.Send(new Details.Query { Id = id }, cancellationToken);
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


        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(ValidateActivityExistsFilter))]
        public async Task<IActionResult> Put(Guid id, [FromBody] Edit.Command request,
            CancellationToken cancellationToken)
        {
            request.Id = id;
            await Mediator.Send(request, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(ValidateActivityExistsFilter))]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new Delete.Command { Id = id }, cancellationToken);
            return NoContent();
        }

        [HttpPost("{activityId}/attend")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Attend(Guid activityId, CancellationToken cancellationToken)
        {
            await Mediator.Send(new Attend.Command { ActivityId = activityId }, cancellationToken);
            return NoContent();
        }


        [HttpPost("{activityId}/unattend")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Unattend(Guid activityId, CancellationToken cancellationToken)
        {
            await Mediator.Send(new Unattend.Command { ActivityId = activityId }, cancellationToken);
            return NoContent();
        }
        #endregion
    }
}