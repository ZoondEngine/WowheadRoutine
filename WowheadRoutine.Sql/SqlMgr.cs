using WowheadRoutine.Sql.Builders;
using WowheadRoutine.Sql.Contracts;
using WowheadRoutine.Support;

namespace WowheadRoutine.Sql
{
    public enum SqlBuildTypes
    {
        Creatures = 0,
        Quests = 1,
        Gameobjects = 2,
        Item = 3,
    }

    public class SqlMgr : Singleton<SqlMgr>
    {
        public static ISqlBuilder Create(SqlBuildTypes type)
        {
            ISqlBuilder builder = null;

            switch(type)
            {
                case SqlBuildTypes.Creatures:
                    {

                        break;
                    }

                case SqlBuildTypes.Gameobjects:
                    {

                        break;
                    }

                case SqlBuildTypes.Item:
                    {

                        break;
                    }

                case SqlBuildTypes.Quests:
                    {
                        builder = new QuestBuilder();

                        break;
                    }
            }

            return builder;
        }
    }
}
