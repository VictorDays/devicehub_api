using devicehub_api.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Adicionar o DbContext com banco de dados em memória
builder.Services.AddDbContext<DeviceHubDbContext>(options =>
    options.UseInMemoryDatabase("DeviceHubDb"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new Microsoft.OpenApi.Models.OpenApiInfo
        {
            Title = "DeviceHub API",
            Version = "v1",
            Description = "API para gerenciar ativos como notebooks, desktops, monitores e periféricos em diferentes empresas.",
            Contact = new OpenApiContact
            {
                Name = "API DeviceHub",
                Email = "victoralves@unitins.br"
            }
        }
        );
    var xmlFile =$"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile) ;

    c.IncludeXmlComments(xmlPath);
}
);

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
