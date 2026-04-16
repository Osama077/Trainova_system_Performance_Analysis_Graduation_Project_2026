using Trainova.Application.Authentication.Queries.Login;

namespace Trainova.Api.Requests.Auth
{
    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public LoginQuery ToQuery()
        {
            return new LoginQuery(Email, Password);
        }
    }
}
