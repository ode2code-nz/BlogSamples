using System.ComponentModel.DataAnnotations;

namespace ToDo.SharedModels.v1.Identity
{
    public class TokenRequestModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
