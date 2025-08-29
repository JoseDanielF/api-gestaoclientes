using GestaoClientes.Application.Clientes.Commands;
using GestaoClientes.Application.Clientes.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestaoClientes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ClientesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CriarCliente([FromBody] CriaClienteCommand command)
        {
            try
            {
                var clienteId = await _mediator.Send(command);
                return CreatedAtAction(nameof(ObterClientePorId), new { id = clienteId }, command);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> ObterClientePorId(Guid id)
        {
            var query = new ObtemClientePorIdQuery(id);
            var cliente = await _mediator.Send(query);

            if (cliente is null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }
    }
}