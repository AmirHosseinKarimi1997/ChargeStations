
using GreenFlux.Application.Groups.Queries.Dtos;
using GreenFlux.Application.Interfaces;
using GreenFlux.Domain.Common;
using GreenFlux.Domain.Entities.GroupAggregate;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace GreenFlux.Infra.DataAccess.Persistence;

public partial class UnitOfWork: DbContext, IUnitOfWork
{
    private readonly IDateTime _dateTime;

    public UnitOfWork(
        DbContextOptions<UnitOfWork> options,
        IDateTime dateTime) : base(options)
    {
        _dateTime = dateTime;
    }

    public DbSet<Group> Groups => Set<Group>();

    public DbSet<ChargeStation> ChargeStations => Set<ChargeStation>();

    public DbSet<Connector> Connectors => Set<Connector>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {

        AuditEntity();

        var result = await base.SaveChangesAsync(cancellationToken);

        return result;
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(builder);
    }

    private void AuditEntity()
    {
        foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.SetCreated(_dateTime.Now);
                    entry.Entity.SetIsDeleted(false);
                    break;

                case EntityState.Modified:
                    entry.Entity.SetLastModified(_dateTime.Now);
                    break;

                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.SetLastModified(_dateTime.Now);
                    entry.Entity.SetDeletedAt(_dateTime.Now);
                    entry.Entity.SetIsDeleted(true);
                    break;
            }
        }
    }
}

