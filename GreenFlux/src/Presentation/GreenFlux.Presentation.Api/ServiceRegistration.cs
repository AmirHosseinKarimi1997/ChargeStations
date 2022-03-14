using FluentValidation.AspNetCore;
using GreenFlux.Api.Infrastructure.Filters;
using GreenFlux.Application;
using GreenFlux.Domain.Common;
using GreenFlux.Infra.DataAccess;

namespace GreenFlux.Presentation.Api
{
    public static class ServiceRegistration
    {

        public static IServiceCollection RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddApplication();
            services.AddInfrastructure(configuration);

            services.AddScoped<ResultFilter>();
            services.AddScoped<ExceptionHandlerFilter>()
                .AddFluentValidation((x => x.AutomaticValidationEnabled = false));

            ConfigHelper.MaxNumberOfConnectorsAttachedToChargeStations = Convert.ToInt32(configuration["MaxNumberOfConnectorsAttachedToChargeStations"]);
            // Add services to the container.

            services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            return services;
        }
    }
}
