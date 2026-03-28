namespace ProjetoLojaDeRoupas.Repositories
{
    public class ClienteRepository : IClienteRepository
    {
        private readonly SystemContext _context;

        public ClienteRepository(SystemContext context)
        {
            _context = context;
        }

        public async Task<Cliente> CreateClienteAsync(Cliente cliente)
        {
            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

              
        public async Task<Cliente> ReadClienteAsync(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<IEnumerable<Cliente>> ReadAllClientesAsync(string? nomeBuscar, int? pagina, int? tamanhoPagina, string? ordenacao)
        {
            // logica de ordenacao dos registros

            var prepararQuery = _context.Clientes.AsQueryable();
            bool ordenacaoAplicada = false;

            if (!string.IsNullOrEmpty(nomeBuscar))
            {
                prepararQuery = prepararQuery.Where(b => b.NomeCliente.Contains(nomeBuscar));
            }


            if (!string.IsNullOrEmpty(ordenacao))
            {
                string ordenacaoMinuscula = ordenacao.Trim().ToLowerInvariant();

                switch (ordenacaoMinuscula)
                {
                    case "id":
                        prepararQuery = prepararQuery.OrderBy(ord => ord.Id);
                        ordenacaoAplicada = true;
                        break;

                    case "nomecliente":
                        prepararQuery = prepararQuery.OrderBy(ord => ord.NomeCliente);
                        ordenacaoAplicada = true;
                        break;

                    case "valorcliente":
                        prepararQuery = prepararQuery.OrderBy(ord => ord.DataNascimento);
                        ordenacaoAplicada = true;
                        break;

                    case "descritivo":
                        prepararQuery = prepararQuery.OrderBy(ord => ord.Observacoes);
                        ordenacaoAplicada = true;
                        break;

                    default:
                        break;

                }

            }


            if (!ordenacaoAplicada)
            {
                prepararQuery = prepararQuery.OrderBy(ord => ord.Id);
            }


            // logica de paginação dos registros
            if (tamanhoPagina.Value > 0 && tamanhoPagina.HasValue)
            {
                var ordenacaoPorPagina = pagina.Value * tamanhoPagina.Value;

                // aplicação da ordenação na query
                prepararQuery = prepararQuery.Skip(ordenacaoPorPagina).Take(tamanhoPagina.Value);
            }

            // uso do query no banco
            var clientes = await prepararQuery.ToListAsync();

            return clientes;

        }

        public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
        {
            _context.Clientes.Update(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            Cliente clienteParaApagar = await _context.Clientes.FindAsync(id);
            _context.Clientes.Remove(clienteParaApagar);
            return await _context.SaveChangesAsync() > 0;
        }



    } // fechamento da classe ClienteRepository


} // fechamento do namespace
