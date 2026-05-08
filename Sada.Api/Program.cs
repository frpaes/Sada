using Microsoft.EntityFrameworkCore;
using Sada.Application.Interfaces;
using Sada.Application.Service;
using Sada.Domain.Interfaces;
using Sada.Infrastructure.Context;
using Sada.Infrastructure.Repository;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SadaDbContext>(
    x => x.UseInMemoryDatabase("SadaDb"));

builder.Services.AddScoped<ISadaRepository, SadaRepository>();
builder.Services.AddScoped<ISadaService, SadaService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();