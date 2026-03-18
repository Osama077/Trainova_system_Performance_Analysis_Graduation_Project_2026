using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using Trainova.Common.ResultOf;
using Trainova.Domain.MedicalStatus.PlayerInjuries;

namespace Trainova.Application.MedicalStatus.PlayerInjuries.Commands.UpdatePlayerInjury
{
    public class UpdatePlayerInjuryCommand:IRequest<ResultOf<PlayerInjury>>
    {
    }
}
