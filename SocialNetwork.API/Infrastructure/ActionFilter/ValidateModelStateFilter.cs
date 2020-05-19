using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.API
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        #region Members
        public ILogger<ValidateModelStateFilter> _logger { get; }
        #endregion


        #region Constuctor
        public ValidateModelStateFilter(ILogger<ValidateModelStateFilter> logger)
        {
            _logger = logger;
        }
        #endregion


        #region Public Methods
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                _logger.LogError(string.Join(APIConst.LineSeparator, context.ModelState.Values.SelectMany(v => v.Errors)));
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
        #endregion
    }
}
