namespace ProjetoLojaDeRoupas.Entities
{
    public class Carrinho
    {
        public int Id { get; set; }

        public int ClienteId { get; set; }
        public int RoupaId { get; set; }

        public int Quantidade { get; set; }

        [Precision(18, 2)]
        public decimal ValorUnitario { get; set; }
              
        public Cliente? Cliente { get; set; }
        public Roupa? Roupa { get; set; }

        public Carrinho() { }

        public Carrinho(int clienteId, int roupaId, int quantidade, decimal valorUnitario)
        {
            ClienteId = clienteId;
            RoupaId = roupaId;
            Quantidade = quantidade;
            ValorUnitario = valorUnitario;
        }
    }
}