using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_busters.Actions
{
    public class Release : IAction
    {
        public Release() { }

        public void Do(string message = "")
        {
            Console.WriteLine("RELEASE " + message);
        }
    }
}
