using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaxManager.API.Dtos;
using TaxManager.API.Helpers;
using TaxManager.Core.Entities;
using TaxManager.Core.Enums;
using TaxManager.Core.Interfaces;
using TaxManager.Core.Specifications;

namespace TaxManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/municipalities/{name}/[controller]")]
    public class TaxSchedulesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public TaxSchedulesController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<TaxScheduleDto>>> GetTaxSchedulesForMunicipality(string name)
        {
            var municipalitySpec = new MunicipalityWithoutTaxSchedulesSpecification(name);

            var municipality = await _uow.Repository<Municipality>().GetEntityWithSpecification(municipalitySpec);

            if (municipality == null)
            {
                return NotFound();
            }
            
            var spec = new TaxSchedulesForMunicipalitySpecification(municipality.Id);
            
            var taxSchedules = await _uow.Repository<TaxSchedule>().ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<TaxScheduleDto>>(taxSchedules));
        }

        [HttpGet("{date}")]
        public async Task<ActionResult<double>> GetTaxRateByDate(string name, DateTime date)
        {
            var municipalitySpec = new MunicipalityWithoutTaxSchedulesSpecification(name);
            
            var municipality = await _uow.Repository<Municipality>().GetEntityWithSpecification(municipalitySpec);

            if (municipality == null)
            {
                return NotFound();
            }
            
            var spec = new TaxSchedulesForMunicipalitySpecification(municipality.Id);
            
            var taxSchedules = await _uow.Repository<TaxSchedule>().ListAsync(spec);

            var containsDailyRate = false;
            var containsWeeklyRate = false; 
            var containsMonthlyRate = false;
            
            foreach (var taxSchedule in taxSchedules)
            {
                if (date.InRange(taxSchedule.StartDate, taxSchedule.EndDate) && taxSchedule.TaxType == TaxType.Daily)
                {
                    containsDailyRate = true;
                }

                if (date.InRange(taxSchedule.StartDate, taxSchedule.EndDate) && taxSchedule.TaxType == TaxType.Weekly)
                {
                    containsWeeklyRate = true;
                }

                if (date.InRange(taxSchedule.StartDate, taxSchedule.EndDate) && taxSchedule.TaxType == TaxType.Monthly)
                {
                    containsMonthlyRate = true;
                }
            }

            return containsDailyRate ? Ok(municipality.DailyTaxRate) :
                containsWeeklyRate ? Ok(municipality.WeeklyTaxRate) :
                !containsMonthlyRate ? Ok(municipality.YearlyTaxRate) :
                Ok(municipality.MonthlyTaxRate);
        }
    }
}