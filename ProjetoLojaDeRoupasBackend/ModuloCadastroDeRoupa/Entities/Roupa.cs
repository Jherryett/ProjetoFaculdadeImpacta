namespace ProjetoLojaDeRoupas.Entities
{
    public class Roupa
    {
        public int? IdRoupa { get; set; }
        public int? IdCompra {  get; set; }
        public string? NomeRoupa { get; set; }
        public string? NomeFabricante { get; set; }

        [Precision(18, 2)] public decimal? ValorPeca { get; set; }

        [Precision(18, 2)] public decimal? QuantidadeEstoque { get; set; }

        public string? Observacoes { get; set; }


        public Roupa () { }

        public Roupa (int id, int compra, string nomeRoupa, string nomeFabricante, decimal valorPeca, decimal quantidadeEstoque, string observacoes)
         {  IdRoupa = id; 
            IdCompra = compra; 
            NomeRoupa = nomeRoupa;
            NomeFabricante = nomeFabricante; 
            ValorPeca = valorPeca;
            QuantidadeEstoque = quantidadeEstoque; 
            Observacoes = observacoes; 
        }


    }
}
