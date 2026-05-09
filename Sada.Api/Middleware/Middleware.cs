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
        await _request(context);

        var path = context.Request.Path.ToString();

        if (!path.StartsWith("/api/itens"))
            return;

        var log = new Log
        {
            Metodo = context.Request.Method,
            Json = path,
            StatusCode = context.Response.StatusCode,
            DataHora = DateTime.Now
        };

        await logService.SalvarAsync(log);
    }
}