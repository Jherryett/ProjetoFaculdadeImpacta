namespace ProjetoLojaDeRoupas.Services
{
    public class RoupaService : IRoupaService
    {
        private readonly IRoupaRepository _roupaRepository;

        public RoupaService(IRoupaRepository roupaRepository)
        {
            _roupaRepository = roupaRepository;
        }

        public async Task CriarRoupaAsync(Roupa roupa)
        {
            await _roupaRepository.CreateRoupaAsync(roupa);
        }


        public async Task<Roupa> LerRoupaAsync(int id)
        {
            var buscandoRoupa = _roupaRepository.ReadRoupaAsync(id);

            if (buscandoRoupa == null)
            {
                throw new ArgumentNullException("Registro não encontrado");
            }

            else
            {
                return await (buscandoRoupa);
            }
        }

        public async Task<IEnumerable<Roupa>> LerTodasAsRoupasAsync(string? nomeBuscar, int? pagina, int? tamanhoPagina, string? ordenacao)
        {
            if (pagina == null) { pagina = 0; }
            if (tamanhoPagina == null) { tamanhoPagina = 0; }

            var todasAsRoupas = _roupaRepository.ReadAllRoupasAsync(nomeBuscar, pagina, tamanhoPagina, ordenacao);
            return await todasAsRoupas;
        }

        public async Task<Roupa> AtualizarRoupaAsync(Roupa roupa)
        {
            Task<Roupa> retornoOperacao = _roupaRepository.UpdateRoupaAsync(roupa);
            return await retornoOperacao;
        }

        public async Task<bool> ApagarRoupaAsync(int id)
        {
            Task<bool> retornoOperacao = _roupaRepository.DeleteRoupaAsync(id);
            return await retornoOperacao;
        }





    } // fechamento da classe RoupaService

} // Fechamento do namespace
