using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;

namespace MockDbProvider
{
    public class MockDbConnection : DbConnection
    {
        IMockCommandExecution exec;
        ConnectionState state;
        internal MockDbConnection(IMockCommandExecution exec)
        {
            this.exec = exec;
        }
        protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
        {
            throw new NotImplementedException();
        }

        public override void ChangeDatabase(string databaseName)
        {
            throw new NotImplementedException();
        }

        public override void Close()
        {
            this.state = ConnectionState.Closed;
        }

        public override string ConnectionString
        {
            get;
            set;
        }

        protected override DbCommand CreateDbCommand()
        {
            var cmd = new MockDbCommand(this.exec);
            cmd.Connection = this;
            return cmd;
        }

        public override string DataSource
        {
            get { return String.Empty; }
        }

        public override string Database
        {
            get { return String.Empty; }
        }

        public override void Open()
        {
            this.state = ConnectionState.Open;
        }

        public override string ServerVersion
        {
            get { return String.Empty; }
        }

        public override ConnectionState State
        {
            get { return state; }
        }
    }
}
