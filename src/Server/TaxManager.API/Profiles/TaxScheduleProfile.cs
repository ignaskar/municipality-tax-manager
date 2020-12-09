using AutoMapper;
using TaxManager.API.Dtos;
using TaxManager.API.Dtos.TaxSchedule;
using TaxManager.Core.Entities;

namespace TaxManager.API.Profiles
{
    public class TaxScheduleProfile : Profile
    {
        public TaxScheduleProfile()
        {
            CreateMap<TaxSchedule, TaxScheduleDto>();
            CreateMap<TaxScheduleDto, TaxSchedule>();
            CreateMap<TaxScheduleForCreationDto, TaxSchedule>();
        }
    }
}