using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TaxScheduleDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IReadOnlyList<TaxScheduleDto>>> GetTaxSchedulesForMunicipality(string name)
        {
            var municipalitySpec = new MunicipalitiesWithTaxSchedulesSpecification(name);

            var municipality = await _uow.Repository<Municipality>().GetEntityWithSpecification(municipalitySpec);

            if (municipality == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IReadOnlyList<TaxScheduleDto>>(municipality.TaxSchedules));
        }

        [HttpGet("{date}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(TaxScheduleDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<double>> GetTaxRateByDate(string name, DateTime date)
        {
            var municipalitySpec = new MunicipalitiesWithTaxSchedulesSpecification(name);
            
            var municipality = await _uow.Repository<Municipality>().GetEntityWithSpecification(municipalitySpec);

            if (municipality == null)
            {
                return NotFound();
            }

            return Ok(municipality.GetTaxRateForDate(municipality.TaxSchedules, date));
        }
    }
}