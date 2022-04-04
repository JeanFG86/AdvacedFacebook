using AdvacedFacebook.src.Data.Contracts.Apis;
using AdvacedFacebook.src.Data.Contracts.Repos;
using AdvacedFacebook.src.Domain.Error;
using AdvacedFacebook.src.Domain.Features;

namespace AdvacedFacebook.src.Data.Service
{
    public class FacebookAuthenticationService
    {
        private readonly ILoadFacebookUserApi facebookApi;
        private readonly ILoadUserAccountRepository loadUserAccountRepo;
        private readonly ICreateFacebookAccountRepository createUserAccountRepo;

        public FacebookAuthenticationService(ILoadFacebookUserApi facebookApi, 
            ILoadUserAccountRepository loadUserAccountRepo, 
            ICreateFacebookAccountRepository createUserAccountRepo)
        {
            this.facebookApi = facebookApi;
            this.loadUserAccountRepo = loadUserAccountRepo;
            this.createUserAccountRepo = createUserAccountRepo;
        }

        public async Task<AuthenticationError> Perform(string token)
        {
            var fbData = await facebookApi.LoadUser(token);
            if(fbData != null)
            {
                await loadUserAccountRepo.Load(fbData.Email);
                await createUserAccountRepo.CreateFromFacebook(fbData.Email, fbData.Name, fbData.FacebookId);
            }

            return new AuthenticationError();
        }
    }
}
