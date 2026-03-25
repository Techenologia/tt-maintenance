using System.Net;
using System.Text.Json;

namespace TT.Backend.Core.Pipeline
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
        {
            _next   = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erreur non gérée : {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            var (statusCode, message) = ex switch
            {
                KeyNotFoundException      => (HttpStatusCode.NotFound,            ex.Message),
                InvalidOperationException => (HttpStatusCode.BadRequest,          ex.Message),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized,      ex.Message),
                ArgumentException         => (HttpStatusCode.BadRequest,          ex.Message),
                _                         => (HttpStatusCode.InternalServerError, "Une erreur interne est survenue")
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode  = (int)statusCode;

            var response = new
            {
                error   = message,
                status  = (int)statusCode,
                path    = context.Request.Path.ToString(),
                traceId = context.TraceIdentifier
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                })
            );
        }
    }
}