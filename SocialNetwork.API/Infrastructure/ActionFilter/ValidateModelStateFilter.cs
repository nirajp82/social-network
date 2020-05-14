using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace SocialNetwork.API
{
    public class ValidateModelStateFilter : IAsyncActionFilter
    {
        #region MyRegion
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (context.ModelState.IsValid)
                await next();
            else
                context.Result = new BadRequestObjectResult(context.ModelState);
        }
        #endregion
    }
}
