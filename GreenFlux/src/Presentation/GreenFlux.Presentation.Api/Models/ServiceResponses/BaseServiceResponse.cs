namespace GreenFlux.Api.Models.ServiceResponses;

public class BaseServiceResponse
{
    public BaseServiceResponse(bool isSuccess, string errorMessage = null)
    {
        IsSuccess = isSuccess;
        ErrorMessage = errorMessage;
    }

    public bool IsSuccess { get; set; }
    public string ErrorMessage { get; set; }
}

