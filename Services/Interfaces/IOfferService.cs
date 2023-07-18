using RecruitmentServer.Models;

namespace RecruitmentServer.Services.Interfaces
{
    public interface IOfferService
    {
        Offer Create(Offer offer);

        void Update(Offer offer);

        List<Offer> GetOffers();

        Offer GetById(string offerId);

    }
}
