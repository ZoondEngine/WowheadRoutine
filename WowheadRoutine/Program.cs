using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WowheadRoutine.Assert;
using WowheadRoutine.Commands;
using WowheadRoutine.Snippets;

namespace WowheadRoutine
{
    class Program
    {
        static void Main(string[] args)
        {
            SnippetMgr.Instance.OnInitialize += SnippetMgr_Initialize;
            SnippetMgr.Instance.OnLoaded += SnippetMgr_Loaded;
            SnippetMgr.Instance.OnSnippetCalled += SnippetMgr_SnippetCalled;
            SnippetMgr.Instance.Boot();

            CommandMgr.Instance.OnInitialize += CommandMgr_Initialize;
            CommandMgr.Instance.OnLoaded += CommandMgr_Loaded;
            CommandMgr.Instance.OnCommandCalled += CommandMgr_CmdCalled;
            CommandMgr.Instance.Boot();

            CommandMgr.Instance.Enable();
        }

        private static void SnippetMgr_SnippetCalled(ISnippet snippet)
        {
            
        }

        private static void SnippetMgr_Loaded(int count)
        {
            OutMgr.Instance.WriteLine($"Snippet manager succefully loaded '{count}' snippets", OutLevel.Debug);
        }

        private static void SnippetMgr_Initialize()
        {
            OutMgr.Instance.WriteLine($"Snippet manager start initializing", OutLevel.Debug);
        }

        private static void CommandMgr_CmdCalled(Commands.Contracts.ICommand command)
        {
            
        }

        private static void CommandMgr_Loaded(int count)
        {
            OutMgr.Instance.WriteLine($"Command manager succefully loaded '{count}' commands", OutLevel.Debug);
        }

        private static void CommandMgr_Initialize()
        {
            OutMgr.Instance.WriteLine($"Command manager start initializing", OutLevel.Debug);
        }
    }
}
