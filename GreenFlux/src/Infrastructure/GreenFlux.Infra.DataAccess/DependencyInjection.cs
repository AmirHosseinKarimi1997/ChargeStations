using GreenFlux.Application.Interfaces;
using GreenFlux.Domain.Common;
using GreenFlux.Domain.Entities.GroupAggregate;
using GreenFlux.Infra.DataAccess.Persistence;
using GreenFlux.Infra.DataAccess.Queries;
using GreenFlux.Infra.DataAccess.Repositories;
using GreenFlux.Infra.DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GreenFlux.Infra.DataAccess;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        if (Convert.ToBoolean(configuration["UseInMemoryDatabase"]))
        {
            services.AddDbContext<UnitOfWork>(options =>
                options.UseInMemoryDatabase("GreenFluxDb"));
        }
        else
        {
            services.AddDbContext<UnitOfWork>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(UnitOfWork).Assembly.FullName)));
        }

        services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<UnitOfWork>());

        services.AddScoped<IGroupRepository, GroupRepository>();

        services.AddTransient<IDateTime, DateTimeService>();
        services.AddTransient<IGroupQueries, GroupQueries>();

        return services;
    }
}

