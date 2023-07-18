using RecruitmentServer.Models;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using RecruitmentServer.Services.Interfaces;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

namespace RecruitmentServer.Services
{
    public class UserService : IUserService
    {
            
        private readonly IMongoCollection<User> _users;
        public UserService(IDatabaseSettings settings, IMongoClient mongoClient)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>(settings.UserCollectionName);
        }

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void CreateDefaultSuperAdmin()
        {

            if (GetUserLogin("superadmin", "superadmin123") == null)
            {
                var user = new User
                {
                    Username = "superadmin",
                    Password = UtilService.HashPassword("superadmin123"),
                    Firstname = "superadmin",
                    Lastname = "superadmin",
                    Role = UserRole.superadmin,

                };
                _users.InsertOne(user);
            }
        }

        public User GetUserByUsername(string username)
        {
            return _users.Find(user => user.Username == username).FirstOrDefault();
        }
        public User GetUserById(string id)
        {
            return _users.Find(user => user.Id == id).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return _users.Find(user => user.Role == UserRole.user).ToList();
        }

        public List<User> GetManagers()
        {
            return _users.Find(user => user.Role == UserRole.manager).ToList();
        }

        public void Remove(string id)
        {
            _users.DeleteOne(user => user.Id == id);
        }

        public void Update(User user)
        {
            _users.ReplaceOne(u => u.Username == user.Username, user);
        }

        public User? GetUserLogin(string username, string password)
        {
            var user = _users.Find(user => user.Username == username && user.Password == UtilService.HashPassword(password)).FirstOrDefault();
            return user;
        }
    }
}
