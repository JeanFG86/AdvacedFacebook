using AdvacedFacebook.src.Domain.Models;
using Moq;
using Xunit;

namespace AdavacedFacebookTest.src.domain.models
{
    public class FacebookAccountTest
    {
        private static Mock<IFacebookData> CreateMockFacebookData()
        {
            var fbData = new Mock<IFacebookData>();
            fbData.SetupGet(c => c.Email).Returns("any_fb_email");
            fbData.SetupGet(c => c.FacebookId).Returns("any_fb_id");
            fbData.SetupGet(c => c.Name).Returns("any_fb_name");
            return fbData;
        }

        [Fact]
        public void Create_With_Facebook_Data_Only()
        {
            Mock<IFacebookData> fbData = CreateMockFacebookData();

            var sut = new FacebookAccount(fbData.Object);

            Assert.Equal("any_fb_email", sut.Email);
            Assert.Equal("any_fb_name", sut.Name);
            Assert.Equal("any_fb_id", sut.FacebookId);
        }        

        [Fact]
        public void Update_With_Facebook_Name_Empty()
        {
            Mock<IFacebookData> fbData = CreateMockFacebookData();

            var accountData = new Mock<IAccountData>();
            accountData.SetupGet(c => c.Id).Returns("any_id");

            var sut = new FacebookAccount(fbData.Object, accountData.Object);

            Assert.Equal("any_id", sut.Id);
            Assert.Equal("any_fb_email", sut.Email);
            Assert.Equal("any_fb_name", sut.Name);
            Assert.Equal("any_fb_id", sut.FacebookId);
        }

        [Fact]
        public void Update_With_Facebook_Name_Not_Empty()
        {
            Mock<IFacebookData> fbData = CreateMockFacebookData();

            var accountData = new Mock<IAccountData>();
            accountData.SetupGet(c => c.Id).Returns("any_id");
            accountData.SetupGet(c => c.Name).Returns("any_name");

            var sut = new FacebookAccount(fbData.Object, accountData.Object);

            Assert.Equal("any_id", sut.Id);
            Assert.Equal("any_fb_email", sut.Email);
            Assert.Equal("any_name", sut.Name);
            Assert.Equal("any_fb_id", sut.FacebookId);
        }
    }
}
