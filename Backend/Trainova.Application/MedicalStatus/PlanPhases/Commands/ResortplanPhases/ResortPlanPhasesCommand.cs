using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Common.ResultOf;

namespace Trainova.Application.MedicalStatus.PlanPhases.Commands.ResortplanPhases
{
    public record ResortPlanPhasesCommand():IRequest<ResultOf<PlayerInjuryRecoveryPlanData>>
    {
    }

    public class PlayerInjuryRecoveryPlanData
    {
    }
}
