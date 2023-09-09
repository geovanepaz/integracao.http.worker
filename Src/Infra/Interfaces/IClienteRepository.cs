using Infra.Dtos;

namespace Infra.Interfaces
{
    public interface IClienteRepository
    {
        Task<IEnumerable<ClienteDto>> BuscarClientesPendenteIntegracaoAsync();
    }
}
