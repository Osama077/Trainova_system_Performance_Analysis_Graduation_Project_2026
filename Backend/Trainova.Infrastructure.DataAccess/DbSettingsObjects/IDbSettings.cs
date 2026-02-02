using System.Data;

namespace Trainova.Infrastructure.DataAccess.DbSettingsObjects
{
    public interface IDbSettings
    {
        public string WriteConnectionString { get; }
        public string ReadConnectionString { get; }
        IDbConnection CreateReadingConnection();
        IDbConnection CreateWritingConnection();
    }
}
