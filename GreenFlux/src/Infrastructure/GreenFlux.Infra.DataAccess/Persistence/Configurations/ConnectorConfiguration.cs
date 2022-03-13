
using GreenFlux.Domain.Entities.GroupAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenFlux.Infra.DataAccess.Persistence.Configurations;

public class ConnectorConfiguration : IEntityTypeConfiguration<Connector>
{
    public void Configure(EntityTypeBuilder<Connector> builder)
    {

        builder.Property(c => c.ConnectorNumber)
            .IsRequired();

        builder.Property(c => c.MaxCurrentInAmps)
            .IsRequired();

        builder.Property(c => c.ChargeStationId)
            .IsRequired();

        //constraint for combination of chargeStationId and ConnectorNumber
        builder
            .HasIndex(c => new { c.ChargeStationId, c.ConnectorNumber });

        builder
            .HasQueryFilter(c => c.IsDeleted == false);

    }
}

