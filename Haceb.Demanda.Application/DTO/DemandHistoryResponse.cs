using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Application.DTO
{
    public class DemandHistoryResponse
    {
        public int DemandId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Action { get; set; }
        public string Comments { get; set; }
        public DateTime DateRegistry { get; set; }
    }
}
