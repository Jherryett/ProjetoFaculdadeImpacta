namespace ProjetoLojaDeRoupas.Interfaces
{
    public interface IRoupaService
    {
        Task CriarRoupaAsync(Roupa roupa);

        // Task<Roupa> LerRoupaAsync(int id);

        //Task<IEnumerable<Roupa>> LerTodosOsBeneficiosAsync(string? nomeBuscar, int? pagina, int? tamanhoPagina, string? ordenacao);

        //Task <bool> AtualizarRoupaAsync(Roupa roupa);

        //Task<bool> ApagarRoupaAsync();

    }
}
