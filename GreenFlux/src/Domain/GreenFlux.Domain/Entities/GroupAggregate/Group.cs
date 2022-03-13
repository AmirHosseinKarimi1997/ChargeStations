using GreenFlux.Domain.Common;
using GreenFlux.Domain.Exceptions;

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
            throw new GroupCapacityLessThanSumMaxCurrentException();

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
            throw new ChargeStationNotFoundException(this.Id, chargeStationId);

        this._chargeStations.Remove(chargeStation);
    }

    public void UpdateChargeStation(int chargeStationId, string name)
    {
        var chargeStation = this.ChargeStations.FirstOrDefault(x => x.Id == chargeStationId);

        chargeStation.SetName(name);
    }

    public void AddConnectorToChargeStation(int chargeStationId, int connectorNumber, uint maxCurrentInAmps)
    {
        var sumCurrentAmps = CalculateMaxCurrentSumInAmps();

        if (sumCurrentAmps + maxCurrentInAmps > this.CapacityInAmps)
            throw new GroupCapacityOverflowException();

        var chargeStation = this.ChargeStations.FirstOrDefault(x => x.Id == chargeStationId);

        chargeStation.AddConnector(connectorNumber, maxCurrentInAmps);
    }

    public void RemoveConnectorFromChargeStation(int chargeStationId, int connectorNumber)
    {
        var chargeStation = this.ChargeStations.FirstOrDefault(x => x.Id == chargeStationId);

        chargeStation.RemoveConnector(connectorNumber);
    }

    public void SetConnectorMaxCurrent(int chargeStationId, int connectorNumber, uint maxCurrentInAmps)
    {
        var sumCurrentAmps = CalculateMaxCurrentSumInAmpsExceptCurrentConnector(chargeStationId, connectorNumber);

        if (sumCurrentAmps + maxCurrentInAmps > this.CapacityInAmps)
            throw new GroupCapacityOverflowException();

        var chargeStation = this.ChargeStations.FirstOrDefault(x => x.Id == chargeStationId);

        chargeStation.SetConnectorMaxCurrent(connectorNumber, maxCurrentInAmps);
    }

    private ulong CalculateMaxCurrentSumInAmpsExceptCurrentConnector(int chargeStationId, int connectorNumber)
    {
        var connectors = this.ChargeStations.SelectMany(x => x.Connectors);
        var sum = connectors.Where(x => x.ChargeStationId != chargeStationId && x.ConnectorNumber != connectorNumber).Sum(x => x.MaxCurrentInAmps);
            
        return Convert.ToUInt64(sum);
    }

    private ulong CalculateMaxCurrentSumInAmps()
    {
        var connectors = this.ChargeStations.SelectMany(x => x.Connectors);
        var sum = connectors.Sum(x => x.MaxCurrentInAmps);

        return Convert.ToUInt64(sum);
    }
}

