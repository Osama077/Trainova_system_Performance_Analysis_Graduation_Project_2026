using Trainova.Application.Common.Models;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Common.Interfaces.Service;

public interface ICurrentUserProvider
{
    ResultOf<CurrentUser> GetCurrentUser();
}