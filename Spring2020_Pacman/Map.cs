
using Spring2020_Pacman.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring2020_Pacman
{
    public class Map
    {
        private int[,] map;
        public int Width { get; set; }
        public int Height { get; set; }

        public Map(int width, int height)
        {
            map = new int[width, height];
            Width = width;
            Height = height;
        }

        public void SetCase(int x, int y, int content)
        {
            map[x, y] = content;
        }

        public Graph GenerateGraph()
        {
            var graph = new Graph(Width + Height);
            var nodeNb = 0;
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    if (map[x, y] == 0) nodeNb++;
                }
            }
            return graph;
        }
    }
}
