using ShopNT.Services.Exceptions;
using System.Text.Json;

namespace ShopNT.Api.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
               await _next(context);
            }
            catch (Exception ex)
            {
                var response=context.Response;
                response.ContentType = "application/json";
                string message = ex.Message;
                var errors = new List<RestExceptionError>();
                switch (ex)
                {
                    case RestException rex:
                        response.StatusCode = (int)rex.Code;
                        message= rex.Message;
                        errors=rex.Errors;
                        break;
                    default:
                        response.StatusCode=500;
                        break;
                }
                var result = new {message= message,errors};
                await response.WriteAsync(JsonSerializer.Serialize(result));
            }
            
        }

    }
}
