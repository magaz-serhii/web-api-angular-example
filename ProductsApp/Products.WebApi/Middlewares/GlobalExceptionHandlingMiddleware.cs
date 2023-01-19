using FluentValidation;
using Products.WebApi.Exceptions;
using Products.WebApi.Models;
using System.Net;
using System.Text.Json;

namespace Products.WebApi.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        public GlobalExceptionHandlingMiddleware()
        {
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch(EntityNotFoundException e)
            {
                await WriteResponseAsync(context, HttpStatusCode.NotFound, e);
            }
            catch(ValidationException e)
            {
                await WriteResponseAsync(context, HttpStatusCode.BadRequest, e);
            }
            catch (Exception e)
            {
                await WriteResponseAsync(context, HttpStatusCode.InternalServerError, e);
            }
        }

        private static async Task WriteResponseAsync(HttpContext context, HttpStatusCode code, Exception e)
        {
            var responseDetails = new ErrorResponse
            {
                Status = code,
                Error = e.Message
            };

            var json = JsonSerializer.Serialize(responseDetails, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            await context.Response.WriteAsync(json);
        }
    }
}
