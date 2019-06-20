using System.Collections.Generic;
using System.Linq;
using WowheadRoutine.Assert.Providers;
using WowheadRoutine.Support;

namespace WowheadRoutine.Assert
{
    public enum OutLevel
    {
        Info = 0,
        Debug = 1,
        Warning = 2,
        Error = 3
    }

    public class OutMgr : Singleton<OutMgr>
    {
        private readonly List<BaseOut> RegisteredOuts = new List<BaseOut>();

        public void Boot()
        {
            /**
             * Bootstrapped default components
             */
            RegisteredOuts.Add(new BaseOut()); // MUST BE A FIRST IN THIS LIST!
            RegisteredOuts.Add(new FileOut());

            WriteLine($"Registered '{RegisteredOuts.Count}' assert providers for sync job", OutLevel.Debug);
        }

        public void MakeOtherOut<T>() where T : BaseOut, new()
        {
            if (!RegisteredOuts.Any((x) => x.GetType() == typeof(T)))
            {
                RegisteredOuts.Add(new T());
            }
        }

        public BaseOut GetDefaultOut()
        {
            if (RegisteredOuts.Count <= 0) Boot();

            return RegisteredOuts[0];
        }
        public static BaseOut CreateConsole() => Instance.GetDefaultOut();
        public void Write(string msg, OutLevel level) => RegisteredOuts.ForEach((x) => x.Write(msg, level));
        public void WriteLine(string msg, OutLevel level) => RegisteredOuts.ForEach((x) => x.WriteLine(msg, level));
    }
}
