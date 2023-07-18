using MongoDB.Driver;
using RecruitmentServer.Models;
using RecruitmentServer.Services.Interfaces;

namespace RecruitmentServer.Services
{
    public class OfferService: IOfferService
    {
        private readonly IMongoCollection<Offer> _offers;

        public OfferService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _offers = database.GetCollection<Offer>(settings.OfferCollectionName);
        }
        public Offer Create(Offer offer)
        {
            _offers.InsertOne(offer);
            return offer;
        }

        public void Update(Offer offer)
        {
            _offers.ReplaceOne(c => c.Id == offer.Id, offer);
        }

        public List<Offer> GetOffers()
        {
            return _offers.Find(offer => true).ToList();
        }

        public Offer GetById(string offerId)
        {
            return _offers.Find(offer => offer.Id == offerId).FirstOrDefault();
        }

    }
}
