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

              
        public async Task<Roupa> ReadRoupaAsync(int id)
        {
            return await _context.Roupas.FindAsync(id);
        }

        public async Task<IEnumerable<Roupa>> ReadAllRoupasAsync(string? nomeBuscar, int? pagina, int? tamanhoPagina, string? ordenacao)
        {
            // logica de ordenacao dos registros

            var prepararQuery = _context.Roupas.AsQueryable();
            bool ordenacaoAplicada = false;

            if (!string.IsNullOrEmpty(nomeBuscar))
            {
                prepararQuery = prepararQuery.Where(b => b.NomeRoupa.Contains(nomeBuscar));
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

                    case "nomeroupa":
                        prepararQuery = prepararQuery.OrderBy(ord => ord.NomeRoupa);
                        ordenacaoAplicada = true;
                        break;

                    case "valorroupa":
                        prepararQuery = prepararQuery.OrderBy(ord => ord.ValorPeca);
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
            var roupas = await prepararQuery.ToListAsync();

            return roupas;

        }

        public async Task<Roupa> UpdateRoupaAsync(Roupa roupa)
        {
            _context.Roupas.Update(roupa);
            await _context.SaveChangesAsync();
            return roupa;
        }

        public async Task<bool> DeleteRoupaAsync(int id)
        {
            Roupa roupaParaApagar = await _context.Roupas.FindAsync(id);
            _context.Roupas.Remove(roupaParaApagar);
            return await _context.SaveChangesAsync() > 0;
        }



    } // fechamento da classe RoupaRepository


} // fechamento do namespace
