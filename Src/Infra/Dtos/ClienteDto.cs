using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Dtos
{
    public record ClienteDto
    {
        public required int IdIntegracao { get; set; }
        public required string NomeCompleto { get; set; }
        public short Idade { get; set; }
        public char Sexo { get; set; }
        public required string CPF { get; set; }
        public required string Cidade { get; set; }
        public required string UF { get; set; }
        public required string Profissao { get; set; }


    }
}
