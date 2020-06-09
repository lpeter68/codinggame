using Skynet.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet
{
    public static class Algo1
    {

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
            context.Graph.DeleteBidirectionnalLink(context.SkynetNode, closetsExitPath.First());
            return context.SkynetNode + " " + closetsExitPath.First();
        }
    }
}
