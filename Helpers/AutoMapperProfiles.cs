using AutoMapper;
using MasterThesisWebApplication.Dtos;
using MasterThesisWebApplication.Models;

namespace MasterThesisWebApplication.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RegionForCreationDto, Region>();
            CreateMap<Region, RegionToReturnDto>();
            CreateMap<Admin, AdminToReturnDto>();
            CreateMap<ModeratorForCreationDto, Admin>();
        }
    }
}
