using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentServer.Models;
using RecruitmentServer.Models.Enums;
using RecruitmentServer.Services;
using RecruitmentServer.Services.Interfaces;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecruitmentServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {

        private readonly IOfferService offerService;
        private readonly ILoggerService _loggerService;

        public OffersController(IOfferService offerService, ILoggerService loggerService)
        {
            this.offerService = offerService;
            _loggerService = loggerService;
        }

        // GET: api/<OffersController>
        [HttpGet]
        public ActionResult<List<Offer>> Get()
        {
            _loggerService.LogInformation(LoggerInformationEnum.OFFER_GETALL);

            return offerService.GetOffers();
        }


        // GET api/<OffersController>/5
        [HttpGet("{id}")]
        public ActionResult<Offer> Get(string id)
        {
            _loggerService.LogInformation(LoggerInformationEnum.OFFER_GET);

            var offer = offerService.GetById(id);
            if (offer == null)
            {
                return NotFound($"Offer with Id = {id} not found");
            }
            return offer;
        }

        // PUT api/<OffersController>/5
        [HttpPut("{id}")]
        public ActionResult Put([FromBody] Offer offer)
        {
            _loggerService.LogInformation(LoggerInformationEnum.OFFER_UPDATE);

            var existingOffer = offerService.GetById(offer.Id!);

            if (existingOffer == null)
            {
                return NotFound($"Offer with Id = {offer.Id} not found");
            }

            offerService.Update(offer);

            return NoContent();

        }
    }
}
