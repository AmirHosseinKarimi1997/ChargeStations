
namespace GreenFlux.Domain.Exceptions;
public class GroupCapacityLessThanSumMaxCurrentException : DomainBaseException
{
    public GroupCapacityLessThanSumMaxCurrentException()
        : base("Groupc capacity should not be less than it's connectors max current")
    {
    }

}

