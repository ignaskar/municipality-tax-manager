using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TaxManager.API.Dtos.Municipality;
using TaxManager.API.Errors;
using TaxManager.Core.Entities;
using TaxManager.Core.Interfaces;
using TaxManager.Core.Specifications;
using System.Text.Json;

namespace TaxManager.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class MunicipalitiesController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<MunicipalitiesController> _logger;

        public MunicipalitiesController(IUnitOfWork uow, IMapper mapper, ILogger<MunicipalitiesController> logger)
        {
            _uow = uow ?? throw new ArgumentNullException(nameof(uow));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(MunicipalityDto), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MunicipalityDto>> GetMunicipalityById(int id)
        {
            var spec = new MunicipalitiesWithTaxSchedulesSpecification(id);
            
            var municipality = await _uow.Repository<Municipality>().GetEntityWithSpecification(spec);

            if (municipality == null)
            {
                return NotFound(new ApiResponse(404));
            }
            
            return Ok(_mapper.Map<MunicipalityDto>(municipality));
        }

        [HttpPost]
        [ProducesResponseType(typeof(MunicipalityDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<MunicipalityDto>> CreateMunicipality(MunicipalityForCreationDto municipalityToCreate)
        {
            var municipalityEntity = _mapper.Map<Municipality>(municipalityToCreate);
            
            _uow.Repository<Municipality>().Add(municipalityEntity);
            await _uow.Complete();
            _uow.Dispose();

            var municipalityToReturn = _mapper.Map<MunicipalityDto>(municipalityEntity);
            return CreatedAtRoute("GetById", new {id = municipalityToReturn.Id}, municipalityToReturn);
        }

        [HttpPost("upload", Name = "UploadMunicipalities")]
        public async Task<ActionResult> CreateMunicipalitiesFromJson(UploadFile file)
        {
            var municipalitiesToAdd = DeserializeUploadedFile(file.FileContent);
            
            foreach (var municipality in municipalitiesToAdd)
            {
                var municipalityEntity = _mapper.Map<Municipality>(municipality);
                _uow.Repository<Municipality>().Add(municipalityEntity);

                try
                {
                    await _uow.Complete();
                }
                catch (Exception ex)
                {
                    return new ObjectResult(new ApiResponse(400,
                        "Incorrectly formatted JSON file. Please verify the formatting before uploading and try again."));
                }
            }

            return Ok(new ApiResponse(201, $"Successfully created {municipalitiesToAdd.Count} entries"));
        }

        private static IReadOnlyList<MunicipalityForCreationDto> DeserializeUploadedFile(byte[] buffer)
        {
            return JsonSerializer.Deserialize<IReadOnlyList<MunicipalityForCreationDto>>(System.Text.Encoding.UTF8.GetString(buffer));
        }

    }
}