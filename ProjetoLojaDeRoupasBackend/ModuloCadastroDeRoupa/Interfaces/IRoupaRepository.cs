namespace ProjetoLojaDeRoupas.Interfaces
{
    public interface IRoupaRepository
    {

        Task<Roupa> CreateRoupaAsync(Roupa roupa);

        Task<Roupa> ReadRoupaAsync(int id);

        Task<IEnumerable<Roupa>> ReadAllRoupasAsync(string? nomeBuscar, int? pagina, int? tamanhoPagina, string? ordenacao);

        Task<Roupa> UpdateRoupaAsync(Roupa roupa);
        
        Task<bool> DeleteRoupaAsync(int id);


    }
}
