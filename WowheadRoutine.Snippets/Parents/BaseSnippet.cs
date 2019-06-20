using AngleSharp;
using WowheadRoutine.Assert;
using WowheadRoutine.Assert.Providers;
using WowheadRoutine.Sql;
using WowheadRoutine.Sql.Builders;

namespace WowheadRoutine.Snippets
{
    public class BaseSnippet
    {
        protected virtual BaseBuilder GetBuilder() => SqlMgr.Instance.GetBuilder();
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
