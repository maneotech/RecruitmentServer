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
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly ILoggerService _loggerService;

        public UsersController(IUserService userService, ILoggerService loggerService)
        {
            this.userService = userService;
            _loggerService = loggerService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        [Route("users")]

        public ActionResult<List<User>> Get()
        {
            _loggerService.LogInformation(LoggerInformationEnum.USER_GETALL);
            return Ok(userService.GetUsers());
        }

        // GET: api/<UsersController>
        [HttpGet]
        [Route("managers")]
        public ActionResult<List<User>> GetManagers()
        {
            _loggerService.LogInformation(LoggerInformationEnum.USER_GETMANAGERS);
            return Ok(userService.GetManagers());
        }


        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<User> Get(string id)
        {
            _loggerService.LogInformation(LoggerInformationEnum.USER_GET);

            var user = userService.GetUserById(id);
            if (user == null)
            {
                return NotFound($"User with Id = {id} not found");
            }
            return user;
        }

        // POST api/<UsersController>
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
            _loggerService.LogInformation(LoggerInformationEnum.USER_CREATE);

            var existingUser = userService.GetUserByUsername(user.Username);

            if (existingUser != null)
            {
                return Conflict("Username already exist");
            }
  
            user.Password = UtilService.HashPassword(user.Password);

            userService.Create(user);
            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        // PUT api/<UsersController>
        [HttpPut]
        public ActionResult Put([FromBody] User user)
        {
            _loggerService.LogInformation(LoggerInformationEnum.USER_UPDATE);

            var existingUser = userService.GetUserByUsername(user.Username!);

            if (existingUser == null)
            {
                return NotFound($"User with Id = {user.Username} not found");
            }

            user.Id = existingUser.Id;

            if (user.Password.IsNullOrEmpty())
            {
                user.Password = existingUser.Password;
            }
            else
            {
                user.Password = UtilService.HashPassword(user.Password);
            }

            userService.Update(user);

            return NoContent();
        }

        // DELETE api/<UsersController>
        [HttpDelete]
        public ActionResult Delete([FromBody]string username)
        {
            _loggerService.LogInformation(LoggerInformationEnum.USER_REMOVE);

            var user = userService.GetUserByUsername(username);

            if ( user == null )
            {
                return NotFound($"User with username = {username} not found");
            }

            userService.Remove(user.Id!);

            return Ok($"User with username = {username} deleted");
        }
    }
}
