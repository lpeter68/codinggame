using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code_busters.Actions;
using Code_busters.Objects;

namespace Code_busters.States
{
    public class EmptyState : State
    {
        public EmptyState(Buster buster) : base(buster)
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
                if (gameContext.Ghosts.Any())
                {
                    Ghost target = gameContext.Ghosts.First();
                    var dist = Buster.Position.GetDist(target.Position);
                    if (dist < 1760 && dist > 900)
                    {
                        action = new Bust(target.Id);
                    }
                    else
                    {
                        action = new Move(target.Position);
                    }
                }
                else
                {
                    if (Buster.Target == null || Buster.Target.GetDist(Buster.Position) < 500)
                    {
                        var x = new Random().Next(0, 16000);
                        var y = new Random().Next(0, 9000);
                        Buster.Target = new Point(x, y);
                    }
                    action = new Move(Buster.Target);
                }
            }
            action.Do("empty");
        }
    }
}
