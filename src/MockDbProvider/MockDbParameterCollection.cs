using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;

namespace MockDbProvider
{
    public class MockDbParameterCollection : DbParameterCollection
    {
        List<MockDbParameter> inner;
        object sync;

        internal MockDbParameterCollection()
        {
            inner = new List<MockDbParameter>();
            sync = new object();
        }
        public override int Add(object value)
        {
            this.inner.Add((MockDbParameter)value);
            return this.inner.Count;
        }

        public override void AddRange(Array values)
        {
            this.inner.AddRange(values.Cast<MockDbParameter>());
        }

        public override void Clear()
        {
            this.inner.Clear();
        }

        public override bool Contains(string value)
        {
            return this.inner.Count(c => c.ParameterName == value) > 0;
        }

        public override bool Contains(object value)
        {
            return this.inner.Count(c => c.Value == value) > 0;
        }

        public override void CopyTo(Array array, int index)
        {
            Array.Copy(this.inner.ToArray(), index, array, 0, array.Length);
        }

        public override int Count
        {
            get { return this.inner.Count; }
        }

        public override System.Collections.IEnumerator GetEnumerator()
        {
            return this.inner.GetEnumerator();
        }

        protected override DbParameter GetParameter(string parameterName)
        {
            return this.inner.Single(x => x.ParameterName == parameterName);
        }

        protected override DbParameter GetParameter(int index)
        {
            return this.inner[index];
        }

        public override int IndexOf(string parameterName)
        {
            return this.inner.IndexOf(inner.Single(x => x.ParameterName == parameterName));
        }

        public override int IndexOf(object value)
        {
            return this.inner.IndexOf(inner.Single(x => x.Value == value));
        }

        public override void Insert(int index, object value)
        {
            this.inner.Insert(index, (MockDbParameter)value);
        }

        public override bool IsFixedSize
        {
            get { return false; }
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override bool IsSynchronized
        {
            get { return false; }
        }

        public override void Remove(object value)
        {
            this.inner.Remove((MockDbParameter)value);
        }

        public override void RemoveAt(string parameterName)
        {
            this.inner.Remove(this.inner.Single(x => x.ParameterName == parameterName));
        }

        public override void RemoveAt(int index)
        {
            this.inner.RemoveAt(index);
        }

        protected override void SetParameter(string parameterName, DbParameter value)
        {
            var p = this.inner.Single(x => x.ParameterName == parameterName);
            p.ParameterName = value.ParameterName;
            p.Value = value.Value;
            
        }

        protected override void SetParameter(int index, DbParameter value)
        {
            this.SetParameter(this.inner[index].ParameterName, value);
        }

        public override object SyncRoot
        {
            get { return sync; }
        }
    }
}
