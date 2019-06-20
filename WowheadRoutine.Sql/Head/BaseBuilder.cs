using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using WowheadRoutine.Sql.Datatypes;

namespace WowheadRoutine.Sql.Builders
{
    public class BaseBuilder
    {
        private List<PreparedStatements> Prepared { get; set; }

        public BaseBuilder()
        {
            if (!Directory.Exists("Sql"))
                Directory.CreateDirectory("Sql");

            Prepared = new List<PreparedStatements>();
        }

        public virtual BaseBuilder AddStatement(RawStatement statement)
        {
            if(!Prepared.Any((x) => x.Key == statement.Key))
            {
                Prepared.Add(new PreparedStatements(statement.Key, statement.Query, GetIndicies(statement.Query), new List<string>()));
            }

            return this;
        }

        public virtual BaseBuilder AddValues(string key, params string[] values)
        {
            if(Prepared.Any((x) => x.Key == key))
            {
                Prepared.First((x) => x.Key == key).AddValue(values);
            }

            return this;
        }

        private int GetIndicies(string raw)
        {
            return raw.Count((x) => x == '?');

            /* TODO: Remove
            int j = 0;
            
            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i] == '?')
                {
                    j++;
                }
            }

            return j;
            */
        }
        public async virtual void WriteToFiles()
        {
            foreach(var item in Prepared)
            {
                string currentName = "Sql\\" + DateTime.Now.ToString("dd-MM-yy_hh-mm-ss") + $"_{item.Key}.sql";

                File.WriteAllLines(currentName, await item.MakeAsync());
            }
        }
    }
}
