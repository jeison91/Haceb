using Haceb.Demand.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demand.Services.IPort
{
    public interface IDemandServices
    {
        Task<List<LookupItemDto>> GetLookup();
        Task<DemandResponseDto?> GetIdDemand(int id);
        Task<List<DemandResponseDto>> GetListDemand();
        Task<List<UserResponseDto>> GetListUser();
        Task<bool> PostCreateDemand(DemandRequestDto demandRequest);
        Task<bool> PutProgressDemand(DemandUpdateRequestDto demandRequest);
    }
}
