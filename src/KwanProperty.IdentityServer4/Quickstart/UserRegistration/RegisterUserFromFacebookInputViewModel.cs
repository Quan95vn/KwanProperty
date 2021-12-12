namespace KwanProperty.IdentityServer4.Quickstart.UserRegistration
{
    public class RegisterUserFromFacebookInputViewModel
    {
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string Email { get; set; }
        public string Provider { get; set; }
        public string ProviderUserId { get; set; }
    }
}
