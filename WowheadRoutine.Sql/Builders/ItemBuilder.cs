using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowheadRoutine.Sql.Contracts;

namespace WowheadRoutine.Sql.Builders
{
    public class ItemBuilder : BaseBuilder, ISqlBuilder
    {
        /// <summary>
        /// Item information database builder, child from <see cref="BaseBuilder"/>
        /// </summary>
        public ItemBuilder() : base() { }
    }
}
