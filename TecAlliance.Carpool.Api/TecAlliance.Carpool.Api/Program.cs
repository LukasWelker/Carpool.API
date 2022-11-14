using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using TecAlliance.Carpool.Business.Interfaces;
using TecAlliance.Carpool.Business.SampleProvider;
using TecAlliance.Carpool.Business.Services;
using TecAlliance.Carpool.Data.Interfaces;
using TecAlliance.Carpool.Data.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// The follwoing part is needed to enable the use of Samples
builder.Services.AddSwaggerGen(options =>
{
    options.ExampleFilters();
});

//With the following statements your samples will be used
builder.Services.AddSingleton<PassengerDtoProvider>();

builder.Services.AddSwaggerExamplesFromAssemblyOf<PassengerDtoProvider>();
builder.Services.AddSingleton<CarpoolDtoProvider>();
builder.Services.AddSwaggerExamplesFromAssemblyOf<CarpoolDtoProvider>();


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v2",
        Title = "CarpoolAPI",
        Description = "A Web API for managing Carpools",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Nightsleeper2",
            Url = new Uri("https://example.com/contact")
        },
        License = new OpenApiLicense
        {
            Name = "Full License",
            Url = new Uri("https://example.com/license")
        }
    });

    // using System.Reflection;
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});


//Überall wo IPassengerDataService genutzt werden soll nutzt er die PassengerDataService
builder.Services.AddScoped<IPassengerDataService, PassengerDataService>();
builder.Services.AddScoped<IPassengerBusinessService, PassengerBusinessService>();
builder.Services.AddScoped<ICarpoolDataService, CarpoolDataService>();
builder.Services.AddScoped<ICarpoolBusinessService, CarpoolBusinessService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
