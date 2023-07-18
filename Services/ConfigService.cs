using RecruitmentServer.Models;
using RecruitmentServer.Services.Interfaces;
using System.Net;

namespace RecruitmentServer.Services
{
    public class ConfigService : IConfigService
    {
        private readonly IHostApplicationLifetime _appLifetime;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ConfigService(IHostApplicationLifetime appLifetime, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _appLifetime = appLifetime;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        public void ChangePortNumber(int portNumber)
        {
            string url = "http://*:" + portNumber + ";";
            _configuration["urls"] = url;
        }

        public Config GetConfigInfo()
        {

            //connected machines
            var session = _httpContextAccessor.HttpContext.Session;
            var userIds = session.Keys.Where(key => key.StartsWith("UserId"));
            int connectedMachines =  userIds.Count();

            connectedMachines = connectedMachines < 0 ? 0 : connectedMachines;


            IPAddress ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress;
            string ipAddressStr = ipAddress.ToString();
            int portNumber = GetPortNumber();

            return new Config
            {
                ConnectedMachines = connectedMachines,
                IPAddress = ipAddressStr,
                Port = portNumber
            };
        }

        public int GetPortNumber()
        {

            string url = _configuration.GetValue<string>("urls");
            int portNumber = int.Parse(url.Replace("http://*:", "").Replace(";", ""));

            return portNumber;
        }

        public void ShutdownServer()
        {
            // Trigger the application to stop
            _appLifetime.StopApplication();
        }
    }
}
