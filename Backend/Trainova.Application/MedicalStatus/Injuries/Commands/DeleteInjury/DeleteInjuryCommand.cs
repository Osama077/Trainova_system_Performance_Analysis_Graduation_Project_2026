using MediatR;
using Trainova.Application.Common.Authorization;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.Injuries.Commands.DeleteInjury
{
    [Authorize(Role = "Doctor")]
    public record DeleteInjuryCommand(Guid Id) : IRequest<ResultOf<Done>>;
}
