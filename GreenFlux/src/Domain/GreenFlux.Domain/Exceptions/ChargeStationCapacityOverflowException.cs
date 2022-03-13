
namespace GreenFlux.Domain.Exceptions;
public class ChargeStationCapacityOverflowException : DomainBaseException
{
    public ChargeStationCapacityOverflowException()
        : base("ChargeStation capacity is full. you can not add new connector in this charge station.")
    {
    }

}

