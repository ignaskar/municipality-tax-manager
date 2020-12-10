using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaxManager.API.Dtos.Municipality;
using TaxManager.API.Dtos.TaxSchedule;
using TaxManager.API.Errors;
using TaxManager.API.Helpers;
using TaxManager.Core.Entities;
using TaxManager.Core.Interfaces;
using TaxManager.Core.Specifications;

namespace TaxManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/municipalities/{municipalityId}/[controller]")]
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TaxScheduleDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<TaxScheduleDto>>> GetTaxSchedulesForMunicipality(int municipalityId)
        {
            var municipalitySpec = new MunicipalitiesWithTaxSchedulesSpecification(municipalityId);

            var municipality = await _uow.Repository<Municipality>().GetEntityWithSpecification(municipalitySpec);

            if (municipality == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper.Map<IReadOnlyList<TaxScheduleDto>>(municipality.TaxSchedules));
        }

        [HttpGet("{taxScheduleId}", Name = "GetTaxScheduleById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MunicipalityDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TaxScheduleDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TaxScheduleDto>> GetTaxScheduleForMunicipality(int municipalityId, int taxScheduleId)
        {
            var spec = new MunicipalitiesWithTaxSchedulesSpecification(municipalityId);

            var municipality = await _uow.Repository<Municipality>().GetEntityWithSpecification(spec);

            if (municipality == null)
            {
                return NotFound(new ApiResponse(404));
            }

            var taxScheduleSpec = new TaxSchedulesForMunicipalitySpecification(taxScheduleId);

            var taxSchedule = await _uow.Repository<TaxSchedule>().GetEntityWithSpecification(taxScheduleSpec);

            if (taxSchedule == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(_mapper.Map<TaxScheduleDto>(taxSchedule));
        }

        [HttpGet("GetTaxRate/{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MunicipalityDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<double>> GetTaxRateByDate(int municipalityId, DateTime date)
        {
            var municipalitySpec = new MunicipalitiesWithTaxSchedulesSpecification(municipalityId);
            
            var municipality = await _uow.Repository<Municipality>().GetEntityWithSpecification(municipalitySpec);

            if (municipality == null)
            {
                return NotFound(new ApiResponse(404));
            }

            return Ok(municipality.GetTaxRateForDate(municipality.TaxSchedules, date));
        }

        [HttpPost]
        [ProducesResponseType(typeof(MunicipalityDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(TaxScheduleDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<TaxScheduleDto>> CreateTaxScheduleForMunicipality(int municipalityId, TaxScheduleForCreationDto taxScheduleForCreation)
        {
            var spec = new MunicipalitiesWithTaxSchedulesSpecification(municipalityId);

            var municipality = await _uow.Repository<Municipality>().GetEntityWithSpecification(spec);

            if (municipality == null)
            {
                return NotFound(new ApiResponse(404));
            }
            
            var taxScheduleEntity = _mapper.Map<TaxSchedule>(taxScheduleForCreation);
            taxScheduleEntity.MunicipalityId = municipalityId;

            _uow.Repository<TaxSchedule>().Add(taxScheduleEntity);
            await _uow.Complete();

            var taxScheduleToReturn = _mapper.Map<TaxScheduleDto>(taxScheduleEntity);
            return CreatedAtRoute("GetTaxScheduleById",
                new {municipalityId = municipality.Id, taxScheduleId = taxScheduleToReturn.Id}, taxScheduleToReturn);
        }
    }
}