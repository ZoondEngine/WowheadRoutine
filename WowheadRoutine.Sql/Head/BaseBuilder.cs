using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WowheadRoutine.Assert;
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
            int j = 0;
            for (int i = 0; i < statement.Length; i++)
            {
                if (statement[i] == '?')
                {
                    ready += data[j];
                    j++;
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
            using (var file = File.OpenWrite("Sql\\" + DateTime.Now.ToString("dd-MM-yy_hh-mm-ss") + $"_{whatIsIt}.sql"))
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
                string[] arr = ((IEnumerable)values).Cast<object>()
                                 .Select(x => x.ToString())
                                 .ToArray();

                AlreadyBuildedQueries.Add(MakeQuery(statement, arr) + Environment.NewLine);
            }
            else
            {
                OutMgr.Instance.WriteLine($"Statement: '{statement}' not contains is needed values. Received count: '{values.Length}'", OutLevel.Error);
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
