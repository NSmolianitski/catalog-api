using CatalogApi.Core.Exceptions;

namespace CatalogApi.Web.Middleware;

public class GlobalExceptionsHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionsHandlerMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException e)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;
            await context.Response.WriteAsJsonAsync(e.Message);
        }
        catch (ConflictException e)
        {
            context.Response.StatusCode = StatusCodes.Status409Conflict;
            await context.Response.WriteAsJsonAsync(e.Message);
        }
        catch (Exception ex)
        {
            logger.LogError("Unhandled Exception: {ex}", ex);

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await context.Response.WriteAsJsonAsync("An error occurred while processing your request.");
        }
    }
}