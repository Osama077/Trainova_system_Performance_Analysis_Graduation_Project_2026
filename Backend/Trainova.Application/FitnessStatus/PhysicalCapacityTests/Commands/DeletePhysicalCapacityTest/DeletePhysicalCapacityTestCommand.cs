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

namespace Trainova.Application.FitnessStatus.PhysicalCapacityTests.Commands.DeletePhysicalCapacityTest
{
    [Authorize(Roles = "Coach")]
    public record DeletePhysicalCapacityTestCommand(Guid Id) : IRequest<ResultOf<Done>>;

    public class DeletePhysicalCapacityTestCommandHandler : IRequestHandler<DeletePhysicalCapacityTestCommand, ResultOf<Done>>
    {
        private readonly IPhysicalCapacityTestRepository _capacityTestRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeletePhysicalCapacityTestCommandHandler(
            IPhysicalCapacityTestRepository capacityTestRepository,
            IUnitOfWork unitOfWork)
        {
            _capacityTestRepository = capacityTestRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultOf<Done>> Handle(DeletePhysicalCapacityTestCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var capacityTest = await _capacityTestRepository.GetByIdAsync(request.Id);
                if (capacityTest == null)
                    return Error.NotFound(description: "Physical capacity test not found.");

                await _unitOfWork.StartTransactionAsync();
                await _capacityTestRepository.DeleteAsync(capacityTest);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return new Done(id: capacityTest.Id).NoContent;
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "DeletePhysicalCapacityTestCommandHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
