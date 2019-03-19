namespace Xyzies.SSO.Identity.API
{
    public class AuthorizeModel
    {
        public string client_id { get; set; }
        public string redirect_uri { get; set; }
        public string response_type { get; set; }
        public string scope { get; set; }
        public string response_mode { get; set; }
        public string nonce { get; set; }
        public string prompt { get; set; }
        public string ui_locales { get; set; }
        public string state { get; set; }
    }
}
