using Skynet.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet
{
    public static class Algo2
    {

        private static List<int> siblingNodes = new List<int>();
        private static List<int> doubleSiblingNodes = new List<int>();

        public static void FillDoubleExitAccessNode(Context context)
        {
            siblingNodes = new List<int>();
            doubleSiblingNodes = new List<int>();
            foreach (var exit in context.Exits)
            {
                var siblings = context.Graph.GetSiblingNodes(exit);
                foreach (var sibling in siblings)
                {
                    if (siblingNodes.Contains(sibling))
                    {
                        doubleSiblingNodes.Add(sibling);
                    }
                    else
                    {
                        siblingNodes.Add(sibling);
                    }
                }
            }
        }

        public static Graph ReduceGraph(Context context)
        {
            FillDoubleExitAccessNode(context);
            var newGraph = new Graph(context.Graph.nodeNb);
            for (int i = 0; i < context.Graph.nodeNb; i++)
            {
                for (int j = 0; j < context.Graph.nodeNb; j++)
                {
                    if ((siblingNodes.Contains(i) || context.SkynetNode == i) && (siblingNodes.Contains(j) || context.SkynetNode == j) && context.Graph.GetLink(i, j) != 0)
                    {
                        newGraph.AddBidirectionnalLink(i, j);
                    }
                }
            }
            return newGraph;
        }

        public static string Play(Context context)
        {
            int closestExit = -1;
            double closestExitDist = -1;
            List<int> closetsExitPath = null;
            foreach (var exit in context.Exits)
            {
                var result = Dijkstra.Calculate(context.Graph, context.SkynetNode, exit);
                if (result.Path != null && (closestExit == -1 || result.Dist < closestExitDist))
                {
                    closestExit = exit;
                    closestExitDist = result.Dist;
                    closetsExitPath = result.Path;
                }
            }
            int node1 = -1;
            int node2 = -1;

            FillDoubleExitAccessNode(context);

            if (closestExitDist > 1 && doubleSiblingNodes.Any())
            {
                var newGraph = ReduceGraph(context);
                int closestSibling = -1;
                double closestSiblingDist = -1;
                List<int> closetsSiblingPath = null;
                foreach (var doubleSibling in doubleSiblingNodes)
                {
                    var result = Dijkstra.Calculate(newGraph, context.SkynetNode, doubleSibling);
                    if (result.Path != null && (closestSibling == -1 || result.Dist < closestSiblingDist))
                    {
                        closestSibling = doubleSibling;
                        closestSiblingDist = result.Dist;
                        closetsSiblingPath = result.Path;
                    }
                }
                if (closestSibling != -1)
                {
                    var exitRelated = context.Graph.GetSiblingNodes(closestSibling).Intersect(context.Exits).First();
                    node1 = closestSibling;
                    node2 = exitRelated;
                }
            }

            if (node1 == -1 || node2 == -1)
            {
                var exitIndex = closetsExitPath.Count() - 1;
                node1 = exitIndex >= 1 ? closetsExitPath[exitIndex - 1] : context.SkynetNode;
                node2 = closetsExitPath[exitIndex];
            }
            context.Graph.DeleteBidirectionnalLink(node1, node2);
            return node1 + " " + node2;
        }
    }
}
