using EvaluacionTecnica.Business;
using EvaluacionTecnica.Persistence;
using EvaluacionTecnica.Presentation.WebApi.Extensions;
using EvaluacionTecnica.Presentation.WebApi.NewFolder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddServices();
builder.Services.AddDtoMaping();
builder.Services.AddPersistenceInfrastructure(builder.Configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerExtensions();
builder.Services.AddApiVersioningExtension();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseSwaggerExtensions();

app.MapControllers();

app.Run();
