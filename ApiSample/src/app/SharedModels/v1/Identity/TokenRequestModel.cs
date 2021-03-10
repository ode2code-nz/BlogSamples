using System.ComponentModel.DataAnnotations;

namespace ApiSample.SharedModels.v1.Identity
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
