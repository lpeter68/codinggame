using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Code_busters.Actions;
using Code_busters.Objects;

namespace Code_busters.States
{
    public class SupportState : State
    {
        public SupportState(Buster buster) : base(buster)
        {

        }

        public override void DoNextAction(GameContext gameContext)
        {
            IAction action = null;
            var ghostsTargeted = gameContext.Busters.Select(b => b.GhostTarget);
            if (ghostsTargeted != null && ghostsTargeted.Any())
            {
                var ghost = ghostsTargeted.OrderByDescending(g => g.State).FirstOrDefault();
                var dist = ghost.Position.GetDist(Buster.Position);
                if (dist < 1760 && dist > 900)
                {
                    action = new Bust(ghost.Id);
                }
                else
                {
                    if (dist < 900)
                    {
                        action = new Move(ghost.Position.X + 50, ghost.Position.Y);
                    }
                    else
                    {
                        action = new Move(ghost.Position);
                    }
                }
            }
            else
            {
                action = new RandomMove();
            }
            action.Do("support");
        }
    }
}
