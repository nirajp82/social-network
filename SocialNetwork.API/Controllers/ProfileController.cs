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

        #region Command Action Methods
        [HttpPut()]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put([FromBody] Edit.Command request, CancellationToken cancellationToken)
        {
            string displayName = await Mediator.Send(request, cancellationToken);
            return Ok(displayName);
        }
        #endregion
    }
}