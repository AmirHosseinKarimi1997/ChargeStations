
using GreenFlux.Domain.Entities.GroupAggregate;

namespace GreenFlux.Application.Interfaces;

public interface IGroupQueries
{
    Task<Group> GetGroupAsync(int id);

    Task<IEnumerable<Group>> GetAllGroupsAsync();

    Task<ChargeStation> GetChargeStationAsync(int groupId, int id);

    Task<IEnumerable<ChargeStation>> GetAllChargeStationsAsync(int groupId);
}

