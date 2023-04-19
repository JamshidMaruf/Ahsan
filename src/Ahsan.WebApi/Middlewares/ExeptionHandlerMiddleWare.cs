#pragma warning disable

using Ahsan.Service.Exceptions;
using Ahsan.WebApi.Models;
using System;

namespace Ahsan.WebApi.Middlewares;

public class ExeptionHandlerMiddleWare
{
    private readonly RequestDelegate next;
    private readonly ILogger<ExeptionHandlerMiddleWare> logger;
    public ExeptionHandlerMiddleWare(RequestDelegate next, ILogger<ExeptionHandlerMiddleWare> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await this.next(context);

        }
        catch (CustomException exception)
        {
            context.Response.StatusCode = exception.Code;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Code = exception.Code,
                Error = exception.Message
            });
        }
        catch (Exception exception)
        {
            this.logger.LogError($"{exception.ToString()}\n");
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new Response
            {
                Code = 500,
                Error = exception.Message
            });
        }
    }
}
