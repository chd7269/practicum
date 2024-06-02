using Microsoft.AspNetCore.Mvc.Filters;
using MoneySystemServer.Controllers;

namespace MoneySystemServer.Code
{
    public class IsManagerAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //var sessionService = context.HttpContext.RequestServices.GetService<IT>();

            //if (sessionService == null)
            //{
            //    context.Result = GlobalController.GetJsonResult("Error 8888");
            //    return;
            //}

            //if (!context.HttpContext.User.Identity.IsAuthenticated)
            //{
            //    context.Result = GlobalController.GetJsonResult("Error 8888");
            //    return;
            //}

            //var user = await sessionService.GetCurrentUser();
            //if (user == null)
            //{
            //    context.Result = GlobalController.GetJsonResult("Error 8888");
            //    return;
            //}

            //if (user.UserType.Id != 1)
            //{
            //    context.Result = GlobalController.GetJsonResult("Error 8888");
            //    return;
            //}

            await next();
        }

    }
}
