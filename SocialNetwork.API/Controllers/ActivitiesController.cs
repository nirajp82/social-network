using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.APIEntity;
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
        [ProducesResponseType(typeof(IEnumerable<ActivityEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await Mediator.Send(new List.ListQuery(), cancellationToken);
            if (result?.Any() == true)
                return Ok(result);
            else
                return NoContent();
        }


        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(ActivityEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ValidateActivityExists()]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            ActivityEntity entity = await Mediator.Send(new Details.DetailsQuery { Id = id }, cancellationToken);
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
        public async Task<IActionResult> Post([FromBody] Create.CreateCommand request, CancellationToken cancellationToken)
        {
            Guid guid = await Mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(Get), guid);
        }


        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ValidateActivityExists()]
        public async Task<IActionResult> Put(Guid id, [FromBody] Edit.EditCommand request,
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
        [ValidateActivityExists()]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new Delete.DeleteCommand { Id = id }, cancellationToken);
            return NoContent();
        }
        #endregion
    }
}