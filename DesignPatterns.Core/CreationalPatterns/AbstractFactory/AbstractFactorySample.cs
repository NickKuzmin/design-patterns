using System;

namespace DesignPatterns.Core.AbstractFactory
{
    abstract class DbProviderFactory
    {
        public abstract DbConnection CreateConnection();

        public abstract DbCommand CreateCommand();
    }

    class MssqlProviderFactory : DbProviderFactory
    {
        public override DbConnection CreateConnection()
        {
            return new MssqlConnection();
        }

        public override DbCommand CreateCommand()
        {
            return new MssqlCommand();
        }
    }

    class PostgreSqlProviderFactory : DbProviderFactory
    {
        public override DbConnection CreateConnection()
        {
            return new PostgreSqlConnection();
        }

        public override DbCommand CreateCommand()
        {
            return new PostgreSqlCommand();
        }
    }

    abstract class DbConnection : System.IDisposable
    {
        public abstract void Connect(string connectionString);

        public void Dispose()
        {
            DisposeInternal();
        }

        protected abstract void DisposeInternal();
    }

    abstract class DbCommand : System.IDisposable
    {
        public abstract void Execute(string command);

        public void Dispose()
        {
            DisposeInternal();
        }

        protected abstract void DisposeInternal();
    }

    class MssqlConnection : DbConnection
    {
        public override void Connect(string connectionString)
        {
        }

        protected override void DisposeInternal()
        {
        }
    }

    class MssqlCommand : DbCommand
    {
        public override void Execute(string command)
        {
        }

        protected override void DisposeInternal()
        {
        }
    }

    class PostgreSqlConnection : DbConnection
    {
        public override void Connect(string connectionString)
        {
        }

        protected override void DisposeInternal()
        {
        }
    }

    class PostgreSqlCommand : DbCommand
    {
        public override void Execute(string command)
        {
        }

        protected override void DisposeInternal()
        {
        }
    }

    class DbLogger
    {
        public void Run(DbProviderFactory dbProviderFactory, string connectionString, string sqlCommand)
        {
            using (var connection = dbProviderFactory.CreateConnection())
            {
                ExecuteConnection(connection, connectionString);
                using (var command = dbProviderFactory.CreateCommand())
                {
                    command.Execute(sqlCommand);
                }
            }
        }

        private void ExecuteConnection(DbConnection connection, string connectionString)
        {
            connection.Connect(connectionString);
        }
    }

    class DbClient
    {
        public void Execute(string type)
        {
            DbProviderFactory dbProviderFactory;
            if (type == "MsSql")
            {
                dbProviderFactory = new MssqlProviderFactory();
            }
            else if (type == "PostgreSql")
            {
                dbProviderFactory = new PostgreSqlProviderFactory();
            }
            else
                throw new ArgumentException();

            var dbLogger = new DbLogger();
            dbLogger.Run(dbProviderFactory, "", "SELECT * FROM Users");
        }
    }
}
