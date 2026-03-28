namespace ProjetoLojaDeRoupas.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string? NomeCliente { get; set; }
        public DateOnly? DataNascimento { get; set; }
        public string? Cpf { get; set; }
        public string? NumeroTelefone { get; set; }

        public string? EmailCliente { get; set; }

        public string? Observacoes { get; set; }


        public Cliente () { }

        public Cliente(string nomeCliente, string cpf, string numeroTelefone, DateOnly dataNascimento, string observacoes, string emailCliente)
         {   
            NomeCliente = nomeCliente;
            DataNascimento = dataNascimento;
            Cpf = cpf;
            NumeroTelefone = numeroTelefone;
            EmailCliente = emailCliente;
            Observacoes = observacoes; 
        }


    }
}
