using WowheadRoutine.Sql.Builders;

namespace WowheadRoutine.Sql.Contracts
{
    public interface ISqlBuilder
    {
        void Make(SqlAction act);
        ISqlBuilder Append(string statement, params object[] values);
        void Build();
    }
}
