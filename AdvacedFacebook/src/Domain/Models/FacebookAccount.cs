namespace AdvacedFacebook.src.Domain.Models
{
    public class FacebookAccount
    {
        public string? Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }
        public string FacebookId { get; private set; }

        public FacebookAccount(IFacebookData facebokData, IAccountData? accountData)
        {
            Id = accountData?.Id;
            Name = accountData?.Name ?? facebokData.Name;
            Email = facebokData.Email;
            FacebookId = facebokData.FacebookId;
        }
    }
}
