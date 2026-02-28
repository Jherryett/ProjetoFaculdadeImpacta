namespace ProjetoLojaDeRoupas.Entities
{
    public class Roupa
    {
        public int Id { get; set; }
        public string? NomeRoupa { get; set; }
        public string? Tamanho { get; set; }
        public string? NomeFabricante { get; set; }

        [Precision(18, 2)] public decimal? ValorPeca { get; set; }

        [Precision(18, 2)] public decimal? QuantidadeEstoque { get; set; }

        public string? Observacoes { get; set; }


        public Roupa () { }

        public Roupa(string nomeRoupa, string tamanho, string nomeFabricante, decimal valorPeca, decimal quantidadeEstoque, string observacoes)
         {   
            NomeRoupa = nomeRoupa;
            Tamanho = tamanho;
            NomeFabricante = nomeFabricante; 
            ValorPeca = valorPeca;
            QuantidadeEstoque = quantidadeEstoque; 
            Observacoes = observacoes; 
        }


    }
}
