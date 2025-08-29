using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace GestaoClientes.Domain.Interfaces;

public interface IClienteRepository
{
    Task AdicionarAsync(Cliente cliente);
    Task<Cliente?> ObterPorIdAsync(Guid id);
    Task<Cliente?> ObterPorCnpjAsync(Cnpj cnpj);
}