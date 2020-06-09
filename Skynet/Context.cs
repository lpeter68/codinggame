using Skynet.common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skynet
{
    public class Context
    {
        public Graph Graph { get; set; }
        public List<int> Exits { get; set; }

        public int SkynetNode { get; set; }

        public Context()
        {
            Exits = new List<int>();
        }
    }
}
