namespace RecruitmentServer.Models.Enums
{
    public enum LoggerInformationEnum
    {
        EMPTY,
        LOGGED,

        CANDIDATE_GET = 10,
        CANDIDATE_CREATE,
        CANDIDATE_UPDATE,
        CANDIDATE_CONNECT,
        CANDIDATE_DISCONNECT,
        CANDIDATE_UPLOAD,
        CANDIDATE_ACCEPT,
        CANDIDATE_IGNORE,
        CANDIDATE_UNIGNORE,

        COMPANY_GET = 50,
        COMPANY_CREATE,
        COMPANY_UPDATE,
        COMPANY_GETALL,
        COMPANY_DELETE,

        OFFER_GET = 100,
        OFFER_CREATE,
        OFFER_UPDATE,
        OFFER_GETALL,
        OFFER_MATCH,


        USER_GET = 150,
        USER_GETALL,
        USER_CREATE,
        USER_UPDATE,
        USER_REMOVE,
        USER_GETMANAGERS,

        MATCHING_OFFER = 200,

        CONFIG_GET_INFO = 250,
        CONFIG_SHUTDOWN,
        CONFIG_CHANGE_PORT


    }
}
