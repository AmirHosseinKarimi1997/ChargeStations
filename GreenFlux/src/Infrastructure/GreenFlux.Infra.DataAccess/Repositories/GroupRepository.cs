using GreenFlux.Domain.Common;
using GreenFlux.Domain.Entities.GroupAggregate;
using GreenFlux.Infra.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GreenFlux.Infra.DataAccess.Repositories;

public class GroupRepository : IGroupRepository
{
    private readonly UnitOfWork _context;

    public GroupRepository(UnitOfWork context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork
    {
        get
        {
            return _context;
        }
    }

    public Group Add(Group group)
    {
        return _context.Groups.Add(group).Entity;
    }

    public void Delete(Group group)
    {
        _context.Groups.Remove(group);
    }

    public async Task<Group> GetAsync(int groupId)
    {
        var group = await _context
                    .Groups
                    .Include(x => x.ChargeStations)
                    .ThenInclude(x => x.Connectors)
                    .FirstOrDefaultAsync(o => o.Id == groupId);

        if (group == null)
        {
            group = _context
                        .Groups
                        .Local
                        .FirstOrDefault(o => o.Id == groupId);
        }

        return group;
    }

    public void Update(Group group)
    {
        _context.Entry(group).State = EntityState.Modified;
    }
}

