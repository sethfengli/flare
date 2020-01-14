using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipStateTracker
{
    public interface IReporter
    {
        void Write(string log);
        void WriteLine(string log);

    }
    public class Reporter : IReporter
    {
        public void Write (string log)
        {
            Console.Write(log);
        }

        public void WriteLine(string log)
        {
            Console.WriteLine(log);
        }
    }
}
