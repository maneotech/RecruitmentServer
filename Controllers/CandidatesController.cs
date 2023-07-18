using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
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
    public class CandidatesController : ControllerBase
    {
        private readonly ICandidateService candidateService;
        private readonly ILoggerService _loggerService;
        public CandidatesController(ICandidateService candidateService, ILoggerService loggerService)
        {
            this.candidateService = candidateService;
            _loggerService = loggerService;
        }

        // GET api/<CandidatesController>/5
        [HttpGet("{id}")]
        public ActionResult<Candidate> Get(string id)
        {
            _loggerService.LogInformation(LoggerInformationEnum.CANDIDATE_GET);


            var candidate = candidateService.GetCandidateById(id);

            if (candidate == null)
            {
                return NotFound($"Candidate with Id = {id} not found");
            }

            return candidate;
        }


        // POST api/<CandidatesController>
        [HttpPost]
        public ActionResult Post([FromBody] Candidate candidate)
        {
            _loggerService.LogInformation(LoggerInformationEnum.CANDIDATE_CREATE);

            candidateService.Create(candidate);
            return CreatedAtAction(nameof(Get), new { id = candidate.Id }, candidate);
        }



        // PUT api/<CandidatesController>/5
        [HttpPut]
        public ActionResult Put([FromBody] Candidate candidate)
        {
            _loggerService.LogInformation(LoggerInformationEnum.CANDIDATE_UPDATE);

            var existingCandidate = candidateService.GetCandidateById(candidate.Id!);

            if (existingCandidate == null)
            {
                return NotFound($"Candidate with Id = {candidate.Id} not found");
            }

            if (candidateService.Update(candidate) == true)
            {
                return NoContent();
            }


            return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred");
        }

        [HttpPost]
        [Route("connect")]

        public ActionResult Connect(string candidateId, string offerId)
        {
            _loggerService.LogInformation(LoggerInformationEnum.CANDIDATE_CONNECT);

            candidateService.Connect(candidateId, offerId);
            return NoContent();
        }

        [HttpPost]
        [Route("disconnect")]

        public ActionResult Disconnect(string candidateId, string offerId)
        {
            _loggerService.LogInformation(LoggerInformationEnum.CANDIDATE_DISCONNECT);

            candidateService.Disconnect(candidateId, offerId);
            return NoContent();
        }


        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult> UploadPDFFiles(ICollection<IFormFile> files)
        {
            _loggerService.LogInformation(LoggerInformationEnum.CANDIDATE_UPLOAD);

            List<Candidate> candidates = await candidateService.UploadPDFFiles(files);
            if (candidates.IsNullOrEmpty())
            {
                return NoContent();
            }
            else
            {
                candidateService.AcceptCandidates(candidates);

                List<DuplicateCandidate> duplicates = candidateService.GetDuplicatesCandidates(candidates);
                UploadCandidateResponse response = new UploadCandidateResponse() { Candidates = candidates, Duplicates = duplicates };
                return Ok(response);
            }
        }

        [HttpPut("ignore/{id}")]
        public ActionResult IgnoreCandidate(string id)
        {
            _loggerService.LogInformation(LoggerInformationEnum.CANDIDATE_IGNORE);
            Candidate candidate = candidateService.IgnoreOrNotCandidate(id, true);

            if (candidate == null)
            {
                return NotFound();
            }
            else
            {
                return NoContent();

            }
        }

        [HttpPut("unignore/{id}")]
        public ActionResult UnIgnoreCandidate(string id)
        {
            _loggerService.LogInformation(LoggerInformationEnum.CANDIDATE_UNIGNORE);
            Candidate candidate = candidateService.IgnoreOrNotCandidate(id, false);
            if (candidate == null)
            {
                return NotFound();
            }
            else
            {
                return NoContent();

            }
        }

        /* [HttpPost]
         [Route("accept")]
         public async Task<ActionResult> AcceptCandidates([FromBody] List<Candidate> candidates)
         {
             _loggerService.LogInformation(LoggerInformationEnum.CANDIDATE_ACCEPT);

             candidateService.AcceptCandidates(candidates);
             return NoContent();
         }*/
    }
}
