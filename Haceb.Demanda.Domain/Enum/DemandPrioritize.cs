using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Domain.Enum
{
    public enum DemandPrioritize
    {
        [Description("Ninguna")]
        None = 0,
        [Description("Bajo")]
        Low = 1,
        [Description("Medio")]
        Medium = 2,
        [Description("Alto")]
        High = 3,
        [Description("Critico")]
        Critical = 4
    }
}
