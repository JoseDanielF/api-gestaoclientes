using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.ValueObjects;
namespace GestaoClientes.Domain.Interfaces;

public interface IClienteRepository
{
    Task AdicionarAsync(Cliente cliente);
    Task<Cliente?> ObterPorIdAsync(Guid id);
    Task<Cliente?> ObterPorCnpjAsync(Cnpj cnpj);
}