using CryptocurrencyQuote.Domain.Model.Exceptions;
using CryptocurrencyQuote.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace CryptocurrencyQuote.WebAPI.Filters
{
    public class CustomExceptionFilter: ExceptionFilterAttribute

    {
        public override void OnException(ExceptionContext context)
        {
            context.HttpContext.Response.ContentType = "application/json";

            HttpStatusCode statusCode ;

           switch(context.Exception)
            {
                case UnauthorizedException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case BadRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case TooManyRequestsException:
                    statusCode = HttpStatusCode.TooManyRequests;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var exceptionString = new ErrorResponseData()
            {
                StatusCode = (int)statusCode,
                Message = context.Exception.Message,
                Path = context.Exception.StackTrace
            };

            context.Result = new JsonResult(exceptionString);
        }
    }
}
