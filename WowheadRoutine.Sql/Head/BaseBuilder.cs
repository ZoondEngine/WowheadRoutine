using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using WowheadRoutine.Sql.Contracts;

namespace WowheadRoutine.Sql.Builders
{
    public enum SqlAction
    {
        Creatures_QuestEnder,
        Creatures_QuestStarter,
    }

    public class BaseBuilder : ISqlBuilder
    {
        private SqlAction CurrentAction { get; set; }
        private List<string> AlreadyBuildedQueries { get; set; } = new List<string>();

        protected BaseBuilder()
        {
            if (!Directory.Exists("Sql"))
                Directory.CreateDirectory("Sql");
        }

        protected virtual string MakeQuery(string statement, string[] data)
        {
            string ready = "";
            for (int i = 0; i < statement.Length; i++)
            {
                if (statement[i] == '?')
                {
                    ready += data[i];
                }
                else
                {
                    ready += statement[i];
                }
            }

            return ready;
        }
        protected virtual bool IsContainsNeedValues(string statement, int valuesCount)
        {
            int j = 0;
            for (int i = 0; i < statement.Length; i++)
            {
                if (statement[i] == '?')
                {
                    j++;
                }
            }

            return valuesCount == j;
        }
        protected virtual void WriteToFile(List<string> queries, string whatIsIt)
        {
            using (var file = File.OpenWrite(DateTime.Now.ToString("dd/MM/yy_hh-mm-ss") + $"_{whatIsIt}.sql"))
            {
                foreach(var query in queries)
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(query);

                    file.Write(buffer, 0, buffer.Length);
                }
            }
        }

        public void Make(SqlAction act)
        {
            CurrentAction = act;
        }

        public ISqlBuilder Append(string statement, params object[] values)
        {
            if (IsContainsNeedValues(statement, values.Length))
            {
                AlreadyBuildedQueries.Add(MakeQuery(statement, (string[])values));
            }

            return this;
        }

        public void Build()
        {
            if (AlreadyBuildedQueries.Count > 0)
            {
                WriteToFile(AlreadyBuildedQueries, CurrentAction.ToString());
            }
        }
    }
}
