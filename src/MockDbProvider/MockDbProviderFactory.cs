using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace MockDbProvider
{
    using Syntax;
    public class MockDbProviderFactory : DbProviderFactory, IMockCommandExecution
    {
        public static MockDbProviderFactory Instance = new MockDbProviderFactory();

        private List<Syntax.MockCommandBehavior> behaviors;

        public MockDbProviderFactory()
        {
            this.behaviors = new List<Syntax.MockCommandBehavior>();
        }
        public void AddBehavior(Syntax.MockCommandBehavior behavior)
        {
            this.behaviors.Add(behavior);
        }

        public override DbConnection CreateConnection()
        {
            return new MockDbConnection(this);
        }
        public override DbCommand CreateCommand()
        {
            return new MockDbCommand(this);
        }
        public override DbCommandBuilder CreateCommandBuilder()
        {
            return new MockDbCommandBuilder();
        }
        public override DbDataAdapter CreateDataAdapter()
        {
            return new MockDbDataAdapter(this);
        }
        public override DbParameter CreateParameter()
        {
            return new MockDbParameter();
        }
        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return new MockDbConnectionStringBuilder();
        }

        public override bool CanCreateDataSourceEnumerator
        {
            get
            {
                return false;
            }
        }

        private MockCommandBehavior FindBehavior(MockDbCommand cmd)
        {
            foreach (var behavior in this.behaviors)
                if (behavior.Matches(cmd))
                    return behavior;
            throw new InvalidOperationException(String.Format("Could not find behavior for command '{0}'", cmd.CommandText));
        }
        int IMockCommandExecution.ExecuteNonQuery(MockDbCommand cmd)
        {
            return (int)FindBehavior(cmd).ReturnValue;
        }

        object IMockCommandExecution.ExecuteScalar(MockDbCommand cmd)
        {
            return FindBehavior(cmd).ReturnValue;
        }

        MockDbDataReader IMockCommandExecution.ExecuteDataReader(MockDbCommand cmd)
        {            
            return new MockDbDataReader(((DataTable)FindBehavior(cmd).ReturnValue).CreateDataReader());
        }

        public void ClearBehaviors()
        {
            this.behaviors.Clear();
        }
    }
}
