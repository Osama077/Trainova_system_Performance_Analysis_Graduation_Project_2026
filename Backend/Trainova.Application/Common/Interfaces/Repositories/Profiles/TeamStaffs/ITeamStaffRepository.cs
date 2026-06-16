using Trainova.Domain.Profiles;

namespace Trainova.Application.Common.Interfaces.Repositories.Profiles.TeamStaffs;

public interface ITeamStaffRepository
{
    Task AddAsync(TeamStaff teamStaff);
    Task UpdateAsync(TeamStaff teamStaff);
    Task<TeamStaff?> GetByIdAsync(Guid id);
}
