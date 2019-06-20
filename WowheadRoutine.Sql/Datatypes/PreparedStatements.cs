using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WowheadRoutine.Sql.Datatypes
{
    public class PreparedStatements : Tuple<string, string, int, List<string>>
    {
        public PreparedStatements(string statementKey, string query, int indicies, List<string> values)
            : base(statementKey, query, indicies, values) { }

        public string Key
        {
            get
            {
                return Item1;
            }
        }
        public int IndiciesCount
        {
            get
            {
                return Item3;
            }
        }
        public List<string> ValuesList
        {
            get
            {
                return Item4;
            }
        }
        public string Query
        {
            get
            {
                return Item2;
            }
        }

        private string this[int index]
        {
            get
            {
                if (ValuesList.Count > index)
                {
                    return ValuesList[index];
                }
                else
                    throw new ArgumentOutOfRangeException($"This statements collection not contains the '{index}' count indicices!");
            }
        }

        public void AddValue(params string[] values)
        {
            if (values.Length == IndiciesCount)
            {
                string ready = "(";
                for(int i = 0; i < values.Length; i++)
                {
                    if(values[i] == values.Last())
                    {
                        ready += $"'{values[i]}')";
                    }
                    else
                    {
                        ready += $"'{values[i]}', ";
                    }
                }

                ValuesList.Add(ready);
            }
            else
                throw new FormatException($"Incorrect values count for adding to statement. Need: '{IndiciesCount}', received: {values.Length}");
        }

        public string[] Make()
        {
            string[] maked = new string[ValuesList.Count+1];
            maked[0] = Query;

            for(int i = 0; i < ValuesList.Count; i++)
            {
                maked[i + 1] = GetValuesByIndex(i) + ",";
            }

            maked[ValuesList.Count + 1] = maked.Last().Replace(',', ';');

            return maked;
        }

        public async Task<string[]> MakeAsync()
        {
            return await Task.Run(Make);
        }

        public void RemoveValues(int index) => ValuesList.Remove(GetValuesByIndex(index));

        public string GetValuesByIndex(int index) => this[index];
    }
}
