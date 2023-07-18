namespace RecruitmentServer.Models
{
    public interface IDatabaseSettings
    {
        string UserCollectionName { get; set; }
        string CompanyCollectionName { get; set; } 
        string OfferCollectionName { get; set; }
        string CandidateCollectionName { get; set; }

        string LoggerCollectionName { get; set; }

        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
