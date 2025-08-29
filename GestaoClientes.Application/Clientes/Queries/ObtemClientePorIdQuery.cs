using GestaoClientes.Domain.Entities;
using MediatR;

namespace GestaoClientes.Application.Clientes.Queries;

public record ObtemClientePorIdQuery(Guid Id) : IRequest<Cliente?>;