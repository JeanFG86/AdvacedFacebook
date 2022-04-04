namespace AdvacedFacebook.src.Data.Contracts.Apis
{
    public abstract class LoadFacebookUserApiResult
    {
        public string? FacebookId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
