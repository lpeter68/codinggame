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
            IAction action = null;
            if (Buster.StunAvailableIn <= 0)
            {
                var a = gameContext.OppositeBusters.FirstOrDefault(b => b.Position.GetDist(Buster.Position) < 1760);
                if (a != null)
                {
                    action = new Stun(a);
                    Buster.StunAvailableIn = 20;
                }
            }
            if (action == null)
            {
                if (Buster.Position.GetDist(gameContext.GetQG()) < 1600)
                {
                    gameContext.Score++;
                    action = new Release();
                }
                else
                {
                    action = new Move(gameContext.GetQG());
                }
            }
            action.Do();
        }
    }
}
