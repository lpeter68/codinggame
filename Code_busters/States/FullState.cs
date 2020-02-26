using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code_busters.Actions;
using Code_busters.Objects;

namespace Code_busters.States
{
    public class FullState : State
    {
        public FullState(Buster buster) : base(buster)
        {

        }

        public override void DoNextAction(GameContext gameContext)
        {
            if (Buster.Position.GetDist(gameContext.GetQG()) < 1600)
            {
                new Release().Do();
            }
            else
            {
                new Move(gameContext.GetQG()).Do();
            }
        }
    }
}
