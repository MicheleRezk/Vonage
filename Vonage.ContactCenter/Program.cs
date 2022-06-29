using System.Text.Json;
using System.Text.Json.Serialization;
using Vonage.ContactCenter.Common;
using Vonage.ContactCenter.Services;
using Vonage.ContactCenter.Settings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ServiceSettings>(builder.Configuration.GetSection("ServiceSettings"));
builder.Services.AddSingleton<IEmployeesRepo, EmployeesRepo>();
builder.Services.AddSingleton<IContactCenterServices, ContactCenterServices>();

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
public partial class Program { }