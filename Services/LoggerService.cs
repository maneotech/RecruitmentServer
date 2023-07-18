using MongoDB.Driver;
using RecruitmentServer.Models;
using RecruitmentServer.Models.Enums;
using RecruitmentServer.Services.Interfaces;
using System.Security.Claims;

namespace RecruitmentServer.Services
{
    public class LoggerService: ILoggerService
    {
        private readonly IMongoCollection<LoggerInformation> _loggers;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoggerService(IDatabaseSettings settings, IMongoClient mongoClient, IHttpContextAccessor httpContextAccessor)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _loggers = database.GetCollection<LoggerInformation>(settings.LoggerCollectionName);
            _httpContextAccessor = httpContextAccessor;

        }

        public LoggerInformation LogInformation(LoggerInformationEnum information, string? loggedUserId)
        {
            var userId = loggedUserId != null ? loggedUserId : _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
            LoggerInformation logs = new LoggerInformation
            {
                FromUserId = userId,
                IsError = false,
                Operation = LoggerInformationEnum.USER_GETALL
            };

            _loggers.InsertOne(logs);
            return logs;
        }

        public LoggerInformation LogError(LoggerInformationEnum information)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

            LoggerInformation logs = new LoggerInformation
            {
                FromUserId = userId,
                IsError = true,
                Operation = information
            };

            _loggers.InsertOne(logs);
            return logs;
        }

    }
}
