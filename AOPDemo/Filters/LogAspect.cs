using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AOPDemo.Filters
{
    public class LogAspect : IActionFilter
    {
        void IActionFilter.OnActionExecuted(ActionExecutedContext context)
        {
            // after.
        }

        void IActionFilter.OnActionExecuting(ActionExecutingContext context)

        {
            // before.
        }
    }
}
