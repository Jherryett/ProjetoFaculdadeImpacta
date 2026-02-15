namespace ProjetoLojaDeRoupas.Repositories
{
    public class RoupaRepository : IRoupaRepository
    {
        private readonly SystemContext _context;

        public RoupaRepository(SystemContext context)
        {
            _context = context;
        }

        public async Task<Roupa> CreateRoupaAsync(Roupa roupa)
        {
            _context.Roupas.Add(roupa);
            await _context.SaveChangesAsync();
            return roupa;
        }




    }
}
