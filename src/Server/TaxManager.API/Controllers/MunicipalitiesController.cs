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
    [Route("api/v1/[controller]")]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public MunicipalitiesController(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<MunicipalityDto>>> GetAllMunicipalities(
            [FromQuery] MunicipalitySpecParams parameters)
        {
            var spec = new MunicipalitiesWithTaxSchedulesSpecification(parameters);
            
            var municipalities = await _uow.Repository<Municipality>().ListAsync(spec);
            
            return Ok(_mapper.Map<IReadOnlyList<MunicipalityDto>>(municipalities));
        }

        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<MunicipalityDto>> GetMunicipalityById(int id)
        {
            var spec = new MunicipalitiesWithTaxSchedulesSpecification(id);
            
            var municipality = await _uow.Repository<Municipality>().GetEntityWithSpecification(spec);

            if (municipality == null)
            {
                return NotFound();
            }
            
            return Ok(_mapper.Map<MunicipalityDto>(municipality));
        }
    }
}