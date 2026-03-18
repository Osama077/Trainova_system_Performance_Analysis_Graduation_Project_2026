using Trainova.Application.Common.Models;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Common.Interfaces.Services;

public interface ICurrentUserProvider
{
    CurrentUser? GetCurrentUser();
}