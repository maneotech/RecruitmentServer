using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RecruitmentServer.Models
{
    [BsonIgnoreExtraElements]

    public class Candidate
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("firstname")]
        public string Firstname { get; set; } = String.Empty;


        [BsonElement("lastname")]
        public string Lastname { get; set; } = String.Empty;

        [BsonElement("currentLocation")]
        public string CurrentLocation { get; set; } = String.Empty;

        [BsonElement("targetLocation")]
        public string TargetLocation { get; set; } = String.Empty;


        [BsonElement("jobTitle")]
        public string JobTitle { get; set; } = String.Empty;


        [BsonElement("field")]
        public string Field { get; set; } = String.Empty;


        [BsonElement("yearsOfExperience")]
        public int YearsOfExperience { get; set; } = 0;


        [BsonElement("languages")]
        public List<string> Languages { get; set; } = new List<string>();

        [BsonElement("phoneNumber")]
        public string PhoneNumber { get; set; } = String.Empty;

        [BsonElement("email")]
        public string Email { get; set; } = String.Empty;


        [BsonElement("keywords")]
        public List<string> Keywords { get; set; } = new List<string>();


        [BsonElement("pictureUrl")]
        public string PictureUrl { get; set; } = String.Empty;


        [BsonElement("cvUrl")]
        public string CvUrl { get; set; } = String.Empty;


        [BsonElement("enable")]
        public bool Enable { get; set; } = true;


        [BsonElement("fulltime")]
        public bool FullTime { get; set; } = true;


        [BsonElement("isFreelance")]
        public bool IsFreelance { get; set; } = false;

        [BsonElement("comment")]
        public string Comment { get; set; } = String.Empty;


        [BsonElement("ignored")]
        public bool Ignored { get; set; } = false;  
    }
}
