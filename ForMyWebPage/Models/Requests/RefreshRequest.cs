using System.ComponentModel.DataAnnotations;

namespace ForMyWebPage.Models.Requests
{
    public class RefreshRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}
