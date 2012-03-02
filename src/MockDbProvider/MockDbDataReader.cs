using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Collections;

namespace MockDbProvider
{
    public class MockDbDataReader : DbDataReader
    {
        DbDataReader inner;
        public MockDbDataReader(DbDataReader inner)
        {
            this.inner = inner;
        }
        public override void Close()
        {
            this.inner.Close();
        }

        public override int Depth
        {
            get { return this.inner.Depth; }
        }

        public override int FieldCount
        {
            get { return this.inner.FieldCount; }
        }

        public override bool GetBoolean(int ordinal)
        {
            return this.inner.GetBoolean(ordinal);
        }

        public override byte GetByte(int ordinal)
        {
            return this.inner.GetByte(ordinal);
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            return this.inner.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        public override char GetChar(int ordinal)
        {
            return this.inner.GetChar(ordinal);
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            return this.inner.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
        }

        public override string GetDataTypeName(int ordinal)
        {
            return this.inner.GetDataTypeName(ordinal);
        }

        public override DateTime GetDateTime(int ordinal)
        {
            return this.inner.GetDateTime(ordinal);
        }

        public override decimal GetDecimal(int ordinal)
        {
            return this.inner.GetDecimal(ordinal);
        }

        public override double GetDouble(int ordinal)
        {
            return this.GetDouble(ordinal);
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            return ((IEnumerable)this.inner).GetEnumerator();
        }

        public override Type GetFieldType(int ordinal)
        {
            return this.inner.GetFieldType(ordinal);
        }

        public override float GetFloat(int ordinal)
        {
            return this.inner.GetFloat(ordinal);
        }

        public override Guid GetGuid(int ordinal)
        {
            return this.inner.GetGuid(ordinal);
        }

        public override short GetInt16(int ordinal)
        {
            return this.inner.GetInt16(ordinal);
        }

        public override int GetInt32(int ordinal)
        {
            return this.inner.GetInt32(ordinal);
        }

        public override long GetInt64(int ordinal)
        {
            return this.inner.GetInt64(ordinal);
        }

        public override string GetName(int ordinal)
        {
            return this.inner.GetName(ordinal);
        }

        public override int GetOrdinal(string name)
        {
            return this.inner.GetOrdinal(name);
        }

        public override System.Data.DataTable GetSchemaTable()
        {
            return this.inner.GetSchemaTable();
        }

        public override string GetString(int ordinal)
        {
            return this.inner.GetString(ordinal);
        }

        public override object GetValue(int ordinal)
        {
            return this.inner.GetValue(ordinal);
        }

        public override int GetValues(object[] values)
        {
            return this.inner.GetValues(values);
        }

        public override bool HasRows
        {
            get { return this.inner.HasRows; }
        }

        public override bool IsClosed
        {
            get { return this.inner.IsClosed; }
        }

        public override bool IsDBNull(int ordinal)
        {
            return this.inner.IsDBNull(ordinal);
        }

        public override bool NextResult()
        {
            return this.inner.NextResult();
        }

        public override bool Read()
        {
            return this.inner.Read();
        }

        public override int RecordsAffected
        {
            get { return this.inner.RecordsAffected; }
        }

        public override object this[string name]
        {
            get { return this.inner[name]; }
        }

        public override object this[int ordinal]
        {
            get { return this.inner[ordinal]; }
        }
    }
}
