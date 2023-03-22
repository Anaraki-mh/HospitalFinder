using HospitalFinder.API.DTOs;
using HospitalFinder.Domain.Enums;
using HospitalFinder.Domain.HospitalData;
using HospitalFinder.Services;
using HospitalFinder.WebEssentials;
using HospitalFinder.WebEssentials.Coordinate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using static Microsoft.Extensions.Logging.EventSource.LoggingEventSource;
using Convert = HospitalFinder.WebEssentials.Coordinate.Convert;

namespace HospitalFinder.API.Controllers
{
    [Route("api/")]
    [ApiController]
    public class HospitalFinderController : ControllerBase
    {
        #region Properties and fields

        private IHospitalService _hospitalService { get; }
        private IHospitalUpdateService _hospitalUpdateService { get; }

        #endregion


        #region Constructor

        public HospitalFinderController(IHospitalService hospitalService, IHospitalUpdateService hospitalUpdateService)
        {
            _hospitalService = hospitalService;
            _hospitalUpdateService = hospitalUpdateService;
        }

        #endregion


        #region Methods

        //GET api/Search/{keyword}+{numberOfResults}
        [HttpGet("Search/{keyword}/{numberOfResults?}", Name = "Search")]
        public async Task<ActionResult<IEnumerable<HospitalReadDto>>> SearchAsync(string keyword, int numberOfResults = 1)
        {
            if (numberOfResults < 1)
                return BadRequest();

            List<HospitalReadDto> modelsList = new List<HospitalReadDto>();
            List<Hospital> entityList = await _hospitalService.SearchAsync(keyword, numberOfResults);

            if (entityList.Count == 0)
                return NotFound();

            foreach (Hospital entity in entityList)
            {
                modelsList.Add(new HospitalReadDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    City = entity.City,
                    Country = entity.Country,
                    Address = entity.Address,
                    Latitude = entity.Latitude,
                    Longtitude = entity.Longtitude,
                    LatitudeDMS = Convert.ToDMS(entity.Latitude),
                    LongtitudeDMS = Convert.ToDMS(entity.Longtitude),
                    OpenTime = entity.OpenTime,
                    CloseTime = entity.CloseTime,
                    Telephone = entity.Telephone,
                    Website = entity.Website,
                    GoogleMapsLink = $"https://www.google.com/maps/place/{entity.Latitude}+{entity.Longtitude}",
                });
            }

            return Ok(modelsList);
        }

        //POST api/
        [HttpPost("Create")]
        public async Task<ActionResult<HospitalReadDto>> CreateAsync(HospitalUpdateCreateDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(model);

            var entity = new HospitalUpdate
            {
                OperationType = HospitalUpdateOperation.Add,

                Name = model.Name,
                City = model.City,
                Country = model.Country,
                Address = model.Address,
                Latitude = model.Latitude,
                Longtitude = model.Longtitude,
                OpenTime = model.OpenTime,
                CloseTime = model.CloseTime,
                Telephone = model.Telephone,
                Website = model.Website,
            };
            await _hospitalUpdateService.CreateAsync(entity);
            var readDto = new HospitalUpdateReadDto()
            {
                OperationType = HospitalUpdateOperation.Add,
                Id = entity.Id,
                Name = entity.Name,
                City = entity.City,
                Country = entity.Country,
                Address = entity.Address,
                Latitude = entity.Latitude,
                Longtitude = entity.Longtitude,
                OpenTime = entity.OpenTime,
                CloseTime = entity.CloseTime,
                Telephone = entity.Telephone,
                Website = entity.Website,
            };
            return CreatedAtRoute("Search", new { keyword = readDto.Name }, readDto);
        }

        //PUT api/{id}
        [HttpPut("Update/{id}")]
        public async Task<ActionResult> UpdateAsync(int id, HospitalUpdateCreateDto model)
        {
            var entity = await _hospitalService.FindByIdAsync(id);

            if (entity is null || entity?.Id == 0)
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var hospitalUpdateEntity = new HospitalUpdate
            {
                OperationType = HospitalUpdateOperation.Update,
                HospitalId = id,

                Name = model.Name,
                City = model.City,
                Country = model.Country,
                Address = model.Address,
                Latitude = model.Latitude,
                Longtitude = model.Longtitude,
                OpenTime = model.OpenTime,
                CloseTime = model.CloseTime,
                Telephone = model.Telephone,
                Website = model.Website,
            };
            await _hospitalUpdateService.CreateAsync(hospitalUpdateEntity);
            return NoContent();
        }

        //PUT api/FindNearest/{latitude}/{longtitude}/{numberOfResults}
        [HttpGet("FindNearest/{latitude}/{longtitude}/{numberOfResults?}")]
        public async Task<ActionResult<IEnumerable<HospitalReadDto>>> FindNearesrtAsync(double latitude, double longtitude, int numberOfResults = 1)
        {

            List<Hospital> entityList = await _hospitalService.FindNearestAsync(latitude, longtitude, numberOfResults);
            List<HospitalReadDto> modelList = new List<HospitalReadDto>();

            foreach (Hospital entity in entityList)
            {
                modelList.Add(new HospitalReadDto
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    City = entity.City,
                    Country = entity.Country,
                    Address = entity.Address,
                    Latitude = entity.Latitude,
                    Longtitude = entity.Longtitude,
                    LatitudeDMS = Convert.ToDMS(entity.Latitude),
                    LongtitudeDMS = Convert.ToDMS(entity.Longtitude),
                    OpenTime = entity.OpenTime,
                    CloseTime = entity.CloseTime,
                    Telephone = entity.Telephone,
                    Website = entity.Website,
                    GoogleMapsLink = $"https://www.google.com/maps/place/{entity.Latitude}+{entity.Longtitude}",
                });
            }
            return Ok(modelList);
        }
        #endregion
    }
}
