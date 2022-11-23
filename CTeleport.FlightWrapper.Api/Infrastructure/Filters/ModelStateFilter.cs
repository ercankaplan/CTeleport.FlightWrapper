using CTeleport.FlightWrapper.Api.Models.Base;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace CTeleport.FlightWrapper.Api.Infrastructure.Filters
{
    public class ModelStateFilter : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
                context.Result = new BadRequestObjectResult(new ResponseErrorViewModel(context.ModelState));
        }

        public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
