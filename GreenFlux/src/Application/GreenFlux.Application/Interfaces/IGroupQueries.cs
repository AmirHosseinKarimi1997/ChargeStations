
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Domain.Entities.GroupAggregate;

namespace GreenFlux.Application.Interfaces;

public interface IGroupQueries
{
    Task<Group> GetGroupAsync(int id);

    Task<IEnumerable<Group>> GetAllGroupsAsync();

    Task<ChargeStation> GetChargeStationAsync(int groupId, int id);

    Task<IEnumerable<ChargeStation>> GetAllChargeStationsAsync(int groupId);

    Task<Connector> GetConnectorAsync(int groupId, int chargeStationId, int connectorNumber);

    Task<IEnumerable<Connector>> GetAllConnectorsAsync(int groupId, int chargeStationId);
}

