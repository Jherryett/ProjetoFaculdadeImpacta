namespace ProjetoLojaDeRoupas.Context
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options) { }

        public DbSet<Roupa> Roupas { get; set; }

    }
}
