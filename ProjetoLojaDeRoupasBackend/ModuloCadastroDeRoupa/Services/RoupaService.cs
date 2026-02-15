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
















    }
}
