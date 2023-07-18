using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using RecruitmentServer.Models.Enums;

namespace RecruitmentServer.Models
{
    public class LoggerInformation
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }


        [BsonElement("operation")]
        public LoggerInformationEnum Operation { get; set; } = LoggerInformationEnum.EMPTY;


        [BsonElement("isError")]
        public bool IsError { get; set; } = false;


        [BsonElement("fromUserId")]
        public string? FromUserId { get; set; }
    }
}
