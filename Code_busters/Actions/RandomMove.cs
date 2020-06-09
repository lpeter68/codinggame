using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_busters.Actions
{
    public class RandomMove : IAction
    {
        public Point Target { get; set; }

        public RandomMove()
        {
            var x = new Random().Next(0, 3);
            switch (x)
            {
                case 0: Target = new Point(0, 9000); break;
                case 1: Target = new Point(16000, 0); break;
                case 2: Target = new Point(8000, 4500); break;
            }
        }

        public void Do(string message = "")
        {
            Console.WriteLine("MOVE " + Target + " " + message);
        }
    }
}
