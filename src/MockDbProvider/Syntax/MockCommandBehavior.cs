using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;

namespace MockDbProvider.Syntax
{

    public class MockCommandBehavior
    {
        Predicate<DbCommand> match;
        internal object ReturnValue { get; private set; }

        internal bool Matches(DbCommand cmd)
        {
            return match(cmd);
        }

        public MockCommandBehavior When(Predicate<DbCommand> match)
        {
            this.match = match;
            return this;
        }

        public MockCommandBehavior ReturnsData(DataTable dt)
        {
            this.ReturnValue = dt;
            return this;
        }

        public MockCommandBehavior ReturnsScalar(object value)
        {
            this.ReturnValue = value;
            return this;
        }
    }
}
