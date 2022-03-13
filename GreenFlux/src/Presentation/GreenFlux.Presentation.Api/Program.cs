using FluentValidation.AspNetCore;
using GreenFlux.Api.Infrastructure.Filters;
using GreenFlux.Api.Models.GroupViewModels;
using GreenFlux.Application;
using GreenFlux.Domain.Common;
using GreenFlux.Infra.DataAccess;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddScoped<ResultFilter>();
builder.Services.AddScoped<ExceptionHandlerFilter>()
    .AddFluentValidation((x => x.AutomaticValidationEnabled = false));

ConfigHelper.MaxNumberOfConnectorsAttachedToChargeStations = Convert.ToInt32(builder.Configuration["MaxNumberOfConnectorsAttachedToChargeStations"]);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
