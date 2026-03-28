namespace ProjetoLojaDeRoupas.Controllers
{
    [ApiController]
    [Route("api/clientes")]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpPost] // Adicionar um novo registro
        public async Task<IActionResult> AdicionarCliente([FromBody] Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _clienteService.CriarClienteAsync(cliente);
                return StatusCode(StatusCodes.Status201Created);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }

        }

        [HttpGet("{id}")] // Obter um único registro com o ID
        public async Task<IActionResult> ObterCliente(int id)
        {

            try
            {
                Cliente clienteFinal = await _clienteService.LerClienteAsync(id);

                return Ok(clienteFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }
        }

        [HttpGet] // Obter uma lista personalizada com filtros
        public async Task<ActionResult<IEnumerable<Cliente>>> LerTodasOsClientes
            (
            [FromQuery] string? nomeBuscar,
            [FromQuery] int? pagina,
            [FromQuery] int? tamanhoPagina,
            [FromQuery] string? ordenacao
            )
        {
            try
            {
                IEnumerable<Cliente> todosOsRegistros = await _clienteService.LerTodasOsClientesAsync(nomeBuscar, pagina, tamanhoPagina, ordenacao);
                return Ok(todosOsRegistros);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateClienteAsync([FromBody] Cliente cliente)
        {
            if (cliente == null)
                return BadRequest(ModelState);

            try
            {
                await _clienteService.AtualizarClienteAsync(cliente);
                return Ok(cliente);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }
        }


        [HttpDelete("{id}")] // É necessário receber o id na URL, porque quando se trata de apagar/delete, o APS.NET não converte um dado só do corpo, igual ocorre com uma atualização/Put, questão de bidding

        public async Task<IActionResult> DeleteClienteAsync(int id)
        {
            if (id <= 0)
                return BadRequest(ModelState);

            try
            {
                await _clienteService.ApagarClienteAsync(id);
                return NoContent();
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }



        }
    }
}
