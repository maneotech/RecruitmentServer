using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RecruitmentServer.Models
{
    [BsonIgnoreExtraElements]

    public class Offer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("description")]
        public string Description { get; set; } = String.Empty;

        [BsonElement("location")]
        public string Location { get; set; } = String.Empty;


        [BsonElement("jobTitle")]
        public string JobTitle { get; set; } = String.Empty;


        [BsonElement("field")]
        public string Field { get; set; } = String.Empty;


        [BsonElement("yearsOfExperience")]
        public int YearsOfExperience { get; set; } = 0;


        [BsonElement("languages")]
        public List<string> Languages { get; set; } = new List<string>();

        [BsonElement("sellingPoints")]
        public List<string> SellingPoints { get; set; } = new List<string>();

        [BsonElement("idCompany")]
        public string IdCompany { get; set; } = String.Empty;

        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; } = String.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = String.Empty;


        [BsonElement("keywords")]
        public List<string> Keywords { get; set; } = new List<string>();


        [BsonElement("candidates")]
        public List<string> Candidates { get; set; } = new List<string>();


        [BsonElement("link")]
        public string Link { get; set; } = String.Empty;


        [BsonElement("enable")]
        public bool Enable { get; set; } = true;


        [BsonElement("fulltime")]
        public bool FullTime { get; set; } = true;


        [BsonElement("isFreelance")]
        public bool IsFreelance { get; set; } = true;


    }
}
