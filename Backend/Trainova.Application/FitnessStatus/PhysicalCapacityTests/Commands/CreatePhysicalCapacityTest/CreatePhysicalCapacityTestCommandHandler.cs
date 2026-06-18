using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.FitnessStatus;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.Players;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.PhysicalCapacityTests.Commands.CreatePhysicalCapacityTest
{
    public class CreatePhysicalCapacityTestCommandHandler(
        IPhysicalCapacityTestRepository capacityTestRepository,
        IPlayerRepository playerRepository,
        IUnitOfWork unitOfWork,
        CurrentUser currentUser) : IRequestHandler<CreatePhysicalCapacityTestCommand, ResultOf<PhysicalCapacityTest>>
    {
        private readonly IPhysicalCapacityTestRepository _capacityTestRepository = capacityTestRepository;
        private readonly IPlayerRepository _playerRepository = playerRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly CurrentUser _currentUser = currentUser;

        public async Task<ResultOf<PhysicalCapacityTest>> Handle(CreatePhysicalCapacityTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var player = await _playerRepository.GetByIdAsync(request.PlayerId);
                if (player == null)
                    return Error.NotFound(description: "Player not found.");


                var lastTest = await _capacityTestRepository.GetLatestByPlayerIdAsync(request.PlayerId);

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

                var newcapacityTest = player.GenreteNewPhysicalTest(explosive, aerobic, sprint, lastTest, request.CreationType, _currentUser.Id);

                await _unitOfWork.StartTransactionAsync();
                await _capacityTestRepository.AddAsync(newcapacityTest);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return newcapacityTest.AsCreated();
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
