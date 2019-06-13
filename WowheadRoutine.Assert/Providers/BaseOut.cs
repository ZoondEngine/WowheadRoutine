using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WowheadRoutine.Assert.Providers
{
    public class BaseOut
    {
        public virtual void Write(string msg, OutLevel level)
        {
            ChangeColor(level);

            Console.Write(FillMsg(msg, GetTimestamp()));

            ResetColors();
        }

        public virtual void WriteLine(string msg, OutLevel level)
        {
            ChangeColor(level);

            Write(msg + Environment.NewLine, level);

            ResetColors();
        }

        public virtual void Except(Exception ex, [CallerMemberName] string callerName = "", [CallerLineNumber] int callerLine = 0)
        {
            WriteLine(FillMsg($"'{ex.ToString()}' In: {callerName} Line: {callerLine}", GetTimestamp()), OutLevel.Error);
        }

        protected virtual string GetTimestamp()
        {
            return DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
        }

        protected virtual string FillMsg(string msg, string timestamp)
        {
            return $"[{timestamp}] > {msg}";
        }

        private void ChangeColor(OutLevel level)
        {
            switch (level)
            {
                case OutLevel.Debug:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case OutLevel.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case OutLevel.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case OutLevel.Info:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
        }

        private void ResetColors()
        {
            Console.ResetColor();
        }
    }
}
