using Microsoft.Data.SqlClient;
using System.Data;

namespace Trainova.Infrastructure.DataAccess.DbSettingsObjects
{

    public class DbSettings : IDbSettings, IDisposable, IAsyncDisposable
    {
        private readonly ConnectionString _connectionString;
        private IDbConnection? _readConnection;
        private IDbConnection? _writeConnection;
        private bool _disposed;

        public DbSettings(ConnectionString connectionString)
        {
            _connectionString = connectionString;
        }

        public string WriteConnectionString => _connectionString.TrainovaWriteDbConnection;
        public string ReadConnectionString => _connectionString.TrainovaReadDbConnection;

        public IDbConnection CreateReadingConnection()
        {
            if (_readConnection == null)
            {
                _readConnection = new SqlConnection(ReadConnectionString);
            }
            return _readConnection;
        }

        public IDbConnection CreateWritingConnection()
        {
            if (_writeConnection == null)
            {
                _writeConnection = new SqlConnection(WriteConnectionString);
            }
            return _writeConnection;
        }

        #region Disposal Pattern

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    if (_readConnection != null)
                    {
                        if (_readConnection.State != ConnectionState.Closed)
                            _readConnection.Close();
                        _readConnection.Dispose();
                        _readConnection = null;
                    }

                    if (_writeConnection != null)
                    {
                        if (_writeConnection.State != ConnectionState.Closed)
                            _writeConnection.Close();
                        _writeConnection.Dispose();
                        _writeConnection = null;
                    }
                }
                _disposed = true;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (!_disposed)
            {
                if (_readConnection is System.Data.Common.DbConnection readDbConn)
                {
                    await readDbConn.DisposeAsync();
                    _readConnection = null;
                }
                else if (_readConnection != null)
                {
                    _readConnection.Dispose();
                    _readConnection = null;
                }

                if (_writeConnection is System.Data.Common.DbConnection writeDbConn)
                {
                    await writeDbConn.DisposeAsync();
                    _writeConnection = null;
                }
                else if (_writeConnection != null)
                {
                    _writeConnection.Dispose();
                    _writeConnection = null;
                }

                Dispose(false);
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }

        #endregion
    }
    public class ConnectionString
    {
        public string TrainovaWriteDbConnection { get; set; } = string.Empty;
        public string TrainovaReadDbConnection { get; set; } = string.Empty;
    }


}