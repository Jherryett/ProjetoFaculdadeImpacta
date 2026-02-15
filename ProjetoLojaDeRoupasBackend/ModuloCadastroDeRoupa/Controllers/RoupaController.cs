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



    }
}
