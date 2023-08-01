using Breakpoint.Business.Services;
using Breakpoint.Domain.Models;
using Breakpoint.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<BreakpointContext>(opt => opt.UseInMemoryDatabase(databaseName: "Breakpoint"));
builder.Services.AddTransient<ILaptopService, LaptopService>();
builder.Services.AddTransient<ILaptopRepository, LaptopRepository>();

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
