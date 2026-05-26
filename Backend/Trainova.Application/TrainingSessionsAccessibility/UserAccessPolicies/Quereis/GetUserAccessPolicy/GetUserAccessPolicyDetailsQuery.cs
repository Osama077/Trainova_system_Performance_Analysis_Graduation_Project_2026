using MediatR;
using Trainova.Common.ResultOf;
using Trainova.Domain.TrainingSessionsAccessibility;

namespace Trainova.Application.TrainingSessionsAccessibility.UserAccessPolicies.Quereis.GetUserAccessPolicy
{
    public record GetUserAccessPolicyDetailsQuery(Guid PolicyId) : IRequest<ResultOf<IEnumerable<UserAccessDetailes>>>;

    public class UserAccessDetailes
    {
        public Guid Id { get; set; }

        public AttendanceStatus AttendanceStatus { get; set; }
        public decimal DoneScore { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime LastUpdate { get; set; }


        public Guid UserId { get; set; }

        public string UserShowName { get; set; }
        public string FullName { get; set; }
        public string PhotoPath { get; set; }

        public Guid AccessPolicyId { get; set; }
        public string AccessPolicyName { get; set; }
    }

}
