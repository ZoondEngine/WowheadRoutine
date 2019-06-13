using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowheadRoutine.Sql.Contracts;

namespace WowheadRoutine.Sql.Builders
{
    public class CreaturesBuilder : BaseBuilder, ISqlBuilder
    {
        /// <summary>
        /// Creatures database builder, child from <see cref="BaseBuilder"/>
        /// </summary>
        public CreaturesBuilder() : base() { }
    }
}
