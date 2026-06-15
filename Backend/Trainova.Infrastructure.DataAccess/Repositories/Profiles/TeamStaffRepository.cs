using Trainova.Application.Common.Interfaces.Repositories.Profiles.TeamStaffs;
using Trainova.Domain.Profiles;

namespace Trainova.Infrastructure.DataAccess.Repositories.Profiles
{
    internal class TeamStaffRepository : ITeamStaffRepository
    {
        private readonly TrainovaWriteDbContext _dbContext;

        public TeamStaffRepository(TrainovaWriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(TeamStaff teamStaff)
        {
            await _dbContext.AddAsync(teamStaff);
        }

        public Task<TeamStaff?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TeamStaff teamStaff)
        {
            throw new NotImplementedException();
        }
    }
}
