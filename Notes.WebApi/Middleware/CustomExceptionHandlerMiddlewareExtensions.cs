using Microsoft.AspNetCore.Builder;

namespace Notes.WebApi.Middleware
{
    // экстеншн функция для класса, реализующего IApplicationBuilder
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        // ключевое слово this отсылает к тому экземляру, к которому применяется расширяющая функция
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
