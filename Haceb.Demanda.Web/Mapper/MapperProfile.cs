using AutoMapper;
using Haceb.Demand.Services.Dto;
using Haceb.Demanda.Web.Models;

namespace Haceb.Demanda.Web.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<DemandResponseDto, DemandIndexViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PlaintiffName, opt => opt.MapFrom(src => src.PlaintiffName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.DescriptionType))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.DescriptionRating))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.DateRegistry.ToShortDateString()));

            CreateMap<DemandCreateViewModel, DemandRequestDto>()
                .ForMember(dest => dest.PlaintiffName, opt => opt.MapFrom(src => src.PlaintiffName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.RatingId, opt => opt.MapFrom(src => src.RatingId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<DemandProgressViewModel, DemandUpdateRequestDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Prioritize, opt => opt.MapFrom(src => src.Prioritize))
                .ForMember(dest => dest.RatingId, opt => opt.MapFrom(src => src.RatingId))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.StateId))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments));

            CreateMap<DemandResponseDto, DemandProgressViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.TypeId, opt => opt.MapFrom(src => src.TypeId))
                .ForMember(dest => dest.Prioritize, opt => opt.MapFrom(src => src.Prioritize))
                .ForMember(dest => dest.RatingId, opt => opt.MapFrom(src => src.RatingId))
                .ForMember(dest => dest.StateId, opt => opt.MapFrom(src => src.Status))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Users, opt => opt.Ignore())
                .ForMember(dest => dest.Types, opt => opt.Ignore())
                .ForMember(dest => dest.Ratings, opt => opt.Ignore())
                .ForMember(dest => dest.Priorities, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.Ignore());

            CreateMap<DemandResponseDto, DemandDetailViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.PlaintiffName, opt => opt.MapFrom(src => src.PlaintiffName))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.DescriptionType))
                .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => src.DescriptionRating))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.DescriptionStatus))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.DateRegistry.ToShortDateString()))
                .ForMember(dest => dest.ProgressHistory, opt => opt.MapFrom(src => src.History));

            CreateMap<DemandHistoryResponseDto, DemandProgressDetailViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DemandId))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => src.Comments))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.CreatedDate, opt => opt.MapFrom(src => src.DateRegistry));

        }
    }
}
