namespace ProjetoLojaDeRoupas.Interfaces
{
    public interface IRoupaService
    {
        Task CriarRoupaAsync(Roupa roupa);

        Task<Roupa> LerRoupaAsync(int id);

        Task<IEnumerable<Roupa>> LerTodasAsRoupasAsync(string? nomeBuscar, int? pagina, int? tamanhoPagina, string? ordenacao);

        Task <Roupa> AtualizarRoupaAsync(Roupa roupa);

        Task<bool> ApagarRoupaAsync(int id);

    }
}
