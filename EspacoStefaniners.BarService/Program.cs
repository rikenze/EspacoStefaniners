using EspacoStefaniners.BarService.Data;
using EspacoStefaniners.BarService.Data.Interfaces;
using EspacoStefaniners.BarService.Services;
using EspacoStefaniners.BarService.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("BarConnection");
builder.Services.AddDbContext<BarContext>(opts => opts.UseSqlite(connectionString));

// Adiciona os serviços
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<IItensPedidoService, ItensPedidoService>();
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.AddServiceDefaults();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    s.SwaggerDoc("v1", new OpenApiInfo { Title = "EspacoStefaniners", Version = "v1" });

    // Adicione esta linha para incluir o arquivo XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    s.IncludeXmlComments(xmlPath);

    s.SchemaFilter<SwaggerIgnorePostSchemaFilter>();
});

builder.Services.AddDbContext<IBarContext, BarContext>();


var app = builder.Build();

app.MapDefaultEndpoints();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "EspacoStefaniners V1")
    );
}

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Produtos}/{action=GetAll}/{id?}");

app.Run();