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
        private Dictionary<string, List<string>> Queries { get; set; }

        public BaseBuilder()
        {
            if (!Directory.Exists("Sql"))
                Directory.CreateDirectory("Sql");

            Prepared = new List<PreparedStatements>();
            Queries = new Dictionary<string, List<string>>();
        }

        public virtual BaseBuilder AddStatement(RawStatement statement)
        {
            if(!Prepared.Any((x) => x.Key == statement.Key))
            {
                int indicies = GetIndicies(statement.Query, out string query);

                Prepared.Add(new PreparedStatements(statement.Key, query, indicies, new List<string>()));
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

        public virtual BaseBuilder AddQuery(string key, string statement, params string[] values)
        {
            if(!Queries.ContainsKey(key))
            {
                Queries.Add(key, new List<string>());
            }

            Queries[key].Add(ParseStatement(statement, values));

            return this;
        }

        public string ParseStatement(string raw, params string[] values)
        {
            int j = 0;
            string ready = "";

            for(int i = 0; i < raw.Length; i++)
            {
                if(raw[i] == '?')
                {
                    ready += $"{values[j]}";
                    j++;
                }
                else
                {
                    ready += raw[i];
                }
            }

            return ready + ";\n";
        }

        private int GetIndicies(string raw, out string formatted)
        {
            int j = 0;
            string ready = "";

            for (int i = 0; i < raw.Length; i++)
            {
                if (raw[i] == '?')
                {
                    j++;
                }
                else
                {
                    ready += raw[i];
                }
            }

            formatted = ready;

            return j;
        }

        public async virtual void WriteToFiles()
        {
            foreach(var item in Prepared)
            {
                string currentName = "Sql\\" + DateTime.Now.ToString("dd-MM-yy_hh-mm-ss") + $"_{item.Key}.sql";

                File.WriteAllLines(currentName, await item.MakeAsync());
            }

            foreach(var item in Queries)
            {
                string currentName = "Sql\\" + DateTime.Now.ToString("dd-MM-yy_hh-mm-ss") + $"_{item.Key}.sql";

                foreach(var query in item.Value)
                {
                    File.AppendAllText(currentName, query);
                }
            }
        }
    }
}
