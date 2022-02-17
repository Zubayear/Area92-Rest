using Area92.Context;
using Area92.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(setupAction =>
{
    setupAction.ReturnHttpNotAcceptable = true;

}).AddNewtonsoftJson(setupAction =>
{
    setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
}).AddXmlDataContractSerializerFormatters();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AnimesContext>(optionsAction => optionsAction.UseSqlServer(builder.Configuration.GetConnectionString("AnimesDBConnectionString")));
builder.Services.AddScoped<IAnimeRepository, AnimeRepository>();
builder.Services.AddTransient<IPropertyMappingService, PropertyMappingService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddHttpClient();

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
