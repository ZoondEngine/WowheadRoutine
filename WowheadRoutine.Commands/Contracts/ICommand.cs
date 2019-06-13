using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowheadRoutine.Commands.Contracts
{
    public interface ICommand
    {
        bool IsSuitableHandler(string raw);
        void Run(params object[] args);
        string GetDescription();
    }
}
