namespace AdvacedFacebook.src.Data.Contracts.Repos
{
    public interface ILoadUserAccountRepository
    {
        Task Load(string email);
    }
}
