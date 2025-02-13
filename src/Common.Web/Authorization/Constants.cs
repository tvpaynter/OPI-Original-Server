namespace StatementIQ.Common.Web.Authorization
{
    public static class Constants
    {
        public static class ApiUrls
        {
            public static class Authentication
            {
                public static readonly string Login = "/authentication/login";
                public static readonly string RefreshToken = "/authentication/refresh";
            }
        }

        public static class LoginHeaders
        {
            public static readonly string UserAgent =
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/80.0.3987.149 Safari/537.36";

            public static readonly string AcceptEncoding = "gzip, deflate, br";
            public static readonly string AcceptLanguage = "en-US,en;q=0.9,ru;q=0.8,uk;q=0.7";
        }
    }
}