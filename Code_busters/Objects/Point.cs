using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_busters
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int GetDist(Point point)
        {
            return (int)Math.Sqrt(Math.Pow(Math.Abs(X - point.X), 2) + Math.Pow(Math.Abs(Y - point.Y), 2));
        }

        public int GetDist(int x, int y)
        {
            return GetDist(new Point(x, y));
        }


        public override String ToString()
        {
            return X + " " + Y;
        }
    }
}
