namespace AdvacedFacebook.src.Domain.Features
{
    public interface IFacebookAuthentication
    {
        FacebookAuthenticationResult Perform(FacebookAuthenticationParams param);
    }
}
