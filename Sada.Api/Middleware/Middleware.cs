using System.Diagnostics;
using Sada.Application.Interfaces;
using Sada.Domain.Entities;

namespace Sada.Api.Middleware;

public class RequestLoggingMiddleware
{
    private readonly RequestDelegate _request;

    public RequestLoggingMiddleware(RequestDelegate next)
    {
        _request = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogService logService)
    {
        var watch = Stopwatch.StartNew();

        await _request(context);

        watch.Stop();

        var log = new Log
        {
            Metodo = context.Request.Method,
            Endpoint = context.Request.Path,
            StatusCode = context.Response.StatusCode,
            DataHora = DateTime.Now
        };

        await logService.SalvarAsync(log);
    }
}