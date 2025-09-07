using Xunit;
using Moq;
using GestaoClientes.Domain.Interfaces;
using GestaoClientes.Application.Clientes.Commands;
using GestaoClientes.Domain.ValueObjects;
using GestaoClientes.Domain.Entities;

namespace GestaoClientes.Tests.Application.Clientes.Commands;

public class CriaClienteCommandHandlerTests
{
    private readonly Mock<IClienteRepository> _mockRepo;
    private readonly CriaClienteCommandHandler _handler;

    public CriaClienteCommandHandlerTests()
    {
        _mockRepo = new Mock<IClienteRepository>();
        _handler = new CriaClienteCommandHandler(_mockRepo.Object);
    }

    [Fact]
    public async Task Handle_DeveCriarCliente_QuandoDadosSaoValidos()
    {
        var command = new CriaClienteCommand("Empresa Teste", "12345678000199");

        _mockRepo.Setup(r => r.ObterPorCnpjAsync(It.IsAny<Cnpj>()))
                 .ReturnsAsync((Cliente?)null);

        var result = await _handler.Handle(command, CancellationToken.None);

        _mockRepo.Verify(r => r.AdicionarAsync(It.IsAny<Cliente>()), Times.Once);
        Assert.NotEqual(Guid.Empty, result);
    }

    [Fact]
    public async Task Handle_DeveLancarExcecao_QuandoCnpjJaExiste()
    {
        var command = new CriaClienteCommand("Empresa Repetida", "11222333000144");
        var cnpjExistente = Cnpj.Criar(command.Cnpj)!;
        var clienteExistente = Cliente.Criar("Outra Empresa", cnpjExistente);

        _mockRepo.Setup(r => r.ObterPorCnpjAsync(cnpjExistente))
                 .ReturnsAsync(clienteExistente);

        await Assert.ThrowsAsync<InvalidOperationException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_DeveLancarExcecao_QuandoCnpjEhInvalido()
    {
        var command = new CriaClienteCommand("Empresa Invalida", "123");

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }

    [Fact]
    public async Task Handle_DeveLancarExcecao_QuandoNomeFantasiaEhVazio()
    {
        var command = new CriaClienteCommand("", "12345678000199");

        await Assert.ThrowsAsync<ArgumentException>(() => _handler.Handle(command, CancellationToken.None));
    }
}

