using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_busters.Actions
{
    public class Bust : IAction
    {
        private int TargetId { get; set; }

        public Bust(int id)
        {
            TargetId = id;
        }

        public void Do(string message = "")
        {
            Console.WriteLine("BUST " + TargetId + " " + message);
        }
    }
}
