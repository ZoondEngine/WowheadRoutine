using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WowheadRoutine.Assert.Providers
{
    internal class FileOut : BaseOut
    {
        public FileOut()
        {
            if (!Directory.Exists("Logs"))
                Directory.CreateDirectory("Logs");

            foreach(var type in Enum.GetNames(typeof(OutLevel)))
            {
                if (!File.Exists($"Logs\\{type}.txt"))
                    File.Create($"Logs\\{type}.txt").Close();
            }
        }

        public override void Write(string msg, OutLevel level)
        {
            using (var file = File.OpenWrite($"Logs\\{level.ToString()}.txt"))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(FillMsg(msg));

                file.Write(buffer, 0, buffer.Length);
            }
        }

        public override void WriteLine(string msg, OutLevel level)
        {
            Write(msg + Environment.NewLine, level);
        }

        protected override string GetTimestamp()
        {
            return DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        }

        protected string FillMsg(string msg)
        {
            return $"[{GetTimestamp()}] > {msg}";
        }
    }
}
