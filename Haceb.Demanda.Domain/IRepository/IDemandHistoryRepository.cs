using Haceb.Demanda.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Domain.IRepository
{
    public interface IDemandHistoryRepository
    {
        Task AddAsync(DemandHistoryEntity history);
        Task<IReadOnlyCollection<DemandHistoryEntity>> GetByDemandIdAsync(int demandId);
    }
}
