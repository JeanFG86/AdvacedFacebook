using AdvacedFacebook.src.Domain.Models;

namespace AdvacedFacebook.src.Data.Contracts.Apis
{
    public interface ILoadFacebookUserApi
    {
        Task<IFacebookData> LoadUser(string token);
    }
}
