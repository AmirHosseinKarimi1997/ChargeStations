namespace GreenFlux.Api.Models.ServiceResponses;

public class ServiceResponse<T> : BaseServiceResponse
    where T : class
{
    public ServiceResponse() : base(true, null)
    {
    }

    public T Result { get; set; }
}

