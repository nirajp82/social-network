using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Nucleus.Engine.User;

namespace SocialNetwork.API.Controllers
{
    [AllowAnonymous()]
    public class LoginController : BaseController
    {
        #region Queries Action Methods
        [HttpPost]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Login(Login.Command query, CancellationToken cancellationToken)
        {
            var user = await Mediator.Send(query, cancellationToken);
            return Ok(user);
        }
        #endregion
    }
}