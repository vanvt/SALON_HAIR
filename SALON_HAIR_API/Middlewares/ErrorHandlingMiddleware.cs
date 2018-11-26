using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using SALON_HAIR_API.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ULTIL_HELPER;

namespace SALON_HAIR_API.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private  ILogHelper _logHelper;
        public ErrorHandlingMiddleware(RequestDelegate next)
        {
             _next = next;
           
        }
        public async Task Invoke(HttpContext context, ILogHelper logHelper /* other scoped dependencies */)
        {
            try
            {
                _logHelper = logHelper;              
                await _next(context);
            }
            catch (UnexpectedException unexpectedException)
            {
                await HandleExceptionDetailAsync(context, unexpectedException);
            }
            catch (BadRequestException ex)
            {
                await HandleExceptionDetailAsync(context, ex);
            }          
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }           
            finally
            {

            }

        }
        private async Task HandleExceptionDetailAsync(HttpContext context, BadRequestException exception)
        {
            string message = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.HttpCode ;
            var respone = context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                //error = message,
                errorCode = exception.HttpCode,
                errorDesc = message,
            }));
            try
            {
                await LogAsync(context, exception);
            }
            catch (Exception e)
            {

            }
            await respone;
        }
        private async Task HandleExceptionDetailAsync(HttpContext context, UnexpectedException unexpectedException)
        {
            Exception exception = unexpectedException.exception;
            string message = exception.InnerException == null ? exception.Message : exception.InnerException.Message;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = unexpectedException.HttpCode;
            var respone = context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {            
                errorCode = unexpectedException.HttpCode,
                errorDesc = message,
            }));
            try
            {
                await LogAsync(context, unexpectedException);
            }
            catch (Exception e)
            {

            }
            await respone;
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string message = "";
            if (exception.InnerException != null)
            {
                message = exception.InnerException.Message;             
            }
            else
            {
                message = exception.Message;
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 500;
            var respone = context.Response.WriteAsync(JsonConvert.SerializeObject(new {
                //error = message,
                errorCode = 500,
                errorDesc = message,
            }));
            try
            {
               await LogAsync(context, exception);
            }
            catch (Exception e)
            {

            }
            await respone;
        }
        private async Task LogAsync(HttpContext context, Exception exception)
        {
            var _data = new
            {           
                _Soure = exception.InnerException == null ? exception.Source : exception.InnerException.Source,
                _StackTrace = exception.InnerException == null ? exception.StackTrace : exception.InnerException.StackTrace,
                _Data = exception.InnerException == null ? exception.Data : exception.InnerException.Data,
                _Message = exception.InnerException == null ? exception.Message : exception.InnerException.Message,

            };
            await LogAsync(context, _data);
        }
        private async Task LogAsync(HttpContext context, UnexpectedException exception)
        {
            var _data = new
            {
                _Soure = exception.InnerException == null ? exception.Source : exception.InnerException.Source,
                _StackTrace = exception.InnerException == null ? exception.StackTrace : exception.InnerException.StackTrace,
                _Data = exception.DataLog,
                _Message = exception.InnerException == null ? exception.Message : exception.InnerException.Message,

            };
            await LogAsync(context, _data);
        }
        private async Task LogAsync(HttpContext context, BadRequestException exception)
        {
            var _data = new
            {
                _Soure = exception.InnerException == null ? exception.Source : exception.InnerException.Source,
                _StackTrace = exception.InnerException == null ? exception.StackTrace : exception.InnerException.StackTrace,
                _Data = exception.DataLog,
                _Message = exception.InnerException == null ? exception.Message : exception.InnerException.Message,

            };
            await LogAsync(context, _data);
        }
        private async Task LogAsync(HttpContext context, object data)
        {



            object datalog = new
            {
                context.TraceIdentifier,
                _method = context.Request.Method,
                _url = context.Request.Path.Value,
                _tooken = context.Request.Headers.Where(e => e.Key.Equals("Authorization")).Select(e => e.Value).FirstOrDefault(),
                _data = data

            };
          //  , "wwwroot", "exception");


            var setting = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };
            string json = "";
            try
            {
                 json = JsonConvert.SerializeObject(datalog);

            }catch(Exception xx)
            {
                try
                {
                    json = JsonConvert.SerializeObject(
             new
             {
                 context.TraceIdentifier,
                 _method = context.Request.Method,
                 _url = context.Request.Path.Value,
                 _tooken = context.Request.Headers.Where(e => e.Key.Equals("Authorization")).Select(e => e.Value).FirstOrDefault(),

             }
             );
                }
                catch(Exception cc)
                {

                }
         
            }
          
           await _logHelper.LogAsync(json, "wwwroot", "exception");

        }

    }
    //Extension method used to add the middleware to the HTTP request pipeline.
    public static class ErrorHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
