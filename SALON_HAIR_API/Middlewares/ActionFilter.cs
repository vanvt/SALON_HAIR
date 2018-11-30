using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ULTIL_HELPER;

namespace SALON_HAIR_API.Middlewares
{
    public class ActionFilter : IActionFilter
    {
        public IConfiguration _configuration { get; }
        private ILogHelper _logHelper;
        public ActionFilter(ILogHelper logHelper, IConfiguration configuration)
        {
            _logHelper = logHelper;
            _configuration = configuration;
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var start = DateTime.Now;
            Console.WriteLine("OnActionExecuted");
            Console.WriteLine("-----------------------------------------------------------------------------");
            bool LogAllRespone = false;
            bool.TryParse( _configuration["LogAllRespone:value"], out LogAllRespone);
            if (LogAllRespone)
            {

                try
                {
                    var datalog = context.Result.GetType().GetProperty("Value").GetValue(context.Result, null);
                    var setting = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    };
                    string json = JsonConvert.SerializeObject(
                        new {
                            context.HttpContext.TraceIdentifier,Respone = datalog
                        }, setting);

                    _logHelper.LogAsync(json, "wwwroot", "respone");
                }
                catch (Exception ex)
                {                  

                }
                              
            }
            var end = DateTime.Now;
            Console.WriteLine($"Finished in {(end-start).TotalMilliseconds} miliseconds");
            Console.WriteLine("-----------------------------------------------------------------------------");
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            bool LogAllRequest = false;
            bool.TryParse(_configuration["LogAllRequest:value"], out LogAllRequest);
            if (LogAllRequest)
            {
                _logHelper.LogAsync( new {
                    context.HttpContext.TraceIdentifier,
                    context.HttpContext.Request.Method,
                    Path =  context.HttpContext.Request.Path.Value,
                    Tooken = context.HttpContext.Request.Headers.Where(e => e.Key.Equals("Authorization")).Select(e => e.Value).FirstOrDefault(),
                    Request = context.ActionArguments
                        }, "wwwroot", "request");
            }          
        }
    }
}
