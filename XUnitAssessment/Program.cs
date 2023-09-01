using Microsoft.EntityFrameworkCore;
using XUnitAssessment.Data;
using XUnitAssessment.Services;
using XUnitAssessment.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MySQL Injection
builder.Services.AddDbContext<XunitDbContext>(option =>
option.UseMySQL(builder.Configuration.GetConnectionString("ConnectionString")));

builder.Services.AddScoped<IAppuser, AppuserRepository>();
builder.Services.AddScoped<IUserType, UsertypeRepository>();

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
