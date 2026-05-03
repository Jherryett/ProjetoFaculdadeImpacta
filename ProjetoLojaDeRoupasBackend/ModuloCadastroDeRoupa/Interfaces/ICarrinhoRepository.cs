namespace ProjetoLojaDeRoupas.Interfaces
{
    public interface ICarrinhoRepository
    {
        Task<Carrinho> CreateCarrinhoAsync(Carrinho carrinho);

        Task<Carrinho> ReadCarrinhoAsync(int id);

        Task<IEnumerable<Carrinho>> ReadAllCarrinhosAsync(int clienteId);

        Task<Carrinho> UpdateCarrinhoAsync(Carrinho carrinho);

        Task<bool> DeleteCarrinhoAsync(int id);
    }
}