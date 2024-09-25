
using System.Net;
using Newtonsoft.Json;
using Fiedly.BookingYard.Api.Models;
using Fieldy.BookingYard.Application.Exceptions;

namespace Fiedly.BookingYard.Api
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext httpContext, Exception ex)
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            CustomProblemDetails problemDetails;
            switch (ex)
            {
                case BadRequestException badRequestException:
                    statusCode = HttpStatusCode.BadRequest;
                    problemDetails = new CustomProblemDetails()
                    {
                        Title = badRequestException.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.BadRequest),
                        Detail = badRequestException.InnerException?.Message,
                        Errors = badRequestException.ValidationErrors,
                    };
                    break;
                case NotFoundException notFoundException:
                    statusCode = HttpStatusCode.NotFound;
                    problemDetails = new CustomProblemDetails()
                    {
                        Title = notFoundException.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.NotFound),
                        Detail = notFoundException.InnerException?.Message,
                    };
                    break;
                case ConflictException conflictException:
                    statusCode = HttpStatusCode.Conflict;
                    problemDetails = new CustomProblemDetails()
                    {
                        Title = conflictException.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.Conflict),
                        Detail = conflictException.InnerException?.Message,
                    };
                    break;
                default:
                    problemDetails = new CustomProblemDetails()
                    {
                        Title = ex.Message,
                        Status = (int)statusCode,
                        Type = nameof(HttpStatusCode.InternalServerError),
                        Detail = ex.StackTrace,
                    };
                    break;
            }
            httpContext.Response.StatusCode = (int)statusCode;
            var logMessage = JsonConvert.SerializeObject(problemDetails);
            _logger.LogError(ex, "An error occurred: {LogMessage}", logMessage);
            await httpContext.Response.WriteAsJsonAsync(problemDetails);
        }
    }
}