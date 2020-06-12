using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace SocialNetwork.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        #region Private Members
        //private ILogger<BaseController> _logger { get; set; }

        private IMediator _mediator { get; set; }
        #endregion


        #region Public Members
        //protected ILogger<BaseController> Logger => _logger ??
        //            (_logger = HttpContext.RequestServices.GetService<ILogger<BaseController>>());

        protected IMediator Mediator => _mediator ??
                    (_mediator = HttpContext.RequestServices.GetService<IMediator>());
        #endregion
    }
}