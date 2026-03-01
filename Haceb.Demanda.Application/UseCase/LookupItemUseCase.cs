using AutoMapper;
using Haceb.Demanda.Application.DTO;
using Haceb.Demanda.Application.Port;
using Haceb.Demanda.Common.Helper;
using Haceb.Demanda.Domain.Enum;
using Haceb.Demanda.Domain.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Haceb.Demanda.Application.UseCase
{
    public class LookupItemUseCase(IDemandTypeRepository _demandTypeRepository, IRatingRepository _ratingRepository, IMapper _mapper) : ILookupItemPort
    {
        public async Task<List<LookupItemDto>> Get()
        {
            List<LookupItemDto> lookupItemDto = [
                new LookupItemDto() { Type = "DemandType", Items = await GetDemandType() },
                new LookupItemDto() { Type = "Rating", Items = await GetRatings() },
                new LookupItemDto() { Type = "Prioritize", Items = GetPrioritize() },
                new LookupItemDto() { Type = "Status", Items = GetStatus() }
            ];
            return lookupItemDto;
        }

        private async Task<List<Item>> GetDemandType()
        {
            var DemanTypes = await _demandTypeRepository.GetListAsync();
            var mapTypes = _mapper.Map<List<Item>>(DemanTypes);
            return mapTypes;
        }

        private async Task<List<Item>> GetRatings()
        {
            var Ratings = await _ratingRepository.GetListAsync();
            var mapRatings = _mapper.Map<List<Item>>(Ratings);
            return mapRatings;
        }

        private static List<Item> GetPrioritize() =>
            Enum.GetValues(typeof(DemandPrioritize))
                .Cast<DemandPrioritize>()
                .Select(x => new Item
                {
                    Id = (int)x,
                    Description = Helpers.GetDescription(x),
                }).ToList();

        private static List<Item> GetStatus() =>
            Enum.GetValues(typeof(DemandStatus))
                .Cast<DemandStatus>()
                .Select(x => new Item
                {
                    Id = (int)x,
                    Description = Helpers.GetDescription(x),
                }).ToList();
    }
}
