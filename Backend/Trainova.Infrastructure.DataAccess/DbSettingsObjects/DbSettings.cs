using Microsoft.Data.SqlClient;
using System.Data;

namespace Trainova.Infrastructure.DataAccess.DbSettingsObjects
{

    public class DbSettings :IDbSettings
    {
        public string WriteConnectionString { get; }
        public string ReadConnectionString { get; }

        public DbSettings(string? writeCs, string? readCs)
        {
            WriteConnectionString = writeCs
                ?? throw new ArgumentNullException(nameof(writeCs));

            ReadConnectionString = readCs
                ?? throw new ArgumentNullException(nameof(readCs));
        }
        public IDbConnection CreateReadingConnection()
        {
            return new SqlConnection(ReadConnectionString);
        }
        public IDbConnection CreateWritingConnection()
        {
            return new SqlConnection(WriteConnectionString);
        }
    }


}