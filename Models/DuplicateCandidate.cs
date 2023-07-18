namespace RecruitmentServer.Models
{
    public class DuplicateCandidate
    {
        public Candidate Candidate { get; set; }

        public Candidate Duplicate { get; set; }
    }
}
