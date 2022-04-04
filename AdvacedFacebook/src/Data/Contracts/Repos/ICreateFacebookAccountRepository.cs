namespace AdvacedFacebook.src.Data.Contracts.Repos
{
    public interface ICreateFacebookAccountRepository
    {
        Task CreateFromFacebook(string email, string nome, string facebookId);
    }
}
