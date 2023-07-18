using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecruitmentServer.Models
{
    [BsonIgnoreExtraElements]

    public class Company
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]

        public string? Id { get; set; }


        [BsonElement("name")]
        public string Name { get; set; } = String.Empty;

        [BsonElement("description")]
        public string Description { get; set; } = String.Empty;

        [BsonElement("size")]
        public string Size { get; set; } = String.Empty;

        [BsonElement("sellingPoints")]
        public string SellingPoints { get; set; } = String.Empty;

        [BsonElement("annualSales")]
        public string AnnualSales { get; set; } = String.Empty;

        [BsonElement("comment")]
        public string Comment { get; set; } = String.Empty;


    }
}
