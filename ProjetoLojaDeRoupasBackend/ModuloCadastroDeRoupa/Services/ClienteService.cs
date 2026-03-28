namespace ProjetoLojaDeRoupas.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task CriarClienteAsync(Cliente cliente)
        {
            await _clienteRepository.CreateClienteAsync(cliente);
        }


        public async Task<Cliente> LerClienteAsync(int id)
        {
            var buscandoCliente = _clienteRepository.ReadClienteAsync(id);

            if (buscandoCliente == null)
            {
                throw new ArgumentNullException("Registro não encontrado");
            }

            else
            {
                return await (buscandoCliente);
            }
        }

        public async Task<IEnumerable<Cliente>> LerTodasOsClientesAsync(string? nomeBuscar, int? pagina, int? tamanhoPagina, string? ordenacao)
        {
            if (pagina == null) { pagina = 0; }
            if (tamanhoPagina == null) { tamanhoPagina = 0; }

            var todasAsClientes = _clienteRepository.ReadAllClientesAsync(nomeBuscar, pagina, tamanhoPagina, ordenacao);
            return await todasAsClientes;
        }

        public async Task<Cliente> AtualizarClienteAsync(Cliente cliente)
        {
            Task<Cliente> retornoOperacao = _clienteRepository.UpdateClienteAsync(cliente);
            return await retornoOperacao;
        }

        public async Task<bool> ApagarClienteAsync(int id)
        {
            Task<bool> retornoOperacao = _clienteRepository.DeleteClienteAsync(id);
            return await retornoOperacao;
        }





    } // fechamento da classe ClienteService

} // Fechamento do namespace
