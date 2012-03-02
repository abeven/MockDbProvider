using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace MockDbProvider
{
    public class MockDbDataAdapter : DbDataAdapter
    {
        public MockDbDataAdapter(MockDbProviderFactory provider)
            :base()
        {
            this.SelectCommand = provider.CreateCommand();
            this.UpdateCommand = provider.CreateCommand();
            this.InsertCommand = provider.CreateCommand();
            this.DeleteCommand = provider.CreateCommand();
        }
    }
}
