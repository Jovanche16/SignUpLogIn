using System;
using System.Collections.Generic;

namespace ForMyWebPage.Models
{
    public partial class RefreshTokenDto
    {
        public int Id { get; set; }
        public string? Token { get; set; }
        public int? UserId { get; set; }
    }
}
