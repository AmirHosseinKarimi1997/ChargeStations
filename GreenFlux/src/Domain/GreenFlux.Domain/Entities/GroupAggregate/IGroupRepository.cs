using GreenFlux.Domain.Common;

namespace GreenFlux.Domain.Entities.GroupAggregate;

public interface IGroupRepository : IRepository<Group>
{
    Group Add(Group group);

    void Update(Group group);

    void Delete(Group group);

    Task<Group> GetAsync(int groupId);
}

