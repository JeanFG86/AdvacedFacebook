using AdvacedFacebook.src.Data.Contracts.Apis;
using AdvacedFacebook.src.Data.Contracts.Repos;
using AdvacedFacebook.src.Data.Service;
using AdvacedFacebook.src.Domain.Error;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AdavacedFacebookTest.src.data.services
{
    
    public class FacebookAtuthentication
    {
        private static void CreateMocks(out Mock<ILoadFacebookUserApi> facebookApi, out FacebookAuthenticationService sut, out string token)
        {
            facebookApi = new Mock<ILoadFacebookUserApi>();
            var loadAccount = new Mock<ILoadUserAccountRepository>();
            var createAccount = new Mock<ICreateFacebookAccountRepository>();
            sut = new FacebookAuthenticationService(facebookApi.Object, loadAccount.Object, createAccount.Object);
            token = "any_token";
        }

        [Fact]
        public async Task LoadFacebookUserApi_With_Correct_Params()
        {
            Mock<ILoadFacebookUserApi> facebookApi;
            FacebookAuthenticationService sut;
            string token;
            CreateMocks(out facebookApi, out sut, out token);

            await sut.Perform(token);

            facebookApi.Verify(x => x.LoadUser(token), Times.Once());
        }

        [Fact]
        public async Task LoadFacebookUserApi_Return_AuthenticationError()
        {
            Mock<ILoadFacebookUserApi> facebookApi;
            FacebookAuthenticationService sut;
            string token;
            CreateMocks(out facebookApi, out sut, out token);

            var r = await sut.Perform(token);

            Assert.Equal(new AuthenticationError().HResult, r.HResult);
        }
    }
}
