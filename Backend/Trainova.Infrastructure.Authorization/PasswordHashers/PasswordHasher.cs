using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Services;
namespace Trainova.Infrastructure.Authorization.PasswordHashers
{
    public class PasswordHasher : IPasswordHasher
    {

        public ResultOf<string> HashPassword(string password)
            => (password.Length < 6) 
                ?Error.Validation(description: "Password is too weak") 
                : BCrypt.Net.BCrypt.EnhancedHashPassword(password);
        //public ResultOf<string> HashPassword(string password)
        //    => (password.Length < 9 ||
        //        !password.Any(char.IsLetter) ||
        //        !password.Any(char.IsDigit)) 
        //        ?Error.Validation(description: "Password is too weak") 
        //        : BCrypt.Net.BCrypt.EnhancedHashPassword(password);


        public bool IsCorrectPassword(string password, string hash)
          => BCrypt.Net.BCrypt.EnhancedVerify(password,hash);
        

    }
}
