using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.TrainingSessionAccessablity;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Enums;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.TrainingSessionsAccessibility.AccessPolicies;
using Trainova.Domain.TrainingSessionsAccessibility.TrainingSessions;

namespace Trainova.Application.TrainingSessionsAccessibility.TrainingSessions.Commands.CreateTrainingSession
{
    public class CreateTrainingSessionCommandHandler(
        ITrainingSessionRepository _trainingSessionRepository,
        IPlanRepository _planRepository,
        IUserAccessPolicyRepository _userAccessPolicyRepository,
        IAccsessPolicyRepository _accsessPolicyRepository,
        IUnitOfWork _unitOfWork,
        IUsersRepository _usersRepository)
        : IRequestHandler<CreateTrainingSessionCommand, ResultOf<TrainingSession>>
    {

        public async Task<ResultOf<TrainingSession>> Handle(CreateTrainingSessionCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _unitOfWork.StartTransactionAsync();
                AccessPolicy? accessPolicy = null;
                if ( request.PolicyId.HasValue)
                {
                    accessPolicy = await _accsessPolicyRepository.GetByIdAsync(request.PolicyId.Value);
                    if (accessPolicy == null)
                    {
                        return Error.Conflict(
                            code: "CreateTrainingSessionCommandHandler.Handle_Conflict",
                            description:"Access policy not found"
                            );
                    }
                }
                else
                {
                    accessPolicy = new AccessPolicy(request.SessionName);
                    await _accsessPolicyRepository.AddAsync(accessPolicy);
                    if (request.UserIds.Any())
                    {
                        var userAccessPolicies =new List<UserAccessPolicy>();
                        foreach (var userId in request.UserIds)
                        {
                            var user = await _usersRepository.GetByIdAsync(userId);
                            if(user != null)
                            {
                                var userAccessPolicy = new UserAccessPolicy(accessPolicy.Id, userId, AttendanceStatus.Waiting);
                                userAccessPolicies.Add(userAccessPolicy);
                            }
                            else
                            {
                                return Error.NotFound(
                                    code: "CreateTrainingSessionCommandHandler.Handle_NotFound",
                                    description: $"User with id: {userId} not found"
                                    );
                            }
                        }
                        await _userAccessPolicyRepository.AddRangeAsync(userAccessPolicies);

                    }
                }


                if (request.PlanId.HasValue)
                {
                    if (! await _planRepository.ExistsAsync(request.PlanId.Value))
                        return Error.Conflict(
                            code: "CreateTrainingSessionCommandHandler.Handle_Conflict",
                            description: "Plan not found"
                            );
                }



                var session = new TrainingSession(request.SessionName, accessPolicy.Id, PlanState.Active,request.Place, request.WillHappenAt, request.PlanId);

                await _trainingSessionRepository.AddAsync(session);


                await _unitOfWork.SaveChangesAsync(cancellationToken);


                await _unitOfWork.CommitTransactionAsync();

                return session.AsCreated();

            }
            catch (DomainException ex)
            {
                return Error.DomainFailure(
                    code: ex.Code,
                    description: ex.Message);
            }
            catch (Exception ex)
            {
                return Error.Failure(code: "CreateTrainingSessionCommandHandler.Handle_Failure", description: ex.Message);
            }
        }
    }



}
