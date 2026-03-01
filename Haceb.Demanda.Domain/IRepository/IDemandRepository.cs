using Haceb.Demanda.Domain.Entities;
using Haceb.Demanda.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Domain.IRepository
{
    public interface IDemandRepository
    {
        Task Create(DemandEntity entity);
        Task Update(DemandEntity entity);
        Task<DemandEntity?> GetByIdAsync(int id);
        Task<List<DemandEntity>> GetByTypeStatusAsync(int idType, DemandStatus status);
        Task<List<DemandEntity>> GetListAsync(int? pageNumber = null, int? pageSize = null);
    }
}
