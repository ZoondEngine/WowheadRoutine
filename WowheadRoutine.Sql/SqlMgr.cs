using WowheadRoutine.Sql.Builders;
using WowheadRoutine.Support;

namespace WowheadRoutine.Sql
{
    public class SqlMgr : Singleton<SqlMgr>
    {
        private BaseBuilder BuilderInstance { get; set; }

        public SqlMgr()
        {
            BuilderInstance = new BaseBuilder();
        }

        public BaseBuilder GetBuilder() => BuilderInstance;

        public void FastOnce(string key, string query, params string[] values)
        {
            GetBuilder().AddQuery(key, query, values);
        }
    }
}
