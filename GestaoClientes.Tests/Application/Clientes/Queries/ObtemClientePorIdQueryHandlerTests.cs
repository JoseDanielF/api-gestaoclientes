using Moq;
using Xunit;
using GestaoClientes.Domain.Interfaces;
using GestaoClientes.Application.Clientes.Queries;
using GestaoClientes.Domain.Entities;
using GestaoClientes.Domain.ValueObjects;

namespace GestaoClientes.Tests.Application.Clientes.Queries;

public class ObtemClientePorIdQueryHandlerTests
{
    private readonly Mock<IClienteRepository> _mockRepo;
    private readonly ObtemClientePorIdQueryHandler _handler;

    public ObtemClientePorIdQueryHandlerTests()
    {
        _mockRepo = new Mock<IClienteRepository>();
        _handler = new ObtemClientePorIdQueryHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_DeveRetornarCliente_QuandoIdExiste()
    {
        var clienteId = Guid.NewGuid();
        var cnpj = Cnpj.Criar("12345678000199")!;
        var clienteExistente = Cliente.Criar("Empresa Encontrada", cnpj);

        var query = new ObtemClientePorIdQuery(clienteId);

        _mockRepo.Setup(r => r.ObterPorIdAsync(clienteId))
                 .ReturnsAsync(clienteExistente);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(clienteExistente.NomeFantasia, result.NomeFantasia);
    }

    [Fact]
    public async Task Handle_DeveRetornarNulo_QuandoIdNaoExiste()
    {
        var query = new ObtemClientePorIdQuery(Guid.NewGuid());

        _mockRepo.Setup(r => r.ObterPorIdAsync(It.IsAny<Guid>()))
                 .ReturnsAsync((Cliente?)null);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Null(result);
    }
}

