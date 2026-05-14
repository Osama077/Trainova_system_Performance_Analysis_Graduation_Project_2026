using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.Injuries.Commands.DeleteInjury
{
    [Authorize(Roles = "Doctor")]
    public record DeleteInjuryCommand(Guid Id) : IRequest<ResultOf<Done>>;
}
