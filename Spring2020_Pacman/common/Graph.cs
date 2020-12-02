using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring2020_Pacman.common
{
    public class Graph
    {
        public int nodeNb { get; set; }
        private double[,] map;

        public Graph(int nodesNb)
        {
            nodeNb = nodesNb;
            map = new double[nodesNb, nodesNb];
            for (int i = 0; i < nodesNb; i++)
            {
                for (int j = 0; j < nodesNb; j++)
                {
                    map[i, j] = 0;
                }
            }
        }

        public void AddLink(int node1, int node2, double weight = 1)
        {
            map[node1, node2] = weight;
        }

        public void AddBidirectionnalLink(int node1, int node2, double weight = 1)
        {
            AddLink(node1, node2, weight);
            AddLink(node2, node1, weight);
        }

        public void DeleteBidirectionnalLink(int node1, int node2)
        {
            map[node1, node2] = 0;
            map[node2, node1] = 0;
        }

        public void DeleteLink(int node1, int node2)
        {
            map[node1, node2] = 0;
        }

        public double GetLink(int node1, int node2)
        {
            return map[node1, node2];
        }

        public List<int> GetSiblingNodes(int node)
        {
            List<int> result = new List<int>();
            for (int i = 0; i < nodeNb; i++)
            {
                if (map[node, i] > 0 && i != node)
                {
                    result.Add(i);
                }
            }
            return result;
        }
    }
}
