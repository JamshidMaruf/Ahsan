#pragma warning disable

using Ahsan.Service.Exceptions;
using Ahsan.WebApi.Models;

namespace Ahsan.WebApi.Middlewares;

public class ExeptionHandlerMiddleWare
{
    private readonly RequestDelegate next;

    public ExeptionHandlerMiddleWare(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this.next(context);
        }
        catch(CustomException exeption)
        {
            context.Response.StatusCode = exeption.Code;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Code = exeption.Code,
                Error = exeption.Message
            });
        }
        catch(Exception exeption)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Code = 500,
                Error = exeption.Message
            });
        }
    }
}
