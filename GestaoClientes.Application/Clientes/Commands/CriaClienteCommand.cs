using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace GestaoClientes.Application.Clientes.Commands;

public record CriaClienteCommand(
    string NomeFantasia,
    string Cnpj) : IRequest<Guid>;