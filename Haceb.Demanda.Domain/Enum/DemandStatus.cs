using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Domain.Enum
{
    public enum DemandStatus
    {
        [Description("Recibido")]
        Received = 1,
        [Description("En revisión")]
        InReview = 2,
        [Description("En proceso")]
        InProcess = 3,
        [Description("En espera")]
        InStop = 4,
        [Description("Cerrado")]
        Closed = 5,
        [Description("Rechazado")]
        Rejected = 6
    }
}
