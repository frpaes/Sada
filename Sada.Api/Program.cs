using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Sada.Application.Interfaces;
using Sada.Application.Service;
using Sada.Domain.Interfaces;
using Sada.Infrastructure.Context;
using Sada.Infrastructure.Repository;
using System.Reflection;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

    options.IncludeXmlComments(xmlPath);
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Sada API",
        Version = "v1",
        Description = @"
            # README

            API para gerenciamento de itens.
            -> Métodos

            - Criar item
            - Consultar itens
            - Consultar item por Id
            - Atualizar item
            - Excluir item

            -> Status disponíveis

            Código / Descrição
            1 = Pendente
            2 = Em progresso
            3 = Concluído

            -> Exemplos

            -> Criar item
            {
              ""title"": ""Estudar .NET"",
              ""description"": ""Revisar Clean Architecture"",
              ""dueDate"": ""2026-05-10"",
              ""status"": 1
            }"
    });

});


builder.Services.AddDbContext<SadaDbContext>(x => x.UseInMemoryDatabase("SadaDb"));

builder.Services.AddScoped<ISadaRepository, SadaRepository>();
builder.Services.AddScoped<ISadaService, SadaService>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();



app.MapControllers();

app.Run();