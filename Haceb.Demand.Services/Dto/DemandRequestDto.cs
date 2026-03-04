using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demand.Services.Dto
{
    public class DemandRequestDto
    {
        public string PlaintiffName { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public int RatingId { get; set; }
        public int UserId { get; set; }
    }
}
