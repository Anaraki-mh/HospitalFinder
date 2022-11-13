using HospitalFinder.API.DTOs;
using HospitalFinder.Domain.Enums;
using HospitalFinder.Domain.HospitalData;
using HospitalFinder.Services;
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
    public class HospitalFinder : ControllerBase
    {
        #region Properties and fields

        private IHospitalService _hospitalService { get; }
        private IHospitalUpdateService _hospitalUpdateService { get; }

        private Hospital? _hospitalEntity { get; set; }
        private HospitalUpdate? _hospitalUpdateEntity { get; set; }

        private HospitalReadDto? _hospitalModel { get; set; }
        private HospitalUpdateCreateDto? _hospitalUpdateModel { get; set; }
        #endregion


        #region Constructor

        public HospitalFinder(IHospitalService hospitalService, IHospitalUpdateService hospitalUpdateService)
        {
            _hospitalService = hospitalService;
            _hospitalUpdateService = hospitalUpdateService;

            _hospitalEntity = new Hospital();
            _hospitalUpdateEntity = new HospitalUpdate();

            _hospitalModel = new HospitalReadDto();
            _hospitalUpdateModel = new HospitalUpdateCreateDto();
        }

        #endregion


        #region Methods

        //GET api/Search/{keyword}+{resultsPerPage}+{pageNumebr}
        [HttpGet("Search/{keyword}+{resultsPerPage}+{pageNumebr}")]
        public ActionResult<IEnumerable<HospitalReadDto>> Search(string keyword, int pageNumber = 1, int resultsPerPage = 10)
        {
            if (pageNumber < 1 || resultsPerPage < 1)
                return BadRequest();

            List<HospitalReadDto> modelsList = new List<HospitalReadDto>();
            List<Hospital> entityList = _hospitalService.Search(keyword, pageNumber, resultsPerPage);

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
                    Latitude = Convert.ToDMS(entity.Latitude),
                    Longtitude = Convert.ToDMS(entity.Longtitude),
                    OpenTime = entity.OpenTime,
                    CloseTime = entity.CloseTime,
                    Telephone = entity.Telephone,
                    Website = entity.Website,
                    GoogleMapsLink = entity.GoogleMapsLink,
                });
            }

            return Ok(modelsList);
        }

        //GET api/Search/{keyword}
        [HttpGet("Search/{keyword}")]
        public ActionResult<IEnumerable<HospitalReadDto>> Search(string keyword)
        {
            return Search(keyword, 1, 1);
        }

        //POST api/
        [HttpPost]
        public ActionResult<HospitalReadDto> Create(HospitalUpdateCreateDto model)
        {
            if (ModelState.IsValid)
            {
                _hospitalUpdateEntity = new HospitalUpdate
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
                _hospitalUpdateService.Create(_hospitalUpdateEntity);
                return CreatedAtRoute(nameof(Search), new { keyword = _hospitalUpdateEntity.Name }, _hospitalUpdateEntity);
            }
            return BadRequest(model);

        }

        //PUT api/{id}
        [HttpPut("{id}")]
        public ActionResult Update(int id, HospitalUpdateCreateDto model)
        {
            _hospitalEntity = _hospitalService.FindById(id);

            if (_hospitalEntity == null)
                return NotFound();

            if (ModelState.IsValid)
            {
                _hospitalUpdateEntity = new HospitalUpdate
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
                _hospitalUpdateService.Create(_hospitalUpdateEntity);
            }
            return NoContent();
        }

        [HttpGet("{latitude}+{longtitude}.{numberOfResults?}")]
        public ActionResult<IEnumerable<HospitalReadDto>> FindNearest(double latitude, double longtitude, int numberOfResults = 1)
        {

            List<Hospital> entityList = _hospitalService.FindNearest(latitude, longtitude, numberOfResults);
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
                    Latitude = Convert.ToDMS(entity.Latitude),
                    Longtitude = Convert.ToDMS(entity.Longtitude),
                    OpenTime = entity.OpenTime,
                    CloseTime = entity.CloseTime,
                    Telephone = entity.Telephone,
                    Website = entity.Website,
                    GoogleMapsLink = entity.GoogleMapsLink,
                });
            }
            return Ok(modelList);
        }
        #endregion
    }
}
