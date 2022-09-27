namespace ForMyWebPage.Models.Responses
{
    public class RefreshTokenResponse
    {
        public int? Id { get; set; }
        public string Token { get; set; }
        public int? userId { get; set; }
    }
}
