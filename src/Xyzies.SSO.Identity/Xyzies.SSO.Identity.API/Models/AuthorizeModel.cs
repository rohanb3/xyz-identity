namespace Xyzies.SSO.Identity.API
{
    /// <summary>
    /// 
    /// </summary>
    public class AuthorizeModel
    {
        // TODO: Fix IDE1006 issue
#pragma warning disable IDE1006 // Naming Styles
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
        public string client_id { get; set; }

        public string redirect_uri { get; set; }

        public string response_type { get; set; }

        public string scope { get; set; }

        public string response_mode { get; set; }

        public string nonce { get; set; }

        public string prompt { get; set; }

        public string ui_locales { get; set; }

        public string state { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
#pragma warning restore IDE1006 // Naming Styles
    }
}
