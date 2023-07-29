using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace copyrights_fe.Helpers
{
    public class User : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var user_id = copyrights_fe.App.Comm.GetUserId();
            if (user_id <= 0)
            {
                context.Result = new ViewResult() { ViewName = "~/Views/Account/AccLogin.cshtml" };
            }

            base.OnActionExecuting(context);
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            // do something after the action executes
        }


    }
}
