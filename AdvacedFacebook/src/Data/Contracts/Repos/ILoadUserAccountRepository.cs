using AdvacedFacebook.src.Domain.Models;

namespace AdvacedFacebook.src.Data.Contracts.Repos
{
    public interface ILoadUserAccountRepository
    {
        Task<IAccountData?> Load(string email);
    }
}
