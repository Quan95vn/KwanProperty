using System.ComponentModel.DataAnnotations;

namespace KwanProperty.IdentityServer4.Quickstart.UserRegistration.PasswordReset
{
    public class RequestPasswordViewModel
    {
        [Required]
        [MaxLength(200)]
        [Display(Name = "Email")]
        [EmailAddress]
        public string Email { get; set; }

    }
}
