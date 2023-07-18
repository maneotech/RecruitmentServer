namespace RecruitmentServer.Models
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string UserCollectionName { get; set; } = String.Empty;

        public string CompanyCollectionName { get; set; } = String.Empty;

        public string OfferCollectionName { get; set; } = String.Empty;
        public string CandidateCollectionName { get; set; } = String.Empty;

        public string LoggerCollectionName { get; set; } = String.Empty;

        
        public string ConnectionString { get; set; } = String.Empty;
        public string DatabaseName { get; set; } = String.Empty;
    }
}
