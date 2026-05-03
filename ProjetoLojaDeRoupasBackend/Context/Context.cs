namespace ProjetoLojaDeRoupas.Context
{
    public class SystemContext : DbContext
    {
        public SystemContext(DbContextOptions<SystemContext> options) : base(options) { }

        public DbSet<Roupa> Roupas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Carrinho> Carrinhos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Carrinho>()
                .HasIndex(c => new { c.ClienteId, c.RoupaId })
                .IsUnique();

            modelBuilder.Entity<Carrinho>()
                .HasCheckConstraint("CK_Carrinhos_Quantidade", "[Quantidade] > 0");

            modelBuilder.Entity<Carrinho>()
                .HasCheckConstraint("CK_Carrinhos_ValorUnitario", "[ValorUnitario] >= 0");
        }

    }
}
