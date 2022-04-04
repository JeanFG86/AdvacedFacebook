namespace AdvacedFacebook.src.Domain.Error
{
    public class AuthenticationError: Exception
    {
        public AuthenticationError()
            :base("Authentication failed")
        {

        }
    }
}
