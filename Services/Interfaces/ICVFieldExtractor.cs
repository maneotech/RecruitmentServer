namespace RecruitmentServer.Services.Interfaces
{
    public interface ICVFieldExtractor
    {

        static string GetFirstname(string text) => throw new NotImplementedException();
        string GetLastname(string text) => throw new NotImplementedException();
        static string GetNames(string text) => throw new NotImplementedException();

        string GetCurrentLocation(string text) => throw new NotImplementedException();

        string GetJobTitle(string text) => throw new NotImplementedException();

        string GetField(string text) => throw new NotImplementedException();

        string GetYearsOfExperience(string text) => throw new NotImplementedException();

        public static List<string> GetLanguages(string text) => throw new NotImplementedException();

        public static string  GetPhoneNumber(string text) => throw new NotImplementedException();

        static string GetEmail(string text) => throw new NotImplementedException();

        static List<string> GetKeywords(string text) => throw new NotImplementedException();

        public static bool GetIsFreelance(string text) => throw new NotImplementedException();


    }
}
