using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MockDbProvider
{
    using Syntax;
    interface IMockCommandExecution
    {
        int ExecuteNonQuery(MockDbCommand cmd);
        object ExecuteScalar(MockDbCommand cmd);
        MockDbDataReader ExecuteDataReader(MockDbCommand cmd);
    }
}
