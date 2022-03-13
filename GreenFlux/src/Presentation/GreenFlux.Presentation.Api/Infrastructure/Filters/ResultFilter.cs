using GreenFlux.Api.Models.ServiceResponses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace GreenFlux.Api.Infrastructure.Filters;

public class ResultFilter : IResultFilter
{
    public void OnResultExecuting(ResultExecutingContext context)
    {
        if (context.Result is ObjectResult objectResult)
        {
            WrapResponse(objectResult);
        }
        else
        {
            context.Result = new ObjectResult(new BaseServiceResponse(true));
        }

    }

    private void WrapResponse(ObjectResult objectResult)
    {

        if (objectResult.Value == null)
            WrapNullResponse(objectResult);

        else
        {
            Type typeArgument = objectResult.Value.GetType();

            if (typeArgument == typeof(int))
                WrapIntegerResponse(objectResult);
            else
                WrapGenericResponse(objectResult);

        }

    }


    private void WrapNullResponse(ObjectResult objectResult)
    {

        Type responseClass = typeof(NullDataServiceResponse);
        object serviceResponseInstance = Activator.CreateInstance(responseClass);

        PropertyInfo dataPropertyInfo = responseClass.GetProperty(nameof(NullDataServiceResponse.Result));

        dataPropertyInfo.SetValue(serviceResponseInstance, null, null);
        objectResult.Value = serviceResponseInstance;
    }

    private void WrapGenericResponse(ObjectResult objectResult)
    {

        Type typeArgument = objectResult.Value.GetType();

        Type genericResponseClass = typeof(ServiceResponse<>);
        Type constructedServiceResponseModel = genericResponseClass.MakeGenericType(typeArgument);

        object serviceResponseInstance = Activator.CreateInstance(constructedServiceResponseModel);

        var val = objectResult.Value;

        PropertyInfo dataPropertyInfo = constructedServiceResponseModel.GetProperty(nameof(ServiceResponse<dynamic>.Result));
        dataPropertyInfo.SetValue(serviceResponseInstance, val, null);
        objectResult.Value = serviceResponseInstance;
    }

    private void WrapIntegerResponse(ObjectResult objectResult)
    {

        Type responseClass = typeof(IntegerServiceResponse);
        object serviceResponseInstance = Activator.CreateInstance(responseClass);

        var val = objectResult.Value;

        PropertyInfo dataPropertyInfo = responseClass.GetProperty(nameof(IntegerServiceResponse.Result));
        dataPropertyInfo.SetValue(serviceResponseInstance, val, null);
        objectResult.Value = serviceResponseInstance;
    }

    public void OnResultExecuted(ResultExecutedContext context)
    {
    }
}

