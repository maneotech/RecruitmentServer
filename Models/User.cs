using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
namespace RecruitmentServer.Models
{
    [BsonIgnoreExtraElements]
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("firstname")]
        public string Firstname { get; set; } = String.Empty;

        [BsonElement("lastname")]
        public string Lastname { get; set; } = String.Empty;

        [BsonElement("username")]
        public string Username { get; set; } = String.Empty;

        [BsonElement("password")]
        public string Password { get; set; } = String.Empty;

        [BsonElement("role")]
        public UserRole Role { get; set; } = UserRole.user;
    }

    public enum UserRole
    {
        superadmin,
        manager,
        user
    }
}
