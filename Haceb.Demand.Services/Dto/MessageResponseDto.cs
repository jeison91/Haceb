using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demand.Services.Dto
{
    public class MessageResponseDto
    {
        public int Status { get; set; }
        public string? Message { get; set; }
        public dynamic? Data { get; set; }
    }
}
