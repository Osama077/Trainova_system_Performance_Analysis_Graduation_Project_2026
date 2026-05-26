using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trainova.Application.Common.Authorization;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus.PhysicalCapacityTests;

namespace Trainova.Application.FitnessStatus.PhysicalCapacityTests.Commands.UpdatePhysicalCapacityTest
{
    [Authorize(Roles = "Coach")]
    public record UpdatePhysicalCapacityTestCommand(
        Guid Id,
        decimal MaximumOxygenConsumption,
        int YoYoIntermittentRecoveryLevel1Distance,
        int YoYoIntermittentRecoveryLevel2Distance,
        decimal Time10Meters,
        decimal Time30Meters,
        decimal CountermovementJumpHeight,
        decimal ReactiveStrengthIndex) : IRequest<ResultOf<PhysicalCapacityTest>>;

    public class UpdatePhysicalCapacityTestCommandHandler : IRequestHandler<UpdatePhysicalCapacityTestCommand, ResultOf<PhysicalCapacityTest>>
    {
        private readonly IPhysicalCapacityTestRepository _capacityTestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePhysicalCapacityTestCommandHandler(
            IPhysicalCapacityTestRepository capacityTestRepository,
            IUnitOfWork unitOfWork)
        {
            _capacityTestRepository = capacityTestRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultOf<PhysicalCapacityTest>> Handle(UpdatePhysicalCapacityTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var capacityTest = await _capacityTestRepository.GetByIdAsync(request.Id);
                if (capacityTest == null)
                    return Error.NotFound(description: "Physical capacity test not found.");

                var aerobic = new AerobicCapacityTest(
                    request.MaximumOxygenConsumption,
                    request.YoYoIntermittentRecoveryLevel1Distance,
                    request.YoYoIntermittentRecoveryLevel2Distance
                );

                var sprint = new SprintTest(
                    request.Time10Meters,
                    request.Time30Meters
                );

                var explosive = new ExplosivePowerTest(
                    request.CountermovementJumpHeight,
                    request.ReactiveStrengthIndex
                );

                capacityTest.Update(aerobic, sprint, explosive);

                await _unitOfWork.StartTransactionAsync();
                await _capacityTestRepository.UpdateAsync(capacityTest);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return capacityTest.AsDone();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "UpdatePhysicalCapacityTestCommandHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
