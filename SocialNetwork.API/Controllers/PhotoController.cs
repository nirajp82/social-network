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
using SocialNetwork.Nucleus.Photo;

namespace SocialNetwork.API.Controllers
{
    public class PhotoController : BaseController
    {
        #region Queries Action Methods
        #endregion


        #region Command Action Methods
        [HttpPost]
        [ProducesResponseType(typeof(PhotoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromForm] Add.Command request, CancellationToken cancellationToken)
        {
            PhotoDto result = await Mediator.Send(request, cancellationToken);
            return Ok(result);
        }


        [HttpPost("{photoId:guid}/setmain")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SetMain(Guid photoId, CancellationToken cancellationToken)
        {
            await Mediator.Send(new SetMain.Command { PhotoId = photoId }, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Delete Photo
        /// </summary>
        [HttpDelete("{photoId:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType((int)StatusCodeEx.Status499ClientClosedRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(Guid photoId, CancellationToken cancellationToken)
        {
            await Mediator.Send(new Delete.Command { PhotoId = photoId }, cancellationToken);
            return NoContent();
        }
        #endregion
    }
}