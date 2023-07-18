using RecruitmentServer.Models;
using RecruitmentServer.Models.Enums;

namespace RecruitmentServer.Services.Interfaces
{
    public interface ILoggerService
    {
        LoggerInformation LogInformation(LoggerInformationEnum information, string? loggedUserId = null);
        LoggerInformation LogError(LoggerInformationEnum information);

    }
}
