using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace EasyCommerce.Server.Shared.Common.Filters;

public class ApiExceptionCatcherFilter<TController> : IExceptionFilter
{
    private readonly ILogger<TController> _logger;
    public ApiExceptionCatcherFilter(ILogger<TController> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        context.Result = new ContentResult
        {
            Content = context.Exception.ToString(),
            StatusCode = context.HttpContext.Response.StatusCode
        };
        _logger.LogError($"[{context.ActionDescriptor.DisplayName}] : {context.Exception.Message} \n      [Error] : {context.Exception.StackTrace}");
    }
}
public class ApiExceptionCatcherFilter : IExceptionFilter
{
    private readonly ILogger _logger;
    public ApiExceptionCatcherFilter(ILogger logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        context.Result = new ContentResult
        {
            Content = context.Exception.ToString(),
            StatusCode = context.HttpContext.Response.StatusCode
        };
        _logger.LogError($"[{context.ActionDescriptor.DisplayName}] : {context.Exception.Message} \n      [Error] : {context.Exception.StackTrace}");
    }
}
