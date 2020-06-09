using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code_busters.Actions;
using Code_busters.Objects;

namespace Code_busters.States
{
    public class StunState : State
    {
        public StunState(Buster buster) : base(buster)
        {

        }

        public override void DoNextAction(GameContext gameContext)
        {
            IAction action = null;
            if (Buster.StunAvailableIn <= 0 && gameContext.OppositeBusters.Any())
            {
                var a = gameContext.OppositeBusters.OrderBy(b => b.Position.GetDist(Buster.Position)).First();
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
                Buster.CurrentState = new EmptyState(Buster);
                Buster.CurrentState.DoNextAction(gameContext);
                return;
            }
            action.Do();
        }
    }
}
