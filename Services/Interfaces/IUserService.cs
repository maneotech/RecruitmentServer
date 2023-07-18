using RecruitmentServer.Models;

namespace RecruitmentServer.Services.Interfaces
{
    public interface IUserService
    {
        List<User> GetUsers();

        List<User> GetManagers();

        User GetUserById(string id);

        User GetUserByUsername(string username);

        User Create(User user);

        void CreateDefaultSuperAdmin();
        void Update(User user);

        void Remove(string id);

        User? GetUserLogin(string email, string password);

    }
}
