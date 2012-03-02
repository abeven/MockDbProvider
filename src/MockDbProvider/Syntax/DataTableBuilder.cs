using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace MockDbProvider.Syntax
{
    public class DataTableBuilder
    {
        DataTable _table;
        public DataTableBuilder()
        {
            this._table = new DataTable();
        }

        public DataTable DataTable
        {
            get
            {
                return _table;
            }
        }
        public DataTableBuilder AddColumn(string name, Type type) {
            this._table.Columns.Add(name, type);
            return this;
        }

        public DataTableBuilder AddRow(params object[] data)
        {
            this._table.Rows.Add(data);
            return this;
        }
    }
}
