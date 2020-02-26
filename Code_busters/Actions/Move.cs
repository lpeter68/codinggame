using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_busters.Actions
{
    public class Move : IAction
    {
        public Point Target { get; set; }

        public Move(int x, int y)
        {
            Target = new Point(x, y);
        }

        public Move(Point target)
        {
            Target = target;
        }

        public void Do()
        {
            Console.WriteLine("MOVE " + Target);
        }
    }
}
