using FluentValidation;
using MediatR;
using Trainova.Application.Authentication.Common;
using Trainova.Common.ResultOf;

namespace Trainova.Application.Authentication.Queries.Login;

public record LoginQuery(
    string Email,
    string Password)
    : IRequest<ResultOf<AuthenticationResultBase>>;

public class LogInQueryValidator : AbstractValidator<LoginQuery>
{
    public LogInQueryValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}