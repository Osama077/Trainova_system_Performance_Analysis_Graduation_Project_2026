using Trainova.Domain.Common.Enums;
using Trainova.Domain.Common.Services;
using Trainova.Domain.Profiles.Players;
using Trainova.Domain.Profiles.TeamsStaff;
using Trainova.Domain.SeasonsAnalyses.Teams;
using Trainova.Domain.UserAuth.Roles;
using Trainova.Domain.UserAuth.UserRoles;
using Trainova.Domain.UserAuth.Users;

namespace Trainova.Infrastructure.DataAccess.Seeders
{
    public class RoleSeeder : IDataSeeder<Role>
    {
        public int Order => 1;
        public IEnumerable<Role> Seed()
        {
            return new List<Role>
            {
                new Role(0, "SystemOwner"),
                new Role(8, "TestAccount"),
                new Role(1, "SystemAdmin"),
                new Role(2, "Player"),
                new Role(3, "TeamStaff"),
                new Role(4, "HeadCoach"),
                new Role(5, "AssistantCoach"),
                new Role(6, "Doctor"),
                new Role(7, "FitnessCoach")
            };
        }
    }
    public class PlayerSeeder : IDataSeeder<Player>
    {
        private readonly List<User> _playeresAcc;
        public PlayerSeeder(List<User> playeresAcc)
        {
            _playeresAcc = playeresAcc.Where(user => user.ShowName.Contains("A7rf")).ToList();
        }

        public int Order => 5;

        public IEnumerable<Player> Seed()
        {
            var fahmyAcc = _playeresAcc.First(u=>u.Email == "abdalrahmanmohamedf@gmail.com");
            var fahmy = new Player(
                fahmyAcc.Id,
                5,
                "Fahmy",
                PlayerMedicalStatus.Fit,
                Position.CAM,
                Position.CDM | Position.CB | Position.RB,
                0,
                DateOnly.FromDateTime(DateTime.Now),
                Guid.Empty
            );

            var zezoAcc = _playeresAcc.First(u => u.Email == "ak2156@fayoum.edu.eg");
            var zezo = new Player(
                zezoAcc.Id,
                6,
                "zezo",
                PlayerMedicalStatus.Fit,
                Position.CAM,
                Position.CDM | Position.CB | Position.RB,
                0,
                DateOnly.FromDateTime(DateTime.Now),
                Guid.Empty
            );

            var osamaAcc = _playeresAcc.First(u => u.Email == "eltwo3m@gmail.com");
            var osama = new Player(
                osamaAcc.Id,
                7,
                "osama",
                PlayerMedicalStatus.Fit,
                Position.CAM,
                Position.CDM | Position.CB | Position.RB,
                0,
                DateOnly.FromDateTime(DateTime.Now),
                Guid.Empty
            );
            return new List<Player>
            {
                fahmy,
                zezo,
                osama
            };


        }
    }
    public class TeamStaffSeeder : IDataSeeder<TeamStaff>
    {
        private readonly List<User> _staffUseres;
        public TeamStaffSeeder(List<User> staffUseres)
        {
            _staffUseres = staffUseres.Where(user => user.ShowName.Contains("System")).ToList();
        }

        public int Order => 6;



        public IEnumerable<TeamStaff> Seed()
        {
            var staff = new List<TeamStaff>();

            var ahmedAcc = _staffUseres.First(s=>s.Email == "ahmed.kh.zain2156@gmail.com");
            var ahmed = new TeamStaff(ahmedAcc.Id, null, null, TeamStaffRole.headCoach);
            ahmed.SetCreator(Guid.Empty);
            staff.Add(ahmed);

            var osamaAcc = _staffUseres.First(s=>s.Email == "eltwooo3m@gmail.com");
            var osama = new TeamStaff(osamaAcc.Id, null, null, TeamStaffRole.headCoach);
            osama.SetCreator(Guid.Empty);

            staff.Add(osama);

            var amrAcc = _staffUseres.First(s=>s.Email == "am7899@gmail.com");
            var amr = new TeamStaff(ahmedAcc.Id, null, null, TeamStaffRole.headCoach);
            amr.SetCreator(Guid.Empty);
            staff.Add(amr);


            return staff;
        }
    }
    public class TeamSeeder : IDataSeeder<Team>
    {
        public int Order => 2;

        public IEnumerable<Team> Seed()
        {
            return new List<Team>
            {
                new Team("the Stupids","El fayoum dawla",Guid.Empty)
            };
        }
    }

    public class UserSeeder : IDataSeeder<User>
    {
        private readonly IPasswordHasher _hasher;
        private readonly Team _team;
        public UserSeeder(IPasswordHasher hasher)
        {
            _hasher = hasher;
        }
        public int Order => 3;

        public IEnumerable<User> Seed()
        {
            var users = new List<User>();
            var systemOners = new List<User>();

            var systemOwner = new User(
                showName: "System Owner Ahmed",
                fullName: "Ahmed kh Zain",
                email: "ahmed.kh.zain2156@gmail.com",
                teamId: _team.Id
            );

            var passwordResult =
                systemOwner.SetNewPassword(
                    "Password123",
                    _hasher
                );

            if (passwordResult.IsFailure)
            {
                throw new Exception(
                    "Failed to seed SystemOwner password");
            }

            systemOwner.ConfirmEmail();
            systemOwner.SetCreator(Guid.Empty);

            systemOners.Add(systemOwner);

            // ------------------------------------------------

            var systemOwner1 = new User(
                showName: "System Owner Amr",
                fullName: "Amr Mousv",
                email: "am7899@gmail.com",
                teamId: _team.Id
            );

            var passwordResult1 =
                systemOwner.SetNewPassword(
                    "Password123",
                    _hasher
                );

            if (passwordResult.IsFailure)
            {
                throw new Exception(
                    "Failed to seed SystemOwner password");
            }

            systemOwner1.ConfirmEmail();
            systemOwner1.SetCreator(Guid.Empty);
            systemOners.Add(systemOwner);

            // ------------------------------------------------

            var systemOwner2 = new User(
                showName: "System Owner Osama",
                fullName: "System Owner",
                email: "eltwooo3m@gmail.com",
                teamId: _team.Id
            );

            var passwordResult2 =
                systemOwner.SetNewPassword(
                    "Password123",
                    _hasher
                );

            if (passwordResult2.IsFailure)
            {
                throw new Exception(
                    "Failed to seed SystemOwner password");
            }

            systemOwner2.ConfirmEmail();
            systemOwner2.SetCreator(Guid.Empty);
            systemOners.Add(systemOwner);
            // ------------------------------------------------
            users.AddRange(systemOners);

            // ------------------------------------------------
            var players = new List<User>();
            var player1 = new User(
               showName: "Osama Nsr",
               fullName: "A7rf La3b fe el fayoum",
               email: "eltwo3m@gmail.com",
               teamId: _team.Id
               );


            var testPasswordResult1 =
                player1.SetNewPassword(
                    "Password123",
                    _hasher
                );

            if (testPasswordResult1.IsFailure)
            {
                throw new Exception(
                    "Failed to seed TestAccount password");
            }

            player1.ConfirmEmail();
            player1.SetCreator(Guid.Empty);
            players.Add(player1);
            // ------------------------------------------------
            var player2 = new User(
               showName: "Zezo El gamed gddddaaaa",
               fullName: "A7rf La3b fe el Qahhera",
               email: "ak2156@fayoum.edu.eg",
               teamId: _team.Id
               );


            var testPasswordResult2 =
                player1.SetNewPassword(
                    "Password123",
                    _hasher
                );

            if (testPasswordResult2.IsFailure)
            {
                throw new Exception(
                    "Failed to seed TestAccount password");
            }

            player2.ConfirmEmail();
            player2.SetCreator(Guid.Empty);
            players.Add(player2);
            // ------------------------------------------------
            var player3 = new User(
               showName: "Fahmy el fas44iiii5",
               fullName: "A7rf La3b fe el giza",
               email: "abdalrahmanmohamedf@gmail.com",
               teamId: _team.Id
               );


            var testPasswordResult3 =
                player3.SetNewPassword(
                    "Password123",
                    _hasher
                );

            if (testPasswordResult3.IsFailure)
            {
                throw new Exception(
                    "Failed to seed TestAccount password");
            }

            player3.ConfirmEmail();
            player3.SetCreator(Guid.Empty);
            players.Add(player3);
            // ------------------------------------------------
            users.AddRange(players);

            // ------------------------------------------------
            return users;
        }
    }

    public class UesrRoleSeeder : IDataSeeder<UserRole>
    {
        public int Order => 4;

        public IEnumerable<UserRole> Seed()
        {
            throw new NotImplementedException();
        }
    }
}
