using MediatR;
using Trainova.Application.Common.Interfaces.Repositories.Profiles.TeamStaffs;
using Trainova.Application.Common.Interfaces.Repositories.UserAuth;
using Trainova.Application.Common.Interfaces.Services;
using Trainova.Application.Common.Models;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.Common.Helpers;
using Trainova.Domain.Common.Services;
using Trainova.Domain.Profiles;
using Trainova.Domain.UserAuth;

namespace Trainova.Application.Profiles.Commands.CreateTeamStaffProfile;

public class CreateTeamStaffProfileCommandHandler(
    IUsersRepository _usersRepository,
    ITeamStaffRepository _teamStaffRepository,
    IPasswordHasher _passwordHasher,
    IUnitOfWork _unitOfWork,
    CurrentUser _currentUser)
    : IRequestHandler<CreateTeamStaffProfileCommand, ResultOf<CreateTeamStaffProfileResponse>>
{
    public async Task<ResultOf<CreateTeamStaffProfileResponse>> Handle(CreateTeamStaffProfileCommand request, CancellationToken cancellationToken)
    {
        try
        {
            if (await _usersRepository.ExistsByEmailAsync(request.Email))
                return Error.Conflict("EmailExistsAlready", "cant use already used Email");
            var user = new User(request.ShowName, request.FullName, request.Email, request.PhotoPath);

            var passwordResult = user.SetNewPassword(request.Password, _passwordHasher);
            if (passwordResult.IsFailure)
            {
                return passwordResult.Errors;
            }

            var teamStaff = new TeamStaff(
                Id: user.Id,
                insuranceFilesLink: request.InsuranceFilesLink,
                contractFilesLink: request.ContractFilesLink,
                role: request.Role,
                createdBy: _currentUser.Id);

            await _unitOfWork.StartTransactionAsync();

            await _usersRepository.AddUserAsync(user);
            await _teamStaffRepository.AddAsync(teamStaff);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            await _unitOfWork.CommitTransactionAsync();

            return new CreateTeamStaffProfileResponse(
                teamStaff.Id,
                user.ShowName,
                user.FullName,
                teamStaff.Role
            );
        }
        catch (DomainException ex)
        {
            return Error.DomainFailure(code: ex.Code, description: ex.Message);
        }
        catch (Exception ex)
        {
            return Error.Unexpected(code: "CreateTeamStaffProfileCommandHandler.Handle_Unexpected", description: ex.Message);
        }
    }
}
