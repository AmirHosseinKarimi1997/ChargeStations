
using GreenFlux.Domain.Common;
using GreenFlux.Domain.Exceptions;

namespace GreenFlux.Domain.Entities.GroupAggregate;

public class ChargeStation: AuditableEntity
{
    public ChargeStation()
    {
        _connectors = new List<Connector>();
    }
    public ChargeStation(string name)
        :this()
    {
        SetName(name);
    }

    public int Id { get; private set; }

    public string Name { get; private set; }

    public int GroupId { get; private set; }

    public Group Group { get; private set; } = null!;


    private readonly List<Connector> _connectors;
    public IReadOnlyCollection<Connector> Connectors => _connectors;

    internal void SetName(string name)
    {
        this.Name = name;
    }

    internal void AddConnector(int connectorNumber, uint maxCurrentInAmps)
    {
        ValidateAddingNewConnector(connectorNumber);

        var connector = new Connector(connectorNumber, maxCurrentInAmps);
        
        this._connectors.Add(connector);
    }

    internal void RemoveConnector(int connectorNumber)
    {
        var connector = this._connectors.FirstOrDefault(x => x.ConnectorNumber == connectorNumber);

        if (connector == null)
            throw new ConnectorNotFoundException(this.Id, connectorNumber);

        this._connectors.Remove(connector);
    }

    internal void SetConnectorMaxCurrent(int connectorNumber, uint maxCurrentInAmps)
    {
        var connector = this.Connectors.FirstOrDefault(x => x.ConnectorNumber == connectorNumber);

        connector.SetMaxCurrentInAmps(maxCurrentInAmps);
    }

    private void ValidateAddingNewConnector(int connectorNumber)
    {
        var addedConnectorsCount = this._connectors.Count;

        //5 from config
        if (addedConnectorsCount >= 5)
            throw new ChargeStationCapacityOverflowException();

        var isAlreadyExist = this.Connectors.Any(x => x.ConnectorNumber == connectorNumber);

        if (isAlreadyExist)
            throw new ConnectorAlreadyExistsException();
    }
}

