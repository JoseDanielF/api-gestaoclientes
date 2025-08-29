using GestaoClientes.Application.Clientes.Commands;
using GestaoClientes.Domain.Interfaces;
using GestaoClientes.Infrastructure.Persistence.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
    cfg.RegisterServicesFromAssemblyContaining<CriaClienteCommand>());

builder.Services.AddSingleton<IClienteRepository, ClienteRepositoryEmMemoria>();

builder.Services.AddOpenApi();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
