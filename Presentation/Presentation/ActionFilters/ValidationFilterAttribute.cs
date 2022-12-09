using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.ActionFilters
{
    /// <summary>
    /// 
    /// </summary>
    public class ValidationFilterAttribute : IActionFilter
    {
        /// <summary>
        /// 
        /// </summary>
        public ValidationFilterAttribute()
        {
        }
        /// <inheritdoc/>

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var action = context.RouteData.Values["action"];
            var controller = context.RouteData.Values["controller"];

           
            var param = context.ActionArguments.Values?.Contains("Dto");

            if (param is null)
            {
                context.Result = new BadRequestObjectResult($"Object is null. Controller: {controller}, action: {action}");
                return;
            }

            if (!context.ModelState.IsValid)
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
        }
/// <inheritdoc/>

        public void OnActionExecuted(ActionExecutedContext context) { }
    }

}
