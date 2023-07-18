using RecruitmentServer.Models;

namespace RecruitmentServer.Services.Interfaces
{
    public interface ICandidateService
    {
        Candidate GetCandidateById(string id);

        List<Candidate> GetAll();

        Candidate Create(Candidate candidate);
        bool Update(Candidate candidate);
        void Connect(string candidateId, string offerId);
        void Disconnect(string candidateId, string offerId);

        Task<List<Candidate>> UploadPDFFiles(ICollection<IFormFile> files);
        List<Candidate> AcceptCandidates(List<Candidate> candidates);

        List<DuplicateCandidate> GetDuplicatesCandidates(List<Candidate> candidates);
        Candidate IgnoreOrNotCandidate(string candidateId, bool ignore);
    }
}
