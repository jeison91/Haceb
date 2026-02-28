using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Domain.Entities
{
    public class RatingEntity
    {
        public int Id { get; set; }
        public required string Description { get; set; }
    }
}
