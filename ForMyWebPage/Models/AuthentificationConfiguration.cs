namespace ForMyWebPage.Models
{
    public class AuthentificationConfiguration
    {
        public string AccessTokenSecret { get; set; }
        public string AccessTokenExpiratonDate { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
