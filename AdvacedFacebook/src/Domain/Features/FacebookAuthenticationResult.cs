using AdvacedFacebook.src.Domain.Error;
using AdvacedFacebook.src.Domain.Models;

namespace AdvacedFacebook.src.Domain.Features
{
    public abstract class FacebookAuthenticationResult
    {
        public Tuple<IAccessToken?, AuthenticationError?>? Result { get; set; }
    }
}
