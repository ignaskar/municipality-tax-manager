using AutoMapper;
using TaxManager.API.Dtos;
using TaxManager.Core.Entities;

namespace TaxManager.API.Profiles
{
    public class MunicipalityProfile : Profile
    {
        public MunicipalityProfile()
        {
            CreateMap<Municipality, MunicipalityDto>();
            CreateMap<MunicipalityDto, Municipality>();
            CreateMap<MunicipalityForCreationDto, Municipality>();
            CreateMap<Municipality, MunicipalityForCreationDto>();
        }
    }
}