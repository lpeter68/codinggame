using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code_busters.Actions;
using Code_busters.Objects;

namespace Code_busters.States
{
    public class GuardState : State
    {
        public GuardState(Buster buster) : base(buster)
        {

        }

        public override void DoNextAction(GameContext gameContext)
        {
            IAction action = null;
            if (Buster.StunAvailableIn <= 0 && gameContext.OppositeBusters.Any(b => b.State == 1))
            {
                var a = gameContext.OppositeBusters.Where(b => b.State == 1).OrderBy(b => b.Position.GetDist(Buster.Position)).First();
                if (a.Position.GetDist(Buster.Position) < 1760)
                {
                    action = new Stun(a);
                    Buster.StunAvailableIn = 20;
                }
                else
                {
                    action = new Move(a.Position);
                }
            }
            else
            {
                action = new Move(gameContext.GetOppositeQG());
            }
            action.Do("guard");
        }
    }
}
