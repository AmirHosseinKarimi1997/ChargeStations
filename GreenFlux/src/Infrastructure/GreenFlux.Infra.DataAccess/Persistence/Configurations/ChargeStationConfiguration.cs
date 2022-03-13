
using GreenFlux.Domain.Entities.GroupAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenFlux.Infra.DataAccess.Persistence.Configurations;

public class ChargeStationConfiguration : IEntityTypeConfiguration<ChargeStation>
{
    public void Configure(EntityTypeBuilder<ChargeStation> builder)
    {
        builder.Property(t => t.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(t => t.GroupId)
            .IsRequired();

        builder
            .HasMany(g => g.Connectors)
            .WithOne(e => e.ChargeStation)
            .OnDelete(DeleteBehavior.Cascade);

        var navigation = builder.Metadata.FindNavigation(nameof(ChargeStation.Connectors));

        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasQueryFilter(c => c.IsDeleted == false);
    }
}

