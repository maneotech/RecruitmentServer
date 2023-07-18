using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecruitmentServer.Models.Enums;
using RecruitmentServer.Models;
using RecruitmentServer.Services;
using RecruitmentServer.Services.Interfaces;

namespace RecruitmentServer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]

    public class ConfigController : ControllerBase
    {
        private readonly IConfigService _configService;
        private readonly ILoggerService _loggerService;

        public ConfigController(ILoggerService loggerService, IConfigService configService)
        {
            _loggerService = loggerService;
            _configService = configService;
        }


        [HttpGet]
        [Route("info")]
        public ActionResult GetInfo()
        {
            _loggerService.LogInformation(LoggerInformationEnum.CONFIG_GET_INFO);
            Config config = _configService.GetConfigInfo();
            return Ok(config);
        }

        [HttpGet]
        [Route("shutdown")]
        public ActionResult Shutdown()
        {
            _loggerService.LogInformation(LoggerInformationEnum.CONFIG_SHUTDOWN);
            _configService.ShutdownServer();
            return Ok();
        }

        [HttpPut]
        [Route("port")]
        public ActionResult ChangePortNumber([FromBody] int portNumber)
        {
            _loggerService.LogInformation(LoggerInformationEnum.CONFIG_CHANGE_PORT);
            _configService.ChangePortNumber(portNumber);
            return NoContent();
        }
    }
}
