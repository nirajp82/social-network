using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Dto;
using SocialNetwork.Infrastructure;
using SocialNetwork.Nucleus;
using SocialNetwork.Nucleus.User;

namespace SocialNetwork.API.Controllers
{
    public class UserController : BaseController
    {
        #region Query Action Methods
        [HttpGet]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Current(CancellationToken cancellationToken)
        {
            var user = await Mediator.Send(new CurrentAppUser.Query(), cancellationToken);
            return Ok(user);
        }
        #endregion


        #region Command Action Methods
        [HttpPost(nameof(Login))]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous()]
        public async Task<IActionResult> Login(Login.Command command, CancellationToken cancellationToken)
        {
            UserDto user = await Mediator.Send(command, cancellationToken);
            return Ok(user);
        }

        [HttpPost(nameof(Register))]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [AllowAnonymous()]
        public async Task<IActionResult> Register(Register.Command command, CancellationToken cancellationToken)
        {
            UserDto user = await Mediator.Send(command, cancellationToken);
            return Ok(user);
        }

        [HttpPost(nameof(Refresh))]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ServiceFilter(typeof(ValidateExpiredTokenFilter))]
        [AllowAnonymous()]
        public async Task<IActionResult> Refresh(RefreshToken.Query query, CancellationToken cancellationToken) 
        {
            UserDto user = await Mediator.Send(query, cancellationToken);
            return Ok(user);
        }
        #endregion
    }
}