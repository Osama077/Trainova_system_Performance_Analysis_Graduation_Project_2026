namespace Trainova.Infrastructure.DataAccess.Seeders
{
    public interface IDataSeeder<TEntity>
        where TEntity : class
    {
        int Order { get; }

        IEnumerable<TEntity> Seed();
    }

}
