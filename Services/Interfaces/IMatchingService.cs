using RecruitmentServer.Models;

namespace RecruitmentServer.Services.Interfaces
{
    public interface IMatchingService
    {
        List<Matching> getMatchingCandidates(string offer);

    }
}
