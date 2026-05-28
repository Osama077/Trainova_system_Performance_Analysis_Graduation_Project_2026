using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility;
using Trainova.Domain.UserAuth;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.CreateTrainingSession
{
    public class CreateTrainingSessionCommandHandler(
        ITrainingSessionRepository _trainingSessionRepository,
        IPlanRepository _planRepository,
        IUserAccessPolicyRepository _userAccessPolicyRepository,
        IAccessPolicyRepository _accessPolicyRepository,
        IUnitOfWork _unitOfWork,
        IUsersRepository _usersRepository,
        CurrentUser _currentUser)
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



                AccessPolicy accessPolicy;
                List<UserAccessPolicy> userAccessPolicies;

                if (!creatingNewPolicy)
                {
                    var polcyToBeCopied = await _accessPolicyRepository.GetByIdIncludingUsersAsync(request.PolicyId!.Value);
                    if (polcyToBeCopied is null)
                        return Error.NotFound("CreateTrainingSession.PolicyNotFound", "Access policy not found");

                    accessPolicy = polcyToBeCopied.CopyAccessPolicy(out userAccessPolicies);
                }
                else
                {
                    accessPolicy = new AccessPolicy(request.SessionName, AccessPolicyType.Session);

                    var users = await _usersRepository.GetByIdsAsync(request.UserIds!);

                    if (users.Count() != request.UserIds!.Count)
                        return Error.NotFound("CreateTrainingSession.UserNotFound", "One or more users not found");

                    userAccessPolicies = users
                        .Select(u => new UserAccessPolicy(accessPolicy.Id, u.Id, AttendanceStatus.Waiting))
                        .ToList();

                }
                var session = CreateTrainingSessionAsWithNeededType(request, accessPolicy);

                userAccessPolicies.ForEach(u=>u.AddNotification(session));


                await _unitOfWork.StartTransactionAsync();


                await _accessPolicyRepository.AddAsync(accessPolicy);

                await _userAccessPolicyRepository.AddRangeAsync(userAccessPolicies);



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
        private TrainingSession CreateTrainingSessionAsWithNeededType(CreateTrainingSessionCommand request, AccessPolicy accessPolicy)
        {
            var sessionType = _currentUser switch
            {
                _ when _currentUser.IsInRole(StaticRoleNamesData.DoctorName) => SessionType.DoctorVisit,
                _ when _currentUser.IsInRole(StaticRoleNamesData.HeadCoachName) => SessionType.TrainingVisit,
                _ when _currentUser.IsInRole(StaticRoleNamesData.FitnessCoachName) => SessionType.FitnessVisit,
                _ => SessionType.Other
            };
            return new TrainingSession(
                    request.SessionName,
                    accessPolicy.Id,
                    request.PlanState,
                    sessionType,
                    request.Place,
                    request.WillHappenAt,
                    request.PlanId,
                    _currentUser.Id);
        }

    }




}
