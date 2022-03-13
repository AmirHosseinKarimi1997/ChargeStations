
using GreenFlux.Domain.Common;

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

    public void SetName(string name)
    {
        this.Name = name;
    }

    public void SetConnector(int connectorNumber, uint currentInAmps)
    {
        var addedConnectorsCount = this._connectors.Count;

        //5 from config
        if (addedConnectorsCount >= 5)
            throw new Exception();

        var connector = new Connector(connectorNumber, currentInAmps);
        
        this._connectors.Add(connector);
    }

    public void RemoveConnector(int connectorNumber)
    {
        var connector = this._connectors.FirstOrDefault(x => x.ConnectorNumber == connectorNumber);

        if (connector == null)
            throw new Exception();

        this._connectors.Remove(connector);
    }
}

