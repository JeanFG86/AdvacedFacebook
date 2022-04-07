using AdvacedFacebook.src.Data.Contracts.Apis;
using AdvacedFacebook.src.Data.Contracts.Repos;
using AdvacedFacebook.src.Domain.Error;
using AdvacedFacebook.src.Domain.Features;
using AdvacedFacebook.src.Domain.Models;

namespace AdvacedFacebook.src.Data.Service
{
    public class FacebookAuthenticationService
    {
        private readonly ILoadFacebookUserApi facebookApi;
        private readonly ILoadUserAccountRepository loadUserAccountRepo;
        private readonly ISaveFacebookRepository saveUserAccountRepo;

        public FacebookAuthenticationService(ILoadFacebookUserApi facebookApi, 
            ILoadUserAccountRepository loadUserAccountRepo,
            ISaveFacebookRepository saveUserAccountRepo)
        {
            this.facebookApi = facebookApi;
            this.loadUserAccountRepo = loadUserAccountRepo;
            this.saveUserAccountRepo = saveUserAccountRepo;
        }

        public async Task<AuthenticationError> Perform(string token)
        {
            var fbData = await facebookApi.LoadUser(token);
            if(fbData != null)
            {
                var accountData = await this.loadUserAccountRepo.Load(fbData.Email);
                var fbAccount = new FacebookAccount(fbData, accountData);
                await saveUserAccountRepo.SaveWithFacebook(new ISaveFacebookRepositoryParams(fbAccount.Id, fbAccount.Email, fbAccount.Name, fbAccount .FacebookId));
            }

            return new AuthenticationError();
        }
    }
}
