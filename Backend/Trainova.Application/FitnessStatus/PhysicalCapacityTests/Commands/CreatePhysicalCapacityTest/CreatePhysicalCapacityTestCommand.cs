using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Trainova.Application.Common.Authorization;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Players;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus.PhysicalCapacityTests;

namespace Trainova.Application.FitnessStatus.PhysicalCapacityTests.Commands.CreatePhysicalCapacityTest
{
    [Authorize(Roles = "Coach")]
    public record CreatePhysicalCapacityTestCommand(
        Guid PlayerId,
        decimal MaximumOxygenConsumption,
        int YoYoIntermittentRecoveryLevel1Distance,
        int YoYoIntermittentRecoveryLevel2Distance,
        decimal Time10Meters,
        decimal Time30Meters,
        decimal CountermovementJumpHeight,
        decimal ReactiveStrengthIndex) : IRequest<ResultOf<PhysicalCapacityTest>>;

    public class CreatePhysicalCapacityTestCommandHandler : IRequestHandler<CreatePhysicalCapacityTestCommand, ResultOf<PhysicalCapacityTest>>
    {
        private readonly IPhysicalCapacityTestRepository _capacityTestRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePhysicalCapacityTestCommandHandler(
            IPhysicalCapacityTestRepository capacityTestRepository,
            IPlayerRepository playerRepository,
            IUnitOfWork unitOfWork)
        {
            _capacityTestRepository = capacityTestRepository;
            _playerRepository = playerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultOf<PhysicalCapacityTest>> Handle(CreatePhysicalCapacityTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var player = await _playerRepository.GetByIdAsync(request.PlayerId);
                if (player == null)
                    return Error.NotFound(description: "Player not found.");

                var existing = await _capacityTestRepository.GetByPlayerIdAsync(request.PlayerId);
                if (existing != null)
                    return Error.Conflict(description: "Physical capacity test already exists for this player. Use Update instead.");

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

                var capacityTest = new PhysicalCapacityTest(
                    request.PlayerId,
                    aerobic,
                    sprint,
                    explosive
                );

                await _unitOfWork.StartTransactionAsync();
                await _capacityTestRepository.AddAsync(capacityTest);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return capacityTest.AsCreated();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "CreatePhysicalCapacityTestCommandHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
