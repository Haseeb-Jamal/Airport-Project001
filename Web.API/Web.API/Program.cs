using Dapper;
using Microsoft.Data.SqlClient;
using Web.API.Endpoints;
using Web.API.Models;
using Web.API.Services;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton(ServiceProvider =>
{
    var configuration = ServiceProvider.GetRequiredService<IConfiguration>();
   var connectionString = configuration.GetConnectionString("DefaultConnection") ??
    throw new ApplicationException("The Connection String is Null");
    return new SqlConnectionFactory(connectionString);

});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapAirportEndpoints();
   


app.Run();


