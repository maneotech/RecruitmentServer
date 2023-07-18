using RecruitmentServer.Models;
using RecruitmentServer.Services.Interfaces;

namespace RecruitmentServer.Services
{
    public class MatchingService : IMatchingService
    {
        private readonly ICandidateService _candidateService;

        public MatchingService(ICandidateService candidateService)
        {
            _candidateService = candidateService;
        }

        public List<Matching> getMatchingCandidates(string offer)
        {
            List<Matching> matchs = new List<Matching>();

            List<Candidate> candidates = _candidateService.GetAll();

            int i = 90;
            foreach (Candidate candidate in candidates)
            {
                Matching match = new Matching()
                {
                    Score = i,
                    Candidate = candidate
                };
                matchs.Add(match);
                i -= 10;
            }


            return matchs;
        }

    }
}
