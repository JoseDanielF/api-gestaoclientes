using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Interfaces;
using GestaoClientes.Domain.ValueObjects;

namespace GestaoClientes.Infrastructure.Persistence.Repositories;

public class ClienteRepositoryEmMemoria : IClienteRepository
{
    private static readonly List<Cliente> _clientes = new();

    public Task AdicionarAsync(Cliente cliente)
    {
        _clientes.Add(cliente);
        return Task.CompletedTask;
    }

    public Task<Cliente?> ObterPorCnpjAsync(Cnpj cnpj)
    {
        var cliente = _clientes.FirstOrDefault(c => c.Cnpj == cnpj);
        return Task.FromResult(cliente);
    }
    public Task<Cliente?> ObterPorIdAsync(Guid id)
    {
        var cliente = _clientes.FirstOrDefault(c => c.Id == id);
        return Task.FromResult(cliente);
    }
}