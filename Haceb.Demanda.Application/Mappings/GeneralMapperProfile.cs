using AutoMapper;
using Haceb.Demanda.Application.DTO;
using Haceb.Demanda.Domain.Entities;

namespace Haceb.Demanda.Application.Mappings
{
    public class GeneralMapperProfile : Profile
    {
        public GeneralMapperProfile()
        {
            CreateMap<DemandRequest, DemandEntity>()
                .ForMember(dest => dest.PlaintiffName, opt => opt.MapFrom(src => src.PlaintiffName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.RatingId, opt => opt.MapFrom(src => src.RatingId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<UserEntity, UserResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username));

            CreateMap<DemandTypeEntity, Item>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));

            CreateMap<RatingEntity, Item>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
               .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
