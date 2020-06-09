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
            var comment = "";
            IAction action = null;
            if (Buster.StunAvailableIn <= 0)
            {
                var a = gameContext.OppositeBusters.FirstOrDefault(b => b.Position.GetDist(Buster.Position) < 1760);
                if (a != null && a.StunFor < 3)
                {
                    action = new Stun(a);
                    Buster.StunAvailableIn = 20;
                }
            }
            if (action == null && gameContext.Ghosts.Any(g => g.Position.GetDist(Buster.Position) < 2200))
            {
                comment = "ghost to catch";
                Ghost target;
                if (Buster.GhostTarget != null && gameContext.Ghosts.Select(g => g.Id).Contains(Buster.GhostTarget.Id))
                {
                    target = Buster.GhostTarget;
                }
                else
                {
                    target = gameContext.Ghosts
                        .Where(g => g.Position.GetDist(Buster.Position) < 2200 && gameContext.MyBusters.Where(b => b.GhostTarget?.Id == g.Id).Count() <= g.Value)
                        .OrderBy(g => Buster.Position.GetDist(g.Position))
                        .FirstOrDefault();
                }
                Buster.GhostTarget = target;
                if (target != null)
                {
                    var dist = Buster.Position.GetDist(target.Position);
                    if (dist < 1760 && dist > 900)
                    {
                        action = new Bust(target.Id);
                    }
                    else
                    {
                        if (dist < 900)
                        {
                            action = new Move(target.Position.X + 50, target.Position.Y);
                        }
                        else
                        {
                            action = new Move(target.Position);
                        }
                    }
                }
            }
            if (action == null)
            {
                comment = "no ghost";
                action = new RandomMove();
            }
            action.Do(comment);
        }
    }
}
