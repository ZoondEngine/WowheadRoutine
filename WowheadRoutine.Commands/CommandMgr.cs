using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WowheadRoutine.Assert;
using WowheadRoutine.Commands.Contracts;
using WowheadRoutine.Support;

namespace WowheadRoutine.Commands
{
    public delegate void CommandMgr_Init();
    public delegate void CommandMgr_Loaded(int count);
    public delegate void CommandMgr_Call(ICommand command);

    public class CommandMgr : Singleton<CommandMgr>
    {
        public event CommandMgr_Init   OnInitialize;
        public event CommandMgr_Loaded OnLoaded;
        public event CommandMgr_Call   OnCommandCalled;

        private List<ICommand> AvailableCommands { get; set; } = new List<ICommand>();
        private bool IsListening { get; set; }

        public void Boot()
        {
            OnInitialize?.Invoke();

            AvailableCommands = Load();

            OnLoaded?.Invoke(AvailableCommands.Count);
        }

        private List<ICommand> Load()
        {
            var console = OutMgr.CreateConsole();

            if (!Directory.Exists("Commands"))
            {
                console.WriteLine("Directory 'Commands' is empty. New folder has been created.", OutLevel.Warning);

                Directory.CreateDirectory("Commands");
            }

            List<ICommand> snippets = new List<ICommand>();

            var assemblies = Directory.GetFiles("Commands");
            if (assemblies.Length > 0)
            {
                foreach (var assembly in assemblies)
                {
                    try
                    {
                        var ass = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + assembly);
                        var type = ass.GetTypes().Where((x) => typeof(ICommand).IsAssignableFrom(x)).Single();
                        var instance = (ICommand)type.GetConstructor(Type.EmptyTypes)?.Invoke(null);
                        snippets.Add(instance);
                    }
                    catch (Exception ex)
                    {
                        console.WriteLine($"Error occured while loading command: '{assembly}'", OutLevel.Error);
                        console.Except(ex);
                    }
                }
            }
            else
            {
                console.WriteLine("Not have a commands for loading", OutLevel.Warning);
            }

            return snippets;
        }

        public List<ICommand> GetAllCommands() => AvailableCommands;

        public void Enable()
        {
            IsListening = true;

            Read();
        }

        public void Disable()
        {
            IsListening = false;
        }

        private void Read()
        {
            while(IsListening)
            {
                string raw = Console.ReadLine();
                bool suitable = false;

                foreach (var cmd in AvailableCommands)
                {
                    if (cmd.IsSuitableHandler(raw))
                    {
                        OnCommandCalled?.Invoke(cmd);

                        suitable = true;
                        cmd.Run(raw);
                    }
                }

                if (!suitable)
                {
                    OutMgr.Instance.WriteLine($"Incorrect command typed: '{raw}'. Use .help for more details", OutLevel.Error);
                }
            }
        }
    }
}
