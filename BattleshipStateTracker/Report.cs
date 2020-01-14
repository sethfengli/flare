using System;
using System.Collections.Generic;
using System.Text;

namespace BattleshipStateTracker
{
    public interface IReport
    {
        void Write(string log);
        void WriteLine(string log);

    }
    public class Report : IReport
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
