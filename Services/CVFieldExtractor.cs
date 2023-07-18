using RecruitmentServer.Services.Interfaces;
using static iText.Kernel.Pdf.Colorspace.PdfSpecialCs;
using System.Text.RegularExpressions;
using Microsoft.IdentityModel.Tokens;

namespace RecruitmentServer.Services
{
    public class CVFieldExtractor : ICVFieldExtractor
    {
        public static string GetFirstname(string text)
        {
            List<string> names = GetNames(text);
            if (names.Count != 2)
            {
                throw new Exception();
            }

            return names[0];
        }

        public static string GetLastname(string text)
        {
            List<string> names =  GetNames(text);
            if (names.Count != 2)
            {
                throw new Exception();
            }

            return names[1];
        }

        public static List<string>? GetNames(string text)
        {

            List<string> firstnames = new List<string>() { "quentin", "alexis", "salma"};
            String lowerText = text.ToLower();

            List<string> names = new List<string>();
            foreach(string firstname in firstnames)
            {
                if (lowerText.Contains(firstname))
                {
                    names.Add(firstname);
                    break;
                }
            }

            if (names.IsNullOrEmpty())
            {
                return null;
            }

            //after the firstname, let's get the the second word after the firstname


            int firstIndexOfFirstname = lowerText.IndexOf(names[0]); //0
            int firstCharOfLastname= UtilService.GetPositionOfNextSpace(lowerText, firstIndexOfFirstname) + 1; //7
            int endCharOfLastname= UtilService.GetPositionOfNextSpace(lowerText, firstCharOfLastname) - 1; //12



            string lastname = lowerText.Substring(firstCharOfLastname, endCharOfLastname - firstCharOfLastname + 1);
            names.Add(lastname);


            return names;
        }

        public static string GetCurrentLocation(string text)
        {
            //todo => algo à améliorer parce qu'actuellement il peut récupérer une ville où la personne à travailler. Il faut donc aller chercher dans des informations 
            //proche du nom
            List<string> cities = new List<string>() { "bury", "aix-en-provence" };
            String lowerText = text.ToLower();

            string location = "";

            foreach (string city in cities)
            {
                if (lowerText.Contains(city))
                {
                    location = city;
                    break;
                }
            }

            return location;

        }

        public static string GetEmail(string text)
        {
            string pattern = @"\b[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Z|a-z]{2,}\b";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            Match match = regex.Match(text);

            string email = "";

            if (match.Success)
            {
                email = match.Value;
            }

            return email;

        }

        public static string GetField(string text)
        {
            List<string> ITKeywords = new List<string>(){ "it", "software", "java", "c++", "c#", "swift" };

            string lowerText = text.ToLower();

            string field = "unknown";

            foreach (string keyword in ITKeywords)
            {
                if (lowerText.Contains(keyword))
                {
                    field = "IT";
                    return field;
                }
            }


            return field;
        }

        public static bool GetIsFreelance(string text)
        {
            string pattern = @"\bfreelance\b";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            Match match = regex.Match(text);

            if (match.Success)
            {
                return true;
            }
            return false;
        }

        public static string GetJobTitle(string field, string text)
        {
            //todo, meme probleme que pour currentLocation, il faut chercher au debut et non pas n'importe où.
            List<string> ITKeywords = new List<string>() { "software developer", "ios developer", "nodejs developer"};

            string lowerText = text.ToLower();
            string jobTitle = "";

            if (field == "IT")
            {
                foreach (string keyword in ITKeywords)
                {
                    if (lowerText.Contains(keyword))
                    {
                        return keyword;
                    }
                }
            }


            return jobTitle;
        }

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

        string ICVFieldExtractor.GetLastname(string text)
        {
            throw new NotImplementedException();
        }
        public static string GetPhoneNumber(string text)
        {
            string pattern = @"\+?\d{1,3}\s?\d{1,3}\s?\d{1,3}\s?\d{1,2}\s?\d{1,2}\s?\d{1,2}";
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            Match match = regex.Match(text);

            string phone = "";

            if (match.Success)
            {
                phone = match.Value;
            }

            return phone;
        }

        public static List<string> GetLanguages(string text)
        {
            string[] languages = new string[]
            {
                "mandarin",
                "chinese",
                "spanish",
                "english",
                "hindi",
                "bengali",
                "portuguese",
                "russian",
                "japanese",
                "punjabi",
                "german",
                "javanese",
                "wu",
                "korean",
                "french",
                "telugu",
                "marathi",
                "turkish",
                "tamil",
                "vietnamese",
                "urdu",
                "italian",
                "yoruba",
                "min nan chinese",
                "azerbaijani",
                "hakka",
                "thai",
                "gujarati",
                "kannada",
                "persian",
                "xinyi",
                "malayalam",
                "sunda",
                "hausa",
                "oriya",
                "burmese",
                "hakka",
                "bhojpuri",
                "tagalog",
                "uzbek",
                "sindhi",
                "amharic",
                "fula",
                "romanian",
                "ojibwe",
                "azeri",
                "awadhi",
                "maithili",
                "gan chinese",
                "igranja",
                "moshi",
                "belarusian",
                "suomi",
                "swahili",
                "chewa",
                "bulgarian",
                "kanuri",
                "gan chinese",
                "kurdish",
                "yao",
                "kanembu",
                "jula",
                "susu",
                "serbo-croatian",
                "kirundi",
                "czech",
                "syriac",
                "kinyarwanda",
                "lozi",
                "chiga",
                "havu",
                "romani",
                "mbunda",
                "senoufo",
                "soninke",
                "tumbuka",
                "gusii",
                "kako",
                "lemba",
                "teso",
                "makua",
                "nyamwezi",
                "mbundu",
                "shubi",
                "sandawe",
                "nyakyusa-ngonde",
                "mende",
                "gogo",
                "ndamba",
                "langi",
                "lugbara",
                "mosiro",
                "tsonga",
                "ruga",
                "lamba",
                "giryama",
                "aheu",
                "segeju",
                "domaaki",
                "asim",
                "maldivian",
                "logoli",
                "tiruray",
                "pursat",
                "subanon",
                "asim",
                "berawan",
                "nagpuri",
                "kakwa",
                "susquehannock",
                "goemai",
                "kanakanabu",
                "sika",
                "bezhta",
                "nivkh",
                "cuyonon",
                "bakhtiari",
                "sumbawa",
                "ijaw",
                "bandjalang",
                "aramaic",
                "yangoru boiken",
                "salvadoran sign language",
                "chayahuita",
                "nagari",
                "dalabon",
                "cahita",
                "singpho",
                "ka"
            };

            string lowerText = text.ToLower();
            List<string> languageSpoken = new List<string>();
            foreach (string language in languages)
            {
                if (lowerText.Contains(language))
                {
                    languageSpoken.Add(language);
                }
            }

            return languageSpoken;
        }

        string ICVFieldExtractor.GetYearsOfExperience(string text)
        {
            throw new NotImplementedException();
        }
    }

}
