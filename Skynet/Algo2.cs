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

        public static void DivideDualAccess(Context context)
        {
            Dictionary<int, int> siblingNodes = new Dictionary<int, int>();
            foreach (var exit in context.Exits)
            {
                var sib = context.Graph.GetSiblingNodes(exit);
                foreach (var item in sib)
                {
                    if (siblingNodes.ContainsKey(item))
                    {
                        context.Graph.AddBidirectionnalLink(item, exit, 0.5);
                        context.Graph.AddBidirectionnalLink(siblingNodes[item], exit, 0.5);
                    }
                    else
                    {
                        siblingNodes.Add(item, exit);
                    }
                }
            }
        }

        public static string Play(Context context)
        {
            DivideDualAccess(context);
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
            var exitIndex = closetsExitPath.Count() - 1;
            var node1 = exitIndex >= 1 ? closetsExitPath[exitIndex - 1] : context.SkynetNode;
            var node2 = closetsExitPath[exitIndex];
            context.Graph.DeleteBidirectionnalLink(node1, node2);
            return node1 + " " + node2;
        }
    }
}
