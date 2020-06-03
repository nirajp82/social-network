using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.APIEntity;
using SocialNetwork.Nucleus.Engine.Activities;
using System;
using System.Threading;
using SocialNetwork.WebUtil;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        #region Members
        private ILogger<ActivitiesController> _logger { get; }
        private IMediator _mediator { get; }
        #endregion


        #region Constructor
        public ActivitiesController(ILogger<ActivitiesController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }
        #endregion


        #region Queries Action Methods
        /// <summary>
        /// Fetch list of all activities
        /// </summary>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ActivityEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new List.Query(), cancellationToken);
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
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            ActivityEntity entity = await _mediator.Send(new Details.Query { Id = id }, cancellationToken);
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
            Guid guid = await _mediator.Send(request, cancellationToken);
            return CreatedAtAction(nameof(Get), guid);
        }


        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ValidateActivityExists()]
        public async Task<IActionResult> Put(Guid id, [FromBody] Edit.Command request,
            CancellationToken cancellationToken)
        {
            request.Id = id;
            await _mediator.Send(request, cancellationToken);
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
            await _mediator.Send(new Delete.Command { Id = id }, cancellationToken);
            return NoContent();
        }
        #endregion
    }
}