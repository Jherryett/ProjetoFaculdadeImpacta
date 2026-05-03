namespace ProjetoLojaDeRoupas.Interfaces
{
    public interface ICarrinhoService
    {
        Task CriarCarrinhoAsync(Carrinho carrinho);

        Task<Carrinho> LerCarrinhoAsync(int id);

        Task<IEnumerable<Carrinho>> LerTodosCarrinhosAsync(int clienteId);

        Task<Carrinho> AtualizarCarrinhoAsync(Carrinho carrinho);

        Task<bool> ApagarCarrinhoAsync(int id);
    }
}