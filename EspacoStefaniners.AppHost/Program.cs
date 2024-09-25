using Microsoft.Extensions.DependencyInjection;

var builder = DistributedApplication.CreateBuilder(args);

// Add service defaults & Aspire components.
//builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

//var cache = builder.AddRedis("cache");

var barservice = builder.AddProject<Projects.EspacoStefaniners_BarService>("barservice")
    .WithExternalHttpEndpoints();
    //.WithReference(cache);

builder.Build().Run();
