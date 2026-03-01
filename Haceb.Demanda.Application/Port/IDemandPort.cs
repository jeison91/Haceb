using Haceb.Demanda.Application.DTO;
using Haceb.Demanda.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Application.Port
{
    public interface IDemandPort
    {
        Task<List<DemandResponse>> GetList(int? pageNumber = null, int? pageSize = null);
        Task<DemandResponse> GetById(int id);
        Task<List<DemandResponse>> GetByFilter(int type, DemandStatus status);
        Task AddDemand(DemandRequest request);
        Task UpdateDemand(DemandUpdateRequest request);
    }
}
