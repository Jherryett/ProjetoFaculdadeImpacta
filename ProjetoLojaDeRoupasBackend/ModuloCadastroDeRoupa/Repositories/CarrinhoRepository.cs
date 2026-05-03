namespace ProjetoLojaDeRoupas.Repositories
{
    public class CarrinhoRepository : ICarrinhoRepository
    {
        private readonly SystemContext _context;

        public CarrinhoRepository(SystemContext context)
        {
            _context = context;
        }

        public async Task<Carrinho> CreateCarrinhoAsync(Carrinho carrinho)
        {
            _context.Carrinhos.Add(carrinho);
            await _context.SaveChangesAsync();
            return carrinho;
        }

        public async Task<Carrinho> ReadCarrinhoAsync(int id)
        {
            return await _context.Carrinhos.FindAsync(id);
        }

        public async Task<IEnumerable<Carrinho>> ReadAllCarrinhosAsync(int clienteId)
        {
            var carrinhos = await _context.Carrinhos
                .Where(c => c.ClienteId == clienteId)
                .Include(c => c.Roupa) // pra trazer os dados da roupa
                .ToListAsync();

            return carrinhos;
        }

        public async Task<Carrinho> UpdateCarrinhoAsync(Carrinho carrinho)
        {
            _context.Carrinhos.Update(carrinho);
            await _context.SaveChangesAsync();
            return carrinho;
        }

        public async Task<bool> DeleteCarrinhoAsync(int id)
        {
            Carrinho carrinhoParaApagar = await _context.Carrinhos.FindAsync(id);
            _context.Carrinhos.Remove(carrinhoParaApagar);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
