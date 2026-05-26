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

namespace Trainova.Application.FitnessStatus.SessionMovements.Commands.DeleteSessionMovement
{
    [Authorize(Roles = "Coach")]
    public record DeleteSessionMovementCommand(Guid Id) : IRequest<ResultOf<Done>>;

    public class DeleteSessionMovementCommandHandler : IRequestHandler<DeleteSessionMovementCommand, ResultOf<Done>>
    {
        private readonly ISessionMovementRepository _sessionMovementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteSessionMovementCommandHandler(
            ISessionMovementRepository sessionMovementRepository,
            IUnitOfWork unitOfWork)
        {
            _sessionMovementRepository = sessionMovementRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ResultOf<Done>> Handle(DeleteSessionMovementCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var movement = await _sessionMovementRepository.GetByIdAsync(request.Id);
                if (movement == null)
                    return Error.NotFound(description: "Session movement stats not found.");

                await _unitOfWork.StartTransactionAsync();
                await _sessionMovementRepository.DeleteAsync(movement);
                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return new Done(id: movement.Id).NoContent;
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(code: ex.Code, description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "DeleteSessionMovementCommandHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }
}
