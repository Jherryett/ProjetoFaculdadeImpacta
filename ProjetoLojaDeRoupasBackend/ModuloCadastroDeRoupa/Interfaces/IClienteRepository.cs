namespace ProjetoLojaDeRoupas.Interfaces
{
    public interface IClienteRepository
    {

        Task<Cliente> CreateClienteAsync(Cliente cliente);

        Task<Cliente> ReadClienteAsync(int id);

        Task<IEnumerable<Cliente>> ReadAllClientesAsync(string? nomeBuscar, int? pagina, int? tamanhoPagina, string? ordenacao);

        Task<Cliente> UpdateClienteAsync(Cliente cliente);
        
        Task<bool> DeleteClienteAsync(int id);


    }
}
