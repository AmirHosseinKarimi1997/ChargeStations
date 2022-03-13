namespace GreenFlux.Api.Models.ServiceResponses;

public class IntegerServiceResponse : BaseServiceResponse
{
    public IntegerServiceResponse() : base(true, null)
    {
    }

    public int Result { get ; set; }
}

