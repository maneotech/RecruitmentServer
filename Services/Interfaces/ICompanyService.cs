using RecruitmentServer.Models;

namespace RecruitmentServer.Services.Interfaces
{
    public interface ICompanyService
    {
        Company GetById(string id);
        Company GetByName(string companyName);

        Company Create(Company offer);
        void Update(Company offer);
        List<Company> GetCompanies();

        void DeleteById(string id);
    }
}
