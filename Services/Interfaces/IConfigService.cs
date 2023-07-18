using RecruitmentServer.Models;

namespace RecruitmentServer.Services.Interfaces
{
    public interface IConfigService
    {
        public Config GetConfigInfo();
        public void ShutdownServer();
        public void ChangePortNumber(int portNumber);

        public int GetPortNumber();


    }
}
