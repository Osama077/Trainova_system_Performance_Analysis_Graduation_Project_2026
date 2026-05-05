using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.CreateTrainingSession
{
    public class CreateTrainingSessionCommandHandler(
        ITrainingSessionRepository _trainingSessionRepository,
        IPlanRepository _planRepository,
        IUserAccessPolicyRepository _userAccessPolicyRepository,
        IAccessPolicyRepository _accessPolicyRepository,
        IUnitOfWork _unitOfWork,
        IUsersRepository _usersRepository)
        : IRequestHandler<CreateTrainingSessionCommand, ResultOf<TrainingSession>>
    {
        public async Task<ResultOf<TrainingSession>> Handle(CreateTrainingSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {

                if (string.IsNullOrWhiteSpace(request.SessionName))
                    return Error.Validation("CreateTrainingSession.InvalidName", "Session name is required");

                var creatingNewPolicy = !request.PolicyId.HasValue;

                if (creatingNewPolicy && !(request.UserIds?.Any() ?? false))
                    return Error.Validation("CreateTrainingSession.EmptyUsers", "Users are required when creating a new policy");

                if (request.PlanId.HasValue)
                {
                    var exists = await _planRepository.ExistsAsync(request.PlanId.Value);
                    if (!exists)
                        return Error.NotFound("CreateTrainingSession.PlanNotFound", "Plan not found");
                }

                await _unitOfWork.StartTransactionAsync();

                AccessPolicy accessPolicy;

                if (!creatingNewPolicy)
                {
                    accessPolicy = await _accessPolicyRepository.GetByIdAsync(request.PolicyId!.Value);

                    if (accessPolicy is null)
                        return Error.NotFound("CreateTrainingSession.PolicyNotFound", "Access policy not found");
                }
                else
                {
                    accessPolicy = new AccessPolicy(request.SessionName);

                    var users = await _usersRepository.GetByIdsAsync(request.UserIds!);

                    if (users.Count() != request.UserIds!.Count)
                        return Error.NotFound("CreateTrainingSession.UserNotFound", "One or more users not found");

                    var userAccessPolicies = users
                        .Select(u => new UserAccessPolicy(accessPolicy.Id, u.Id, AttendanceStatus.Waiting))
                        .ToList();
                    await _accessPolicyRepository.AddAsync(accessPolicy);

                    await _userAccessPolicyRepository.AddRangeAsync(userAccessPolicies);
                }

                var session = new TrainingSession(
                    request.SessionName,
                    accessPolicy.Id,
                    request.PlanState,
                    request.Place,
                    request.WillHappenAt,
                    request.PlanId);

                await _trainingSessionRepository.AddAsync(session);

                await _unitOfWork.SaveChangesAsync(cancellationToken);
                await _unitOfWork.CommitTransactionAsync();

                return session.AsCreated();
            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(ex.Code, ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Unexpected("CreateTrainingSession.Unexpected", ex.Message);
            }
        }
    }




}
