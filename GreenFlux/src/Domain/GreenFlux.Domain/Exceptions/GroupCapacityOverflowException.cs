
namespace GreenFlux.Domain.Exceptions;
public class GroupCapacityOverflowException : DomainBaseException
{
    public GroupCapacityOverflowException()
        : base("Groupc capacity will be overflowed!")
    {
    }

}

