using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Application.DTO
{
    public class LookupItemDto
    {
        public string Type { get; set; }
        public List<Item> Items { get; set; }
    }

    public class Item
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
