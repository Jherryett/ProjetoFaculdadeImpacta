namespace ProjetoLojaDeRoupas.Services
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;
        private readonly IRoupaRepository _roupaRepository;

        public CarrinhoService(ICarrinhoRepository carrinhoRepository, IRoupaRepository roupaRepository)
        {
            _carrinhoRepository = carrinhoRepository;
            _roupaRepository = roupaRepository;
        }

        public async Task CriarCarrinhoAsync(Carrinho carrinho)
        {
            // Verificar se já existe o mesmo produto
            var itensCliente = await _carrinhoRepository.ReadAllCarrinhosAsync(carrinho.ClienteId);

            var itemExistente = itensCliente
                .FirstOrDefault(x => x.RoupaId == carrinho.RoupaId);

            if (itemExistente != null)
            {
                // Somar quantidade
                itemExistente.Quantidade += carrinho.Quantidade;

                await _carrinhoRepository.UpdateCarrinhoAsync(itemExistente);
            }
            else
            {
                // Buscar valor atual da roupa
                var roupa = await _roupaRepository.ReadRoupaAsync(carrinho.RoupaId);

                carrinho.ValorUnitario = roupa.ValorPeca ?? 0;

                await _carrinhoRepository.CreateCarrinhoAsync(carrinho);
            }
        }

        public async Task<Carrinho> LerCarrinhoAsync(int id)
        {
            var buscandoCarrinho = _carrinhoRepository.ReadCarrinhoAsync(id);

            if (buscandoCarrinho == null)
            {
                throw new ArgumentNullException("Registro não encontrado");
            }
            else
            {
                return await buscandoCarrinho;
            }
        }

        public async Task<IEnumerable<Carrinho>> LerTodosCarrinhosAsync(int clienteId)
        {
            var todosCarrinhos = _carrinhoRepository.ReadAllCarrinhosAsync(clienteId);
            return await todosCarrinhos;
        }

        public async Task<Carrinho> AtualizarCarrinhoAsync(Carrinho carrinho)
        {
            Task<Carrinho> retornoOperacao = _carrinhoRepository.UpdateCarrinhoAsync(carrinho);
            return await retornoOperacao;
        }

        public async Task<bool> ApagarCarrinhoAsync(int id)
        {
            Task<bool> retornoOperacao = _carrinhoRepository.DeleteCarrinhoAsync(id);
            return await retornoOperacao;
        }
    }
}