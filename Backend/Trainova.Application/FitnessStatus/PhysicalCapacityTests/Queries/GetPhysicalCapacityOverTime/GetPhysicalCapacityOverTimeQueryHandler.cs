using Dapper;
using MediatR;
using System.Data;
using Trainova.Application.Common.Interfaces.Repositories;
using Trainova.Common.Errors;
using Trainova.Common.ResultOf;
using Trainova.Domain.FitnessStatus;

namespace Trainova.Application.FitnessStatus.PhysicalCapacityTests.Queries.GetPhysicalCapacityOverTime
{
    public class GetPhysicalCapacityOverTimeQueryHandler(IDbSettings _dbSettings)
        : IRequestHandler<GetPhysicalCapacityOverTimeQuery, ResultOf<IEnumerable<PhysicalCapacityDataReadModel>>>
    {
        public async Task<ResultOf<IEnumerable<PhysicalCapacityDataReadModel>>> Handle(
            GetPhysicalCapacityOverTimeQuery request,
            CancellationToken cancellationToken)
        {
            try
            {
                const string sql = "FitnessData.sp_GetPhysicalCapacityHistoryByPlayerId";

                var parameters = new
                {
                    PlayerId = request.PlayerId
                };

                using var connection = _dbSettings.CreateReadingConnection();

                var rawTests = await connection.QueryAsync<PhysicalCapacityTest, AerobicCapacityTest, SprintTest, ExplosivePowerTest, PhysicalCapacityTest>(
                    sql,
                    (test, aerobic, sprint, explosive) =>
                    {
                        typeof(PhysicalCapacityTest).GetProperty(nameof(PhysicalCapacityTest.AerobicCapacityTest))?.SetValue(test, aerobic);
                        typeof(PhysicalCapacityTest).GetProperty(nameof(PhysicalCapacityTest.SprintTest))?.SetValue(test, sprint);
                        typeof(PhysicalCapacityTest).GetProperty(nameof(PhysicalCapacityTest.ExplosivePowerTest))?.SetValue(test, explosive);
                        return test;
                    },
                    parameters,
                    splitOn: "MaximumOxygenConsumption,Time10Meters,CountermovementJumpHeight",
                    commandType: CommandType.StoredProcedure
                );

                const decimal baselineVO2Max = 60.0m;
                const decimal baseline10m = 1.75m;
                const decimal baseline30m = 4.0m;
                const decimal baselineJump = 45.0m;
                const decimal baselineRSI = 2.0m;

                var result = rawTests.Select(test =>
                {
                    double aerobicScore = test.AerobicCapacityTest is not null
                        ? (double)(test.AerobicCapacityTest.MaximumOxygenConsumption / baselineVO2Max)
                        : 0.0;

                    double speedScore = 0.0;
                    if (test.SprintTest is not null && test.SprintTest.Time10Meters > 0 && test.SprintTest.Time30Meters > 0)
                    {
                        decimal score10m = baseline10m / test.SprintTest.Time10Meters;
                        decimal score30m = baseline30m / test.SprintTest.Time30Meters;
                        speedScore = (double)((score10m + score30m) / 2.0m);
                    }

                    double powerScore = 0.0;
                    if (test.ExplosivePowerTest is not null && test.ExplosivePowerTest.CountermovementJumpHeight > 0 && test.ExplosivePowerTest.ReactiveStrengthIndex > 0)
                    {
                        decimal jumpScore = test.ExplosivePowerTest.CountermovementJumpHeight / baselineJump;
                        decimal rsiScore = test.ExplosivePowerTest.ReactiveStrengthIndex / baselineRSI;
                        powerScore = (double)((jumpScore + rsiScore) / 2.0m);
                    }


                    return new PhysicalCapacityDataReadModel
                    {
                        CreatedAt = test.CreatedAt,
                        CalculatedCapacity = (double)test.CalculatedCapacity,
                        CalculatedAerobicCapacity = aerobicScore,
                        CalculatedSpeedCapacity = speedScore,
                        CalculatedExplosivePowerCapacity = powerScore,
                        ProgressFromLastTest = test.ProgressFromLastTest,
                        OverriddenCapacity = test.OverriddenCapacity
                    };
                });

                return result.AsDone();
            }
            catch (Exception ex)
            {
                return Error.Failure(
                    code: "GetPhysicalCapacityOverTimeQueryHandler.Handle_Failure",
                    description: ex.Message);
            }
        }
    }

}
