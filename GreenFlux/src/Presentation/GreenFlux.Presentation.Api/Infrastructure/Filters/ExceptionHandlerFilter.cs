using GreenFlux.Api.Models.ServiceResponses;
using GreenFlux.Application.Exceptions;
using GreenFlux.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace GreenFlux.Api.Infrastructure.Filters;

public class ExceptionHandlerFilter : IExceptionFilter, IActionFilter
{
    private string errorLogTemplate = $"{{@ControllerName}}{{@Params}}";
    private readonly IList<Type> _exceptionBaseTypes;
    private IDictionary<string, object> _actionArguments;
    //private readonly ILogger _logger;
    private string _controllerName;

    public ExceptionHandlerFilter()//ILogger logger
    {
        //_logger = logger;
        _exceptionBaseTypes = new List<Type> { typeof(DomainBaseException), typeof(ApplicationBaseException) };
    }

    public void OnException(ExceptionContext filterContext)
    {
        BaseServiceResponse result;
        string exceptionMessage = string.Empty;
        var exception = filterContext.Exception;

        //check if exception is handled or not.
        if (_exceptionBaseTypes.Any(x => x.Name == exception.GetType().BaseType.Name))
            exceptionMessage = exception.Message;
        else
            exceptionMessage = "There is something wrong :| . Please try again!";

        result = new BaseServiceResponse(false, exceptionMessage);

        filterContext.Result = new ObjectResult(result)
        {
            StatusCode = ((int)HttpStatusCode.OK)
        };
        filterContext.ExceptionHandled = true;

        var inputs = _actionArguments;
        //_logger.LogError(filterContext.Exception, errorLogTemplate, _controllerName, inputs);

    }

    public void OnActionExecuting(ActionExecutingContext context)
    {
        _actionArguments = context.ActionArguments;
        _controllerName = context.RouteData.Values["controller"].ToString();
    }
    public void OnActionExecuted(ActionExecutedContext context)
    {
    }

}

