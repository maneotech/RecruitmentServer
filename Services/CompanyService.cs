using MongoDB.Driver;
using RecruitmentServer.Models;
using RecruitmentServer.Services.Interfaces;

namespace RecruitmentServer.Services
{
    public class CompanyService : ICompanyService
    {
        private readonly IMongoCollection<Company> _companies;

        public CompanyService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _companies = database.GetCollection<Company>(settings.CompanyCollectionName);
        }

        public Company Create(Company company)
        {
            _companies.InsertOne(company);
            return company;
        }
        public void Update(Company cpn)
        {
            _companies.ReplaceOne(company => company.Id == cpn.Id, cpn);
        }
        public List<Company> GetCompanies()
        {
            return _companies.Find(cp => true).ToList();

        }
        public Company GetById(string companyId)
        {
            return _companies.Find(cp => cp.Id == companyId).FirstOrDefault();
        }

        public Company GetByName(string companyName)
        {
            return _companies.Find(cp => cp.Name == companyName).FirstOrDefault();
        }

        public void DeleteById(string id)
        {
            _companies.DeleteOne(cp => cp.Id == id);
        }


    }
}
