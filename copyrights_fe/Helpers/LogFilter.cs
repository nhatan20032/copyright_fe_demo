using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace copyrights_fe.Helpers
{
    public class LogFilter : IActionFilter
    {
        private readonly ILogger<LogFilter> _logger;

        public LogFilter(ILogger<LogFilter> logger)
        {
            _logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            // log all action
            var controller = context.Controller;
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"Executing action {actionName}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // log after the action executes
            var controller = context.Controller;
            var actionName = context.ActionDescriptor.DisplayName;
            _logger.LogInformation($"Executed action {actionName}");
        }
    }
}