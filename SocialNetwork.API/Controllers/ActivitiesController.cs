using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SocialNetwork.APIEntity;
using SocialNetwork.Nucleus.Engine.Activities;

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


        #region Action Methods
        /// <summary>
        /// Fetch list of all Values
        /// </summary>
        [HttpGet()]
        [ProducesResponseType(typeof(IEnumerable<ActivityEntity>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new List.Query());
            if (result?.Any() == true)
                return Ok(result);
            else
                return NoContent();
        }
        #endregion
    }
}
