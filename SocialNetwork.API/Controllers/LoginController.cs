using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.APIEntity;
using SocialNetwork.Nucleus.Engine.User;

namespace SocialNetwork.API.Controllers
{
    public class LoginController : BaseController
    {
        #region Queries Action Methods
        [HttpPost(nameof(Login))]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous()]
        public async Task<IActionResult> Login(Login.Command command, CancellationToken cancellationToken)
        {
            var user = await Mediator.Send(command, cancellationToken);
            return Ok(user);
        }

        [HttpPost(nameof(Register))]
        [ProducesResponseType(typeof(UserEntity), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous()]
        public async Task<IActionResult> Register(Register.Command command, CancellationToken cancellationToken)
        {
            var user = await Mediator.Send(command, cancellationToken);
            return Ok(user);
        }
        #endregion
    }
}