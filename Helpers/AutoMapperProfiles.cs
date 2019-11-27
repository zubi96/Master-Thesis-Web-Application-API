using AutoMapper;
using MasterThesisWebApplication.Dtos;
using MasterThesisWebApplication.Models;

namespace MasterThesisWebApplication.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<Category, CategoryToReturnDto>();

            CreateMap<LocationForCreationDto, Location>();
            CreateMap<Location, LocationToReturnDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<PhotoForCreationDto, Photo>();
            CreateMap<Photo, PhotoToReturnDto>();
            CreateMap<MobileUserForLoginDto, MobileUser>();
            CreateMap<MobileUserForRegisterDto, MobileUser>();
        }
    }
}
