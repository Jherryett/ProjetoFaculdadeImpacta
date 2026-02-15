namespace ProjetoLojaDeRoupas.Interfaces
{
    public interface IRoupaRepository
    {

        Task<Roupa> CreateRoupaAsync(Roupa roupa);
        Task<Roupa> ReadRoupaAsync(int id);
        Task<IEnumerable<Roupa>> ReadAllRoupaAsync();
        Task<Roupa> UpdateRoupaAsync(Roupa roupa);
        Task<bool> DeleteRoupaAsync(int id);


    }
}
