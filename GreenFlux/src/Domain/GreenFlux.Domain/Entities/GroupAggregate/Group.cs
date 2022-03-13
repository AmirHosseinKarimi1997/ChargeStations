using GreenFlux.Domain.Common;

namespace GreenFlux.Domain.Entities.GroupAggregate;

public class Group : AuditableEntity, IAggregateRoot
{
    public Group()
    {
        _chargeStations = new List<ChargeStation>();
    }
    public Group(string name, ulong capacityInAmps)
        :this()
    {
        SetName(name);
        SetCapacity(capacityInAmps);
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public ulong CapacityInAmps { get; private set; }

    private readonly List<ChargeStation> _chargeStations;
    public IReadOnlyCollection<ChargeStation> ChargeStations => _chargeStations;

    public void SetCapacity(ulong capacityinAmps)
    {
        var sumCurrent = CalculateMaxCurrentSumInAmps();

        if (Id > 0 && capacityinAmps < sumCurrent)
            throw new Exception();

        this.CapacityInAmps = capacityinAmps;
    }

    public void SetName(string name)
    {
        this.Name = name;
    }

    public void AddChargeStation(string name)
    {
        var chargeStation = new ChargeStation(name);

        this._chargeStations.Add(chargeStation);
    }

    public void RemoveChargeStation(int chargeStationId)
    {
        var chargeStation = this._chargeStations.FirstOrDefault(x => x.Id == chargeStationId);

        if (chargeStation == null)
            throw new Exception();

        this._chargeStations.Remove(chargeStation);
    }

    public ulong CalculateMaxCurrentSumInAmps()
    {
        var sum = Convert.ToUInt64(this.ChargeStations.Sum(x => x.Connectors.Sum(x => x.MaxCurrentInAmps)));

        return sum;
    }
}

