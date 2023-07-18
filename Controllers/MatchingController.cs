using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentServer.Models.Enums;
using RecruitmentServer.Models;
using RecruitmentServer.Services;
using RecruitmentServer.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RecruitmentServer.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class MatchingController : ControllerBase
    {

        private readonly ILoggerService _loggerService;
        private readonly IMatchingService _matchingService;


        public MatchingController(ILoggerService loggerService, IMatchingService matchingService)
        {
            _loggerService = loggerService;
            _matchingService = matchingService;
        }

        // POST api/<MatchingControllers>
        [HttpPost]
        [Route("offer")]
        public ActionResult Post([FromBody] string offer)
        {
            _loggerService.LogInformation(LoggerInformationEnum.MATCHING_OFFER);

            List<Matching> candidates = _matchingService.getMatchingCandidates(offer);
            return Ok(candidates);
        }
    }
}
