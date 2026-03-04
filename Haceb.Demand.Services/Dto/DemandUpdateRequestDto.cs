using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demand.Services.Dto
{
    public class DemandUpdateRequestDto
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public int Prioritize { get; set; }
        public int RatingId { get; set; }
        public int Status { get; set; }
        public int UserId { get; set; }
        public string? Comments { get; set; }
    }
}
