namespace GreenFlux.Api.Models.ServiceResponses;

public class BaseServiceResponse
{
    public BaseServiceResponse(bool isSuccess, string[] errorMessages = null)
    {
        IsSuccess = isSuccess;
        ErrorMessages = errorMessages;
    }

    public bool IsSuccess { get; set; }
    private string[] _errorMessages { get; set;}
    public string[] ErrorMessages 
    {
        get { return _errorMessages; }
        set 
        { 
            if (IsSuccess) 
                _errorMessages = null;
            else
                _errorMessages = value;
        }
    }
}

