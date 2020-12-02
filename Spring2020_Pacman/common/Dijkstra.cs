using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spring2020_Pacman.common
{
    public static class Dijkstra
    {
        public static DijkstraResult Calculate(Graph graph, int startNode, int endNode)
        {
            Dictionary<int, double> nodeDist = new Dictionary<int, double>();
            Dictionary<int, List<int>> nodePath = new Dictionary<int, List<int>>();
            List<int> nodeInProgress = new List<int>();

            nodeInProgress.Add(startNode);
            nodeDist.Add(startNode, 0);
            nodePath.Add(startNode, new List<int>());

            while (nodeInProgress.Any())
            {
                var currentNode = nodeInProgress.First();
                var currentDist = nodeDist[currentNode];
                var currentPath = nodePath[currentNode];

                var siblingsNode = graph.GetSiblingNodes(currentNode);

                foreach (var sibling in siblingsNode)
                {
                    var newDist = currentDist + graph.GetLink(currentNode, sibling);

                    if (!nodeDist.ContainsKey(sibling) || nodeDist[sibling] > newDist)
                    {
                        nodeDist[sibling] = newDist;

                        var newPath = new List<int>(currentPath);
                        newPath.Add(sibling);

                        nodePath[sibling] = newPath;

                        if (!nodeInProgress.Contains(sibling))
                        {
                            nodeInProgress.Add(sibling);
                        }
                    }
                }
                nodeInProgress.Remove(currentNode);
            }

            if (nodePath.ContainsKey(endNode))
            {
                return new DijkstraResult()
                {
                    Dist = nodeDist[endNode],
                    Path = nodePath[endNode]
                };
            }
            else
            {
                return new DijkstraResult()
                {
                    Dist = -1,
                    Path = null
                };
            }
        }
    }

    public class DijkstraResult
    {
        public List<int> Path { get; set; }
        public double Dist { get; set; }
    }
}
