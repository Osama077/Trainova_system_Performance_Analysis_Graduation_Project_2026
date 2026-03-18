using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.CommonRepos;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.AuditLogs;
using Trainova.Domain.MedicalStatus.Injuries;

namespace Trainova.Application.MedicalStatus.Injuries.Queries.GetInjuriesHistory
{
    public class GetInjuriesHistoryQueryHandler(
        IAuditRepository _auditRepository)
        : IRequestHandler<GetInjuriesHistoryQuery, ResultOf<IEnumerable<AuditLog>>>
    {

        public async Task<ResultOf<IEnumerable<AuditLog>>> Handle(GetInjuriesHistoryQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var injuries = await _auditRepository.GetAuditLogsAsync(
                    typeof(Injury).Name,
                    request.Id.ToString(),
                    request.Page,
                    request.PageSize,
                    request.IncludeAdded,
                    request.IncludeDeleted,
                    request.IncludeUpdated);

                return injuries.AsPartial();
            }
            catch (Exception ex)
            {
                return Error.Unexpected(
                    code: "GetInjuriesHistoryQueryHandler.Handle_Unexpected",
                    description: $"An unexpected error occurred while retrieving injuries.\n {ex.Message}\n");
            }
        }
    }

}