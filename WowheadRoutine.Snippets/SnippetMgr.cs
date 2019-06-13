using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using WowheadRoutine.Assert;
using WowheadRoutine.Support;

namespace WowheadRoutine.Snippets
{
    public delegate void SnippetMgr_Init();
    public delegate void SnippetMgr_Loaded(int count);
    public delegate void SnippetMgr_Call(ISnippet snippet);
    public class SnippetMgr : Singleton<SnippetMgr>
    {
        public event SnippetMgr_Init OnInitialize;
        public event SnippetMgr_Loaded OnLoaded;
        public event SnippetMgr_Call OnSnippetCalled;

        private List<ISnippet> LoadedSnippets { get; set; } = new List<ISnippet>();

        public void Boot()
        {
            OnInitialize?.Invoke();

            LoadedSnippets = Load();

            OnLoaded?.Invoke(LoadedSnippets.Count);
        }

        private List<ISnippet> Load()
        {
            var console = OutMgr.CreateConsole();

            if (!Directory.Exists("Snippets"))
            {
                console.WriteLine("Directory 'Snippets' is empty. New folder has been created.", OutLevel.Warning);

                Directory.CreateDirectory("Snippets");
            }

            List<ISnippet> snippets = new List<ISnippet>();


            var assemblies = Directory.GetFiles("Snippets");
            if (assemblies.Length > 0)
            {
                foreach (var assembly in assemblies)
                {
                    try
                    {
                        var ass = Assembly.LoadFile(AppDomain.CurrentDomain.BaseDirectory + assembly);
                        var type = ass.GetTypes().Where((x) => typeof(ISnippet).IsAssignableFrom(x)).Single();
                        var instance = (ISnippet)type.GetConstructor(Type.EmptyTypes)?.Invoke(null);

                        instance.OnLoaded();
                        snippets.Add(instance);
                    }
                    catch (Exception ex)
                    {
                        console.WriteLine($"Error occured while loading snippet: '{assembly}'", OutLevel.Error);
                        console.Except(ex);
                    }
                }
            }
            else
            {
                console.WriteLine("Not have a snippet for loading", OutLevel.Warning);
            }

            return snippets;
        }

        public void Unload()
        {
            foreach(var snippet in LoadedSnippets)
            {
                snippet.OnUnloaded();
            }

            LoadedSnippets.Clear();
        }

        public void Call<T>(params object[] data)
        {
            bool result = false;

            foreach(var snippet in LoadedSnippets)
            {
                if(snippet.GetType() == typeof(T))
                {
                    OnSnippetCalled?.Invoke(snippet);

                    snippet.Run(data);
                    result = true;

                    break;
                }
            }

            if(!result)
            {
                OutMgr.CreateConsole().WriteLine("No snippet found to process this command", OutLevel.Error);
            }
        }
    }
}
