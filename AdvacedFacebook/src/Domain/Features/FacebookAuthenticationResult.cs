using AdvacedFacebook.src.Domain.Error;
using AdvacedFacebook.src.Domain.Models;

namespace AdvacedFacebook.src.Domain.Features
{
    public record FacebookAuthenticationResult(Tuple<IAccessToken?, AuthenticationError?>? Result);
}
