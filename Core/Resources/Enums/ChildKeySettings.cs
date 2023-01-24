namespace Core.Resources.Enums
{
    public enum ChildKeySettings : short
    {
        Session_Language_CurrentLangOid = 100,
        Session_Language_DefaultLangOid = 101,

        Server_Language_CachedForAll = 200,

        InfoMail_GlobalAccess = 400,
        RegisterMail_GlobalAccess = 401,
        Static_ClientEmailEquivalent = 402,
        MobileContactPhone_GlobalAccess = 403,

        TextPathFormat = 500,
        JsonPathFormat = 501,

        Email_SendGrid_ApiKey = 600,

        SendGrid_API_Key = 700,

        ConcatSerialDefaultMatch = 800,
        ConcatCallbackAsResultMatch = 805,
        ConcatComparisonMatch = 810
    }
}
