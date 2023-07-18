namespace RecruitmentServer.Models
{
    public class UploadCandidateResponse
    {
        public List<Candidate> Candidates { get; set; }
        public List<DuplicateCandidate> Duplicates { get; set; }
    }
}
