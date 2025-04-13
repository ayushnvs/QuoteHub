using QuoteHub.Helper;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using QuoteHub.Exceptions;
using QuoteHub.Enums;

namespace QuoteHub.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context).ConfigureAwait(false);
            }
            catch (UnauthorizedAccessException exception)
            {
                _logger.LogError($"Unauthorized resource access: {exception.Message}");

                BaseResponse<int> response = new(ResponseStatus.Error)
                {
                    Message = exception.Message
                };

                context.Response.StatusCode = StatusCodes.Status401Unauthorized;

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    // Configure the serializer to convert enums to strings
                    Converters = { new JsonStringEnumConverter() },
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                };

                await context.Response.WriteAsJsonAsync(response, options).ConfigureAwait(false);
            }
            catch (NotFoundException exception)
            {
                _logger.LogError($"Not found exception: {exception.Message}");

                BaseResponse<int> response = new(ResponseStatus.Error)
                {
                    Message = exception.Message
                };

                context.Response.StatusCode = StatusCodes.Status404NotFound;

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    // Configure the serializer to convert enums to strings
                    Converters = { new JsonStringEnumConverter() }
                };

                await context.Response.WriteAsJsonAsync(response, options).ConfigureAwait(false);
            }
            catch (BadHttpRequestException exception)
            {
                _logger.LogError($"Bad request exception: {exception.Message}");

                ModelStateDictionary? modelState = null;

                // Check if ModelState is stored in HttpContext.Items
                if (context.Items.ContainsKey("ModelState"))
                {
                    modelState = context.Items["ModelState"] as ModelStateDictionary;
                }

                BaseResponse<int> response = new(ResponseStatus.Error)
                {
                    Message = exception.Message,
                    Errors = modelState?
                        .Where(modelError => modelError.Value != null && modelError.Value.Errors.Any())
                        .ToDictionary(
                            modelError => modelError.Key,
                            modelError => modelError.Value != null
                                ? modelError.Value.Errors.Select(e => e.ErrorMessage).ToList()
                                : new List<string>()
                        )
                };

                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    // Configure the serializer to convert enums to strings
                    Converters = { new JsonStringEnumConverter() }
                };

                await context.Response.WriteAsJsonAsync(response, options).ConfigureAwait(false);
            }
            catch (Exception exception)
            {
                _logger.LogError($"Internal server exception: {exception.Message}");
                _logger.LogError($"Inner exception: {exception.InnerException}");
                _logger.LogError($"Exception stack trace: {exception.StackTrace}");

                BaseResponse<int> response = new(ResponseStatus.Error)
                {
                    Message = $"An internal server error has occurred: {exception.Message}"
                };

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;

                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    // Configure the serializer to convert enums to strings
                    Converters = { new JsonStringEnumConverter() }
                };

                await context.Response.WriteAsJsonAsync(response, options).ConfigureAwait(false);
            }
        }
    }
}