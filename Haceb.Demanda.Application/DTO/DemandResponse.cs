using Haceb.Demanda.Domain.Entities;
using Haceb.Demanda.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Application.DTO
{
    public class DemandResponse
    {
        public int Id { get; set; }
        public string PlaintiffName { get; set; }
        public string Description { get; set; }
        public int TypeId { get; set; }
        public string DescriptionType { get; set; }
        public DemandPrioritize Prioritize { get; set; }
        public string DescriptionPrioritize { get; set; }
        public int RatingId { get; set; }
        public string DescriptionRating { get; set; }
        public DemandStatus Status { get; set; }
        public string DescriptionStatus { get; set; }
        public DateTime DateRegistry { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }


        public virtual ICollection<DemandHistoryDto> History { get; set; }
    }
}
