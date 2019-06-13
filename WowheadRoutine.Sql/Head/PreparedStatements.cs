using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowheadRoutine.Sql.Builders
{
    public struct PreparedStatements
    {
        public const string CREATURE_DEL_QUESTSTARTER = "DELETE FROM `creature_queststarter` WHERE `entry` = '?';";
        public const string CREATURE_DEL_QUESTENDER   = "DELETE FROM `creature_questender` WHERE `entry` = '?';";
        public const string CREATURE_INS_QUESTSTARTER = "INSERT INTO `creature_queststarter` (`entry`, `quest`) VALUES ('?', '?');";
        public const string CREATURE_INS_QUESTENDER   = "INSERT INTO `creature_questender` (`entry`, `quest`) VALUES ('?', '?')";
    }
}
