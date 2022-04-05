namespace AdvacedFacebook.src.Data.Contracts.Repos
{
    public interface ILoadUserAccountRepository
    {
        Task<string> Load(string email);
    }
}
