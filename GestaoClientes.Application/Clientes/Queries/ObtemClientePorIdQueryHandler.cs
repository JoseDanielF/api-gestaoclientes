using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Interfaces;
using MediatR;

namespace GestaoClientes.Application.Clientes.Queries;

public class ObtemClientePorIdQueryHandler : IRequestHandler<ObtemClientePorIdQuery, Cliente?>
{
    private readonly IClienteRepository _clienteRepository;

    public ObtemClientePorIdQueryHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<Cliente?> Handle(ObtemClientePorIdQuery request, CancellationToken cancellationToken)
    {
        return await _clienteRepository.ObterPorIdAsync(request.Id);
    }
}