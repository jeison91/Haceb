using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Domain.Entities
{
    public class DemandAssignmentEntity
    {
        public int Id { get; private set; }
        public int DemandId { get; private set; }
        public int UserId { get; private set; }
        public DateTime AssignedAt { get; private set; }

        //Relaciones
        public DemandEntity DemandEntity { get; private set; }
        public UserEntity UserEntity { get; private set; }
    }
}
