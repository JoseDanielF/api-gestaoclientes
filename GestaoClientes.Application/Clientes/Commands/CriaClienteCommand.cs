using MediatR;

namespace GestaoClientes.Application.Clientes.Commands;

public record CriaClienteCommand(
    string NomeFantasia,
    string Cnpj) : IRequest<Guid>;