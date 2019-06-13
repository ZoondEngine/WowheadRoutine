using AngleSharp;
using WowheadRoutine.Assert;
using WowheadRoutine.Assert.Providers;
using WowheadRoutine.Sql;
using WowheadRoutine.Sql.Contracts;

namespace WowheadRoutine.Snippets
{
    public class BaseSnippet
    {
        protected virtual ISqlBuilder CreateBuilder(SqlBuildTypes type) => SqlMgr.Create(type);
        protected virtual IBrowsingContext CreateBrowsing()
        {
            return BrowsingContext.New(Configuration.Default.WithDefaultLoader());
        }
        protected virtual BaseOut GetConsole()
        {
            return OutMgr.CreateConsole();
        }
    }
}
