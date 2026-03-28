namespace ProjetoLojaDeRoupas.Interfaces
{
    public interface IClienteService
    {
        Task CriarClienteAsync(Cliente cliente);

        Task<Cliente> LerClienteAsync(int id);

        Task<IEnumerable<Cliente>> LerTodasOsClientesAsync(string? nomeBuscar, int? pagina, int? tamanhoPagina, string? ordenacao);

        Task <Cliente> AtualizarClienteAsync(Cliente cliente);

        Task<bool> ApagarClienteAsync(int id);

    }
}
