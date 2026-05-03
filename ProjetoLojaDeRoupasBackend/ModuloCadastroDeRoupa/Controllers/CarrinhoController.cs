namespace ProjetoLojaDeRoupas.Controllers
{
    [ApiController]
    [Route("api/carrinhos")]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoService _carrinhoService;

        public CarrinhoController(ICarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        [HttpPost] // Adicionar item ao carrinho
        public async Task<IActionResult> AdicionarCarrinho([FromBody] Carrinho carrinho)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _carrinhoService.CriarCarrinhoAsync(carrinho);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }
        }

        [HttpGet] // Obter carrinho do cliente
        public async Task<ActionResult<IEnumerable<Carrinho>>> LerTodosCarrinhos
        (
            [FromQuery] int clienteId
        )
        {
            try
            {
                IEnumerable<Carrinho> todosOsRegistros =
                    await _carrinhoService.LerTodosCarrinhosAsync(clienteId);

                return Ok(todosOsRegistros);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCarrinhoAsync([FromBody] Carrinho carrinho)
        {
            if (carrinho == null)
                return BadRequest(ModelState);

            try
            {
                await _carrinhoService.AtualizarCarrinhoAsync(carrinho);
                return Ok(carrinho);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarrinhoAsync(int id)
        {
            if (id <= 0)
                return BadRequest(ModelState);

            try
            {
                await _carrinhoService.ApagarCarrinhoAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }
        }
    }
}