using AdvacedFacebook.src.Data.Contracts.Apis;
using AdvacedFacebook.src.Data.Contracts.Repos;
using AdvacedFacebook.src.Data.Service;
using AdvacedFacebook.src.Domain.Error;
using AdvacedFacebook.src.Domain.Models;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace AdavacedFacebookTest.src.data.services
{
    
    public class FacebookAtuthentication
    {
        private static void CreateMocks(out Mock<ILoadFacebookUserApi> facebookApi,
            out Mock<ILoadUserAccountRepository> loadAccount,
            out Mock<ISaveFacebookRepository> saveAccount,
            out FacebookAuthenticationService sut, 
            out string token)
        {
            facebookApi = new Mock<ILoadFacebookUserApi>();
            loadAccount = new Mock<ILoadUserAccountRepository>();
            saveAccount = new Mock<ISaveFacebookRepository>();
            sut = new FacebookAuthenticationService(facebookApi.Object, loadAccount.Object, saveAccount.Object);
            token = "any_token";
        }

        [Fact]
        public async Task LoadFacebookUserApi_With_Correct_Params()
        {
            Mock<ILoadFacebookUserApi> facebookApi;
            Mock<ILoadUserAccountRepository> loadAccount;
            Mock<ISaveFacebookRepository> saveAccount;
            FacebookAuthenticationService sut;
            string token;
            CreateMocks(out facebookApi, out loadAccount, out saveAccount, out sut, out token);

            await sut.Perform(token);

            facebookApi.Verify(x => x.LoadUser(token), Times.Once());
        }

        [Fact]
        public async Task LoadFacebookUserApi_Return_AuthenticationError()
        {
            Mock<ILoadFacebookUserApi> facebookApi;
            Mock<ILoadUserAccountRepository> loadAccount;
            Mock<ISaveFacebookRepository> saveAccount;
            FacebookAuthenticationService sut;
            string token;
            CreateMocks(out facebookApi, out loadAccount, out saveAccount, out sut, out token);

            var returns = await sut.Perform(token);

            Assert.Equal(new AuthenticationError().HResult, returns.HResult);
        }

        [Fact]
        public async Task LoadFacebookUserApi_Return_LoadFacebookUserApi()
        {
            Mock<ILoadFacebookUserApi> facebookApi;
            Mock<ILoadUserAccountRepository> loadAccount;
            Mock<ISaveFacebookRepository> saveAccount;
            FacebookAuthenticationService sut;
            string token;
            CreateMocks(out facebookApi, out loadAccount, out saveAccount, out sut, out token);

            var aCord = new Mock<IFacebookData>();
            aCord.SetupGet(c => c.Email).Returns("any_fb_email");
            aCord.SetupGet(c => c.FacebookId).Returns("any_fb_id");
            aCord.SetupGet(c => c.Name).Returns("any_fb_name");

            facebookApi.Setup(x => x.LoadUser(token)).ReturnsAsync(aCord.Object);
            await sut.Perform(token);
            loadAccount.Verify(x => x.Load("any_fb_email"), Times.Once());
        }

        [Fact]
        public async Task LoadFacebookUserApi_Return_Undefined()
        {
            Mock<ILoadFacebookUserApi> facebookApi;
            Mock<ILoadUserAccountRepository> loadAccount;
            Mock<ISaveFacebookRepository> saveAccount;
            FacebookAuthenticationService sut;
            string token;
            CreateMocks(out facebookApi, out loadAccount, out saveAccount, out sut, out token);

            var aCord = new Mock<IFacebookData>();
            aCord.SetupGet(c => c.Email).Returns("any_fb_email");
            aCord.SetupGet(c => c.FacebookId).Returns("any_fb_id");
            aCord.SetupGet(c => c.Name).Returns("any_fb_name");

            facebookApi.Setup(x => x.LoadUser(token)).ReturnsAsync(aCord.Object);
            await sut.Perform(token);

            //var aCordParams = new Mock<ISaveFacebookRepositoryParams>();
            //aCordParams.SetupGet(c => c.Email).Returns("any_fb_email");
            //aCordParams.SetupGet(c => c.FacebookId).Returns("any_fb_id");
            //aCordParams.SetupGet(c => c.Name).Returns("any_fb_name");

            saveAccount.Verify(x => x.SaveWithFacebook(new ISaveFacebookRepositoryParams(null, "any_fb_email", "any_fb_name", "any_fb_id")), Times.Once());

            //Mock<ILoadFacebookUserApi> facebookApi;
            //Mock<ILoadUserAccountRepository> loadAccount;
            //Mock<ISaveFacebookRepository> saveAccount;
            //FacebookAuthenticationService sut;
            //string token;
            //CreateMocks(out facebookApi, out loadAccount, out saveAccount, out sut, out token);

            //var aCord = new Mock<IAccountData>();
            ////aCord.SetupGet(c => c.Name).Returns("");

            //var aCordFb = new Mock<IFacebookData>();
            //aCordFb.SetupGet(c => c.Email).Returns("any_fb_email");
            //aCordFb.SetupGet(c => c.FacebookId).Returns("any_fb_id");
            //aCordFb.SetupGet(c => c.Name).Returns("any_fb_name");

            //var aCordParams = new Mock<ISaveFacebookRepositoryParams>();
            //aCordParams.SetupGet(c => c.Email).Returns("any_fb_email");
            //aCordParams.SetupGet(c => c.FacebookId).Returns("any_fb_id");
            //aCordParams.SetupGet(c => c.Name).Returns("any_fb_name");

            //loadAccount.SetupSequence(x => x.Load("any_fb_email")).ReturnsAsync(aCord.Object);
            //facebookApi.Setup(x => x.LoadUser(token)).ReturnsAsync(aCordFb.Object);
            //await sut.Perform(token);
            //saveAccount.Verify(x => x.SaveWithFacebook(aCordParams.Object), Times.Once());
        }
    }
}
