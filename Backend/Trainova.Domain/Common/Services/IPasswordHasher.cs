using Trainova.Common.ResultOf;


namespace Trainova.Domain.Common.Services
{
    public interface IPasswordHasher
    {

        public ResultOf<string> HashPassword(string password);
        public bool IsCorrectPassword(string password, string hash);
        

    }
}
