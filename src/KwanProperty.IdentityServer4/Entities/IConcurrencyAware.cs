namespace KwanProperty.IdentityServer4.Entities
{
    public interface IConcurrencyAware
    {
        string ConcurrencyStamp { get; set; }
    }
}
