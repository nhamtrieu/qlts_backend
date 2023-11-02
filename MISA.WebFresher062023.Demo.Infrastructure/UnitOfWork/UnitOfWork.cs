using MISA.WebFresher062023.Demo.Application;
using MySqlConnector;
using System.Data;
using System.Data.Common;

namespace MISA.WebFresher062023.Demo.Infrastructure
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private DbConnection _connection = null;
        private DbTransaction _transction = null;
        private readonly string _connectionString;

        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbConnection Connection => _connection ??= new MySqlConnection(_connectionString);

        public DbTransaction? Transaction => _transction;

        public void BeginTransaction()
        {
            _connection ??= new MySqlConnection(_connectionString);

            if(_connection.State == ConnectionState.Open)
            {
                _transction = _connection.BeginTransaction();
            } else
            {
                _connection.Open();
                _transction = _connection.BeginTransaction();
            }
        }

        public async Task BeginTransactionAsync()
        {
            _connection ??= new MySqlConnection(_connectionString);

            if( Connection.State == ConnectionState.Open)
            {
                _transction = await _connection.BeginTransactionAsync();
            } else
            {
                await _connection.OpenAsync();
                _transction= await _connection.BeginTransactionAsync();
            }
        }

        public void Commit()
        {
            _transction?.Commit();
            Dispose();
        }

        public async Task CommitAsync()
        {
            if(_transction != null)
            {
                await _transction.CommitAsync();
            }
            await DisposeAsync();
        }

        public void Dispose()
        {
            if(_transction != null)
            {
                _transction.Dispose();
                _transction = null;
            } 

            if(_connection != null)
            {
                _connection.Dispose();
                _connection = null;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if(_transction != null)
            {
                await _transction.DisposeAsync();
                _transction = null;
            } 

            if( _connection != null)
            {
                await _connection.DisposeAsync();
                _connection = null;
            }
        }

        public void RollBack()
        {
            _transction?.Rollback();
            Dispose();
        }

        public async Task RollBackAsync()
        {
            if(_transction != null)
            {
                await _transction.RollbackAsync();
            }
            await DisposeAsync();
        }
    }
}
