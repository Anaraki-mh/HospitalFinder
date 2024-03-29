﻿using HospitalFinder.API.DTOs;
using HospitalFinder.Domain.Enums;
using HospitalFinder.Domain.HospitalData;
using HospitalFinder.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Convert = HospitalFinder.WebEssentials.Coordinate.Convert;

namespace HospitalFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        #region Properties and fields

        private IHospitalService _hospitalService { get; }
        private IHospitalUpdateService _hospitalUpdateService { get; }
        private IAccountService _accountService { get; }

        #endregion


        #region Constructor

        public ManagementController(IHospitalService hospitalService, IHospitalUpdateService hospitalUpdateService, IAccountService accountService)
        {
            _hospitalService = hospitalService;
            _hospitalUpdateService = hospitalUpdateService;
            _accountService = accountService;
        }

        #endregion


        #region Methods

        [HttpGet("GetAllHospitals/{token}/{numberOfResults?}")]
        public async Task<ActionResult<IEnumerable<HospitalReadDto>>> GetAllHospitalsAsync(string token, int numberOfResults = 1)
        {
            bool tokenValidity = await _accountService.IsTokenValid(token);
            if (tokenValidity == false)
                return Unauthorized();

            if (numberOfResults < 1)
                return BadRequest();

            List<HospitalReadDto> modelsList = new List<HospitalReadDto>();
            List<Hospital> entityList = await _hospitalService.ListAsync();
            entityList = entityList.Take(numberOfResults).ToList();

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

        [HttpGet("GetAllHospitalUpdates/{token}/{numberOfResults?}")]
        public async Task<ActionResult<IEnumerable<HospitalUpdateReadDto>>> GetAllHospitalUpdatesAsync(string token, int numberOfResults = 1)
        {
            bool tokenValidity = await _accountService.IsTokenValid(token);
            if (tokenValidity == false)
                return Unauthorized();

            if (numberOfResults < 1)
                return BadRequest();

            List<HospitalUpdateReadDto> modelsList = new List<HospitalUpdateReadDto>();
            List<HospitalUpdate> entityList = await _hospitalUpdateService.ListAsync();
            entityList = entityList.Take(numberOfResults).ToList();

            if (entityList.Count == 0)
                return NotFound();

            foreach (HospitalUpdate entity in entityList)
            {
                modelsList.Add(new HospitalUpdateReadDto
                {
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
                    HospitalId= entity.HospitalId,
                    OperationType = entity.OperationType,
                });
            }

            return Ok(modelsList);
        }

        [HttpGet("GetHospital/{token}/{id}")]
        public async Task<ActionResult<IEnumerable<HospitalReadDto>>> GetHospitalAsync(string token, int id)
        {
            bool tokenValidity = await _accountService.IsTokenValid(token);
            if (tokenValidity == false)
                return Unauthorized();

            var entity = await _hospitalService.FindByIdAsync(id);

            if (entity is null)
                return NotFound();

            var model = new HospitalReadDto
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
            };

            return Ok(model);
        }

        [HttpGet("GetHospitalUpdate/{token}/{id}")]
        public async Task<ActionResult<IEnumerable<HospitalUpdateReadDto>>> GetHospitalUpdateAsync(string token, int id)
        {
            bool tokenValidity = await _accountService.IsTokenValid(token);
            if (tokenValidity == false)
                return Unauthorized();

            var entity = await _hospitalUpdateService.FindByIdAsync(id);

            if (entity is null)
                return NotFound();

            var model = new HospitalUpdateReadDto
            {
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
                HospitalId = entity.HospitalId,
                OperationType = entity.OperationType,
            };

            return Ok(model);
        }

        [HttpPost("CreateHospital/{token}")]
        public async Task<ActionResult<HospitalReadDto>> CreateAsync(string token, HospitalCreateDto model)
        {
            bool tokenValidity = await _accountService.IsTokenValid(token);
            if (tokenValidity == false)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest(model);

            var entity = new Hospital
            {
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
                CreateDateTime = DateTime.UtcNow,
                UpdateDateTime = DateTime.UtcNow,
            };
            await _hospitalService.CreateAsync(entity);
            var readDto = new HospitalReadDto()
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
            };
            return CreatedAtAction("GetHospital", new { token = token , id = readDto.Id }, readDto);
        }

        [HttpPut("UpdateHospital/{token}/{id}")]
        public async Task<ActionResult> UpdateAsync(string token, int id, HospitalCreateDto model)
        {
            bool tokenValidity = await _accountService.IsTokenValid(token);
            if (tokenValidity == false)
                return Unauthorized();

            if (!ModelState.IsValid)
                return BadRequest();

            var entity = await _hospitalService.FindByIdAsync(id);

            if (entity is null || entity?.Id == 0)
                return NotFound();

            entity.Name = model.Name;
            entity.City = model.City;
            entity.Country = model.Country;
            entity.Address = model.Address;
            entity.Latitude = model.Latitude;
            entity.Longtitude = model.Longtitude;
            entity.OpenTime = model.OpenTime;
            entity.CloseTime = model.CloseTime;
            entity.Telephone = model.Telephone;
            entity.Website = model.Website;
            entity.UpdateDateTime = DateTime.UtcNow;

            await _hospitalService.UpdateAsync(entity);
            return NoContent();
        }

        [HttpDelete("DeleteHospital/{token}/{id}")]
        public async Task<ActionResult> DeleteHospitalAsync(string token, int id)
        {
            var entity = await _hospitalService.FindByIdAsync(id);

            if (entity is null || entity?.Id == 0)
                return NotFound();

            await _hospitalService.DeleteAsync(id);
            return NoContent();
        }

        [HttpDelete("DeleteHospitalUpdate/{token}/{id}")]
        public async Task<ActionResult> DeleteHospitalUpdateAsync(string token, int id)
        {
            var entity = await _hospitalUpdateService.FindByIdAsync(id);

            if (entity is null || entity?.Id == 0)
                return NotFound();

            await _hospitalUpdateService.DeleteAsync(id);
            return NoContent();
        }

        #endregion
    }

}
