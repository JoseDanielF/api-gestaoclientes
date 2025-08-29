using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.Interfaces;
using GestaoClientes.Domain.ValueObjects;
using MediatR;

namespace GestaoClientes.Application.Clientes.Commands;

public class CriaClienteCommandHandler : IRequestHandler<CriaClienteCommand, Guid>
{
    private readonly IClienteRepository _clienteRepository;

    public CriaClienteCommandHandler(IClienteRepository clienteRepository)
    {
        _clienteRepository = clienteRepository;
    }

    public async Task<Guid> Handle(CriaClienteCommand request, CancellationToken cancellationToken)
    {
        var cnpjValueObject = Cnpj.Criar(request.Cnpj);
        if (cnpjValueObject is null)
        {
            throw new ArgumentException("O CNPJ fornecido é inválido.");
        }

        var clienteExistente = await _clienteRepository.ObterPorCnpjAsync(cnpjValueObject);
        if (clienteExistente is not null)
        {
            throw new InvalidOperationException("Um cliente com este CNPJ já está cadastrado.");
        }

        var novoCliente = Cliente.Criar(request.NomeFantasia, cnpjValueObject);

        await _clienteRepository.AdicionarAsync(novoCliente);

        return novoCliente.Id;
    }
}