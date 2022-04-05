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
        private static void CreateMocks(out Mock<ILoadFacebookUserApi> facebookApi,
            out Mock<ILoadUserAccountRepository> loadAccount,
            out Mock<ICreateFacebookAccountRepository> createAccount,
            out FacebookAuthenticationService sut, 
            out string token)
        {
            facebookApi = new Mock<ILoadFacebookUserApi>();
            loadAccount = new Mock<ILoadUserAccountRepository>();
            createAccount = new Mock<ICreateFacebookAccountRepository>();
            sut = new FacebookAuthenticationService(facebookApi.Object, loadAccount.Object, createAccount.Object);
            token = "any_token";
        }

        [Fact]
        public async Task LoadFacebookUserApi_With_Correct_Params()
        {
            Mock<ILoadFacebookUserApi> facebookApi;
            Mock<ILoadUserAccountRepository> loadAccount;
            Mock<ICreateFacebookAccountRepository> createAccount;
            FacebookAuthenticationService sut;
            string token;
            CreateMocks(out facebookApi, out loadAccount, out createAccount, out sut, out token);

            await sut.Perform(token);

            facebookApi.Verify(x => x.LoadUser(token), Times.Once());
        }

        [Fact]
        public async Task LoadFacebookUserApi_Return_AuthenticationError()
        {
            Mock<ILoadFacebookUserApi> facebookApi;
            Mock<ILoadUserAccountRepository> loadAccount;
            Mock<ICreateFacebookAccountRepository> createAccount;
            FacebookAuthenticationService sut;
            string token;
            CreateMocks(out facebookApi, out loadAccount, out createAccount, out sut, out token);

            var returns = await sut.Perform(token);

            Assert.Equal(new AuthenticationError().HResult, returns.HResult);
        }

        [Fact]
        public async Task LoadFacebookUserApi_Return_LoadFacebookUserApi()
        {
            Mock<ILoadFacebookUserApi> facebookApi;
            Mock<ILoadUserAccountRepository> loadAccount;
            Mock<ICreateFacebookAccountRepository> createAccount;
            FacebookAuthenticationService sut;
            string token;
            CreateMocks(out facebookApi, out loadAccount, out createAccount, out sut, out token);
            
            facebookApi.Setup(x => x.LoadUser(token)).ReturnsAsync(new LoadFacebookUserApiResult("any_fb_id", "any_fb_name", "any_fb_email"));
            await sut.Perform(token);
            loadAccount.Verify(x => x.Load("any_fb_email"), Times.Once());
        }

        [Fact]
        public async Task LoadFacebookUserApi_Return_Undefined()
        {
            Mock<ILoadFacebookUserApi> facebookApi;
            Mock<ILoadUserAccountRepository> loadAccount;
            Mock<ICreateFacebookAccountRepository> createAccount;
            FacebookAuthenticationService sut;
            string token;
            CreateMocks(out facebookApi, out loadAccount, out createAccount, out sut, out token);

            loadAccount.Setup(x => x.Load("any_fb_email")).ReturnsAsync("");
            facebookApi.Setup(x => x.LoadUser(token)).ReturnsAsync(new LoadFacebookUserApiResult("any_fb_id", "any_fb_name", "any_fb_email"));
            await sut.Perform(token);
            createAccount.Verify(x => x.CreateFromFacebook("any_fb_email", "any_fb_name", "any_fb_id"), Times.Once());
        }
    }
}
