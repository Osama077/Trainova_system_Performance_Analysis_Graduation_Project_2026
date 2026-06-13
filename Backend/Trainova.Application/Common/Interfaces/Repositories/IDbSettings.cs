using System.Data;

namespace Trainova.Application.Common.Interfaces.Repositories
{
    public interface IDbSettings
    {
        public string WriteConnectionString { get; }
        public string ReadConnectionString { get; }
        IDbConnection CreateReadingConnection();
        IDbConnection CreateWritingConnection();
    }
}
