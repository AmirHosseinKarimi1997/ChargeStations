using GreenFlux.Domain.Entities.GroupAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenFlux.Infra.DataAccess.Persistence.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.Property(g => g.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(g => g.CapacityInAmps)
            .IsRequired();

        builder
            .HasMany(g => g.ChargeStations)
            .WithOne(e => e.Group)
            .OnDelete(DeleteBehavior.Cascade);

        var navigation = builder.Metadata.FindNavigation(nameof(Group.ChargeStations));

        navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder
            .HasQueryFilter(g => g.IsDeleted == false);

    }
}

