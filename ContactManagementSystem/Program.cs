using ContactManagementSystem;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
var dbType = builder.Configuration["DatabaseType"];//

if (dbType == "SQL")
{
    builder.Services.AddScoped<SqlContactRepository>();
    builder.Services.AddScoped<IContactRepository,SqlContactRepository>(); // ✅ Important
}


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
