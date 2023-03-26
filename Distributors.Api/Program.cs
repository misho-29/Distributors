using Distributors.Api.StartUp;
using Distributors.Api.Middleware;
using FluentValidation.AspNetCore;
using System.Reflection;
using System.Text.Json.Serialization;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Distributors.Application.Models.Validators.AddDistributor;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("DistributorsConnection")!;

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.SuppressModelStateInvalidFilter = true;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    })
    .AddFluentValidation(options =>
    {
        options.RegisterValidatorsFromAssembly(typeof(AddDistributorValidator).Assembly);
    });


builder.Services.AddFluentValidationRulesToSwagger();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureDatabase(connectionString);
builder.Services.ConfigureAutoMapper();
builder.Services.ConfigureDependencyInjection();

var app = builder.Build();
app.UseCustomExceptionsMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
