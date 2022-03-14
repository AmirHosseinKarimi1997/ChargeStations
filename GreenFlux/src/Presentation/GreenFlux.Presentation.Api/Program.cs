using FluentValidation.AspNetCore;
using GreenFlux.Api.Infrastructure.Filters;
using GreenFlux.Api.Models.GroupViewModels;
using GreenFlux.Application;
using GreenFlux.Domain.Common;
using GreenFlux.Infra.DataAccess;
using GreenFlux.Presentation.Api;

var builder = WebApplication.CreateBuilder(args);

ServiceRegistration.RegisterServices(builder.Services, builder.Configuration);

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