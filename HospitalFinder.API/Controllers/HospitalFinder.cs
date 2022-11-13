using HospitalFinder.API.DTOs;
using HospitalFinder.Domain.HospitalData;
using HospitalFinder.Services;
using HospitalFinder.WebEssentials.Coordinate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

        private Hospital _hospitalEntity { get; set; }
        private HospitalUpdate _hospitalUpdateEntity { get; set; }

        private HospitalReadDto _hospitalModel { get; set; }
        private HospitalUpdateCreateDto _hospitalUpdateModel { get; set; }
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
            return Ok();
        }


        #endregion
    }
}
