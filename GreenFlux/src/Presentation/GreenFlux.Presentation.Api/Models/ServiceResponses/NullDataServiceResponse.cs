namespace GreenFlux.Api.Models.ServiceResponses;

public class NullDataServiceResponse : BaseServiceResponse
{
    public NullDataServiceResponse() : base(true, null)
    {
        Result = null;
    }

    public object Result { get; set; }
}

