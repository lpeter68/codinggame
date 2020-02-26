using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_busters.Objects
{
    public abstract class BaseObject
    {
        public Point Position { get; set; }
        public int Id { get; set; }
        public int EntityType { get; set; }
        public virtual int State { get; set; }
        public int Value { get; set; }
    }
}
