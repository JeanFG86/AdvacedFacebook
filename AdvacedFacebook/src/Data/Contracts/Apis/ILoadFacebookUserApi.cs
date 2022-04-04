namespace AdvacedFacebook.src.Data.Contracts.Apis
{
    public interface ILoadFacebookUserApi
    {
        Task<LoadFacebookUserApiResult> LoadUser(string token);
    }
}
