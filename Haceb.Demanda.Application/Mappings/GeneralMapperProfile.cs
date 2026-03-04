using AutoMapper;
using Haceb.Demanda.Application.DTO;
using Haceb.Demanda.Common.Helper;
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

            CreateMap<DemandEntity, DemandResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PlaintiffName, opt => opt.MapFrom(src => src.PlaintiffName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.DescriptionType, opt => opt.MapFrom(src => src.TypeEntity.Description))
                .ForMember(dest => dest.Prioritize, opt => opt.MapFrom(src => src.Prioritize))
                .ForMember(dest => dest.DescriptionPrioritize, opt => opt.MapFrom(src => Helpers.GetDescription(src.Prioritize)))
                .ForMember(dest => dest.RatingId, opt => opt.MapFrom(src => src.RatingId))
                .ForMember(dest => dest.DescriptionRating, opt => opt.MapFrom(src => src.Rating.Description))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.DescriptionStatus, opt => opt.MapFrom(src => Helpers.GetDescription(src.Status)))
                .ForMember(dest => dest.DateRegistry, opt => opt.MapFrom(src => src.DateRegistry))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserEntity.Username))
                .ForMember(dest => dest.History, opt => opt.MapFrom(src => src.HistoryEntities));

            CreateMap<DemandHistoryEntity, DemandHistoryResponse>()
                .ForMember(dest => dest.DemandId, opt => opt.MapFrom(src => src.DemandId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserEntity.Username))
                .ForMember(dest => dest.Action, opt => opt.MapFrom(src => src.Action))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.DateRegistry, opt => opt.MapFrom(src => src.DateRegistry));

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
