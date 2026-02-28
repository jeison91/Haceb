using Haceb.Demanda.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Domain.Entities
{
    public class DemandEntity
    {
        public int Id { get; private set; }
        public string CaseNumber { get; private set; }
        public string PlaintiffName { get; private set; }
        public string Description { get; private set; }
        public int TypeId { get; private set; }
        public DemandPrioritize Prioritize { get; private set; }
        public int RatingId { get; private set; }
        public DemandStatus Status { get; private set; }
        public DateTime DateRegistry { get; private set; }
        public int UserId { get; private set; }

        //Relaciones
        public DemandTypeEntity TypeEntity { get; private set; }
        public RatingEntity Rating { get; private set; }
        public UserEntity UserEntity { get; private set; }
        public virtual ICollection<DemandAssignmentEntity> AssignmentEntities { get; private set; }
        public virtual ICollection<DemandHistoryEntity> HistoryEntities { get; private set; }
    }
}
