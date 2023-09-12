using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Enum
{
    public enum Integrado : byte
    {
        [Description("Sim")]
        Sim = 1,

        [Description("Não")]
        Nao = 0,
    }
}
