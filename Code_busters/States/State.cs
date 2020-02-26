using Code_busters.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_busters
{
    public abstract class State
    {

        public Buster Buster { get; set; }

        public State(Buster buster)
        {
            Buster = buster;
        }

        public abstract void DoNextAction(GameContext gameContext);
    }
}
