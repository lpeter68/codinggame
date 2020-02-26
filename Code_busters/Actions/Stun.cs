using Code_busters.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_busters.Actions
{
    public class Stun : IAction
    {
        public Buster Target { get; set; }

        public Stun(Buster target)
        {
            Target = target;
        }

        public void Do(string message = "")
        {
            Console.WriteLine("STUN " + Target.Id + " " + message);
        }
    }
}
