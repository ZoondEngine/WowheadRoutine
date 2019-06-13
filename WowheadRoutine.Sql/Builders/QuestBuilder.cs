using WowheadRoutine.Sql.Contracts;

namespace WowheadRoutine.Sql.Builders
{
    public class QuestBuilder : BaseBuilder, ISqlBuilder
    {
        /// <summary>
        /// Quests information database builder, child from <see cref="BaseBuilder"/>
        /// </summary>
        public QuestBuilder() : base() { }
    }
}
