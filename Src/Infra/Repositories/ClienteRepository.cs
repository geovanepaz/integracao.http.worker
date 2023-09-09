using Infra.Dtos;
using Infra.Interfaces;

namespace Infra.Repositories
{

    public class ClienteRepository : IClienteRepository
    {
        private readonly IDapperUtil _dapperUtil;

        public ClienteRepository(IDapperUtil dapperUtil)
        {
            _dapperUtil = dapperUtil;
        }

        public async Task<IEnumerable<ClienteDto>> BuscarClientesPendenteIntegracaoAsync()
        {
            string query = @"
                                select 
                                    id as IdIntegracao,
	                                nome_completo as NomeCompleto, 
	                                idade as Idade, 
	                                sexo as Sexo, 
	                                cpf as CPF, 
	                                cidade as Cidade, 
	                                uf as UF, 
	                                profissao as Profissao  
                                from 
	                                integracao_cliente
                                where 
	                                integrado = 0
                            ";
            return await _dapperUtil.RunQueryAsync<ClienteDto>(query);
        }
    }
}
