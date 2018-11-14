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
            catch(DbUpdateException ex)
            {
                await HandleExceptionDetailAsync(context, ex);
            }
            catch(MySqlException x)
            {
                await HandleExceptionDetailAsync(context, x);
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
            string message = exception.Message;
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
                await _logHelper.LogAsync(new
                {
                    _method = context.Request.Method,
                    _url = context.Request.Path.Value,
                    _data = new
                    {
                        //_body = FormatRequest(context.Request),
                        //_respone = context.Response,
                        _Soure = exception.InnerException == null ? exception.Source : exception.InnerException.Source,
                        _StackTrace = exception.InnerException == null ? exception.StackTrace : exception.InnerException.StackTrace,
                        _Data = exception.InnerException == null ? exception.Data : exception.InnerException.Data,
                        _Message = exception.InnerException == null ? exception.Message : exception.InnerException.Message,
                    }
                });
            }
            catch (Exception e)
            {

            }
            await respone;
        }
        private async Task HandleExceptionDetailAsync(HttpContext context, UnexpectedException unexpectedException)
        {
            Exception exception = unexpectedException.exception;
            string message = exception.Message;
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = context.Response.StatusCode;
            var respone = context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {            
                errorCode = context.Response.StatusCode,
                errorDesc = message,
            }));
            try
            {
           
                await _logHelper.LogAsync(new
                {
                    _method = context.Request.Method,
                    _url = context.Request.Path.Value,
                    _tooken = context.Request.Headers.Where(e=>e.Key.Equals("Authorization")).Select(e => e.Value).FirstOrDefault(),
                    _data = new
                    {
                        //_body = FormatRequest(context.Request),
                        //_respone = context.Response,
                        _Soure = exception.InnerException == null ? exception.Source : exception.InnerException.Source,
                        _StackTrace = exception.InnerException == null ? exception.StackTrace : exception.InnerException.StackTrace,
                        _Data = unexpectedException.DataLog,
                        _Message = exception.InnerException == null ? exception.Message : exception.InnerException.Message,
                    }
                });
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
                await _logHelper.LogAsync(new
                {
                    _method = context.Request.Method,
                    _url = context.Request.Path.Value,
                    _data = new
                    {
                        //_body = FormatRequest(context.Request),
                        //_respone = context.Response,
                       _Soure = exception.InnerException == null ? exception.Source : exception.InnerException.Source,
                       _StackTrace = exception.InnerException == null ? exception.StackTrace : exception.InnerException.StackTrace,
                       _Data = exception.InnerException == null ? exception.Data : exception.InnerException.Data,
                       _Message = exception.InnerException == null ? exception.Message : exception.InnerException.Message ,
                      
                    }
                });
            }
            catch (Exception e)
            {

            }          
            await respone;
        }
        private async Task HandleExceptionDetailAsync(HttpContext context, MySqlException exception)
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
            var respone = context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                //error = message,
                errorCode = 500,
                errorDesc = "Định hack của bố mầy à",
            }));
            try
            {
                await _logHelper.LogAsync(new
                {
                    _method = context.Request.Method,
                    _url = context.Request.Path.Value,
                    _data = new
                    {
                        //_body = FormatRequest(context.Request),
                        //_respone = context.Response,
                        _Soure = exception.InnerException == null ? exception.Source : exception.InnerException.Source,
                        _StackTrace = exception.InnerException == null ? exception.StackTrace : exception.InnerException.StackTrace,
                        _Data = exception.InnerException == null ? exception.Data : exception.InnerException.Data,
                        _Message = exception.InnerException == null ? exception.Message : exception.InnerException.Message,

                    }
                });
            }
            catch (Exception e)
            {

            }
            await respone;
        }
        private async Task HandleExceptionDetailAsync(HttpContext context,DbUpdateException exception)
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
            var respone = context.Response.WriteAsync(JsonConvert.SerializeObject(new
            {
                //error = message,
                errorCode = 500,
                errorDesc = message
            }));
            try
            {
                await _logHelper.LogAsync(new
                {
                    _method = context.Request.Method,
                    _url = context.Request.Path.Value,
                    _data = new
                    {
                        //_body = FormatRequest(context.Request),
                        //_respone = context.Response,
                        _Soure = exception.InnerException == null ? exception.Source : exception.InnerException.Source,
                        _StackTrace = exception.InnerException == null ? exception.StackTrace : exception.InnerException.StackTrace,
                        _Data = exception.InnerException == null ? exception.Data : exception.InnerException.Data,
                        _Message = exception.InnerException == null ? exception.Message : exception.InnerException.Message,

                    }
                });
            }
            catch (Exception e)
            {

            }
            await respone;
        }
        private async Task<string> FormatRequest(HttpRequest request)
        {
            var body = request.Body;

            //This line allows us to set the reader for the request back at the beginning of its stream.
            request.EnableRewind();

            //We now need to read the request stream.  First, we create a new byte[] with the same length as the request stream...
            var buffer = new byte[Convert.ToInt32(request.ContentLength)];

            //...Then we copy the entire request stream into the new buffer.
            await request.Body.ReadAsync(buffer, 0, buffer.Length);

            //We convert the byte[] into a string using UTF8 encoding...
            var bodyAsText = Encoding.UTF8.GetString(buffer);

            //..and finally, assign the read body back to the request body, which is allowed because of EnableRewind()
            request.Body = body;

            return $"{request.Scheme} {request.Host}{request.Path} {request.QueryString} {bodyAsText}";
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
