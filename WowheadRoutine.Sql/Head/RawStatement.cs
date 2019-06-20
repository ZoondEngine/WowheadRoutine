using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowheadRoutine.Sql
{
    public class RawStatement
    {
        public string Query { get; private set; }
        public string Key   { get; private set; }

        public RawStatement(string query, string key)
        {
            Query = query;
            Key   = key;
        }
    }
}
