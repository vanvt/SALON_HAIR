using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALON_HAIR_API.Middlewares
{
    public class ActionFilter : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //if(context.HttpContext.Response.StatusCode!=200 || context.HttpContext.Response.StatusCode != 201)
            //{
            //    throw new Exception("Đã có gì đó sai");
            //}
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            //if (context.HttpContext.Response.StatusCode != 200 || context.HttpContext.Response.StatusCode != 201)
            //{
            //    throw new Exception("Đã có gì đó sai");
            //}
        }
    }
}
