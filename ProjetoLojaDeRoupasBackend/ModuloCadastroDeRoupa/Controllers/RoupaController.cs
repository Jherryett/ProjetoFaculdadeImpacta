namespace ProjetoLojaDeRoupas.Controllers
{
    [ApiController]
    [Route("api/roupas")]
    public class RoupaController : ControllerBase
    {
        private readonly IRoupaService _roupaService;

        public RoupaController(IRoupaService roupaService)
        {
            _roupaService = roupaService;
        }

        [HttpPost] // Adicionar um novo registro
        public async Task<IActionResult> AdicionarRoupa([FromBody] Roupa roupa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _roupaService.CriarRoupaAsync(roupa);
                return StatusCode(StatusCodes.Status201Created);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }

        }

        [HttpGet("{id}")] // Obter um único registro com o ID
        public async Task<IActionResult> ObterRoupa(int id)
        {

            try
            {
                Roupa roupaFinal = await _roupaService.LerRoupaAsync(id);

                return Ok(roupaFinal);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }
        }

        [HttpGet] // Obter uma lista personalizada com filtros
        public async Task<ActionResult<IEnumerable<Roupa>>> LerTodasAsRoupas
            (
            [FromQuery] string? nomeBuscar,
            [FromQuery] int? pagina,
            [FromQuery] int? tamanhoPagina,
            [FromQuery] string? ordenacao
            )
        {
            try
            {
                IEnumerable<Roupa> todosOsRegistros = await _roupaService.LerTodasAsRoupasAsync(nomeBuscar, pagina, tamanhoPagina, ordenacao);
                return Ok(todosOsRegistros);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }
        }


        [HttpPut]
        public async Task<IActionResult> UpdateRoupaAsync([FromBody] Roupa roupa)
        {
            if (roupa == null)
                return BadRequest(ModelState);

            try
            {
                await _roupaService.AtualizarRoupaAsync(roupa);
                return Ok(roupa);
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }
        }


        [HttpDelete("{id}")] // É necessário receber o id na URL, porque quando se trata de apagar/delete, o APS.NET não converte um dado só do corpo, igual ocorre com uma atualização/Put, questão de bidding

        public async Task<IActionResult> DeleteRoupaAsync(int id)
        {
            if (id <= 0)
                return BadRequest(ModelState);

            try
            {
                await _roupaService.ApagarRoupaAsync(id);
                return NoContent();
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex + "Ocorreu um erro interno");
            }



        }
    }
}
