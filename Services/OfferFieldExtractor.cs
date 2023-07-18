using RecruitmentServer.Services.Interfaces;

namespace RecruitmentServer.Services
{
    public class OfferFieldExtractor : IOfferFieldExtractor
    {
        public static List<string> GetKeywords(string field, string text)
        {
            List<string> ITKeywords = new List<string>() { "java", "c++", "c#", "swift" };

            string lowerText = text.ToLower();
            List<string> keywords = new List<string>();

            if (field == "IT")
            {
                foreach (string keyword in ITKeywords)
                {
                    if (lowerText.Contains(keyword))
                    {
                        keywords.Add(keyword);
                    }
                }
            }
            return keywords;
        }
    }
}
