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
            if (gameContext.Ghosts.Any())
            {
                Ghost target = gameContext.Ghosts.First();
                var dist = Buster.Position.GetDist(target.Position);
                if (dist < 1760 && dist > 900)
                {
                    new Bust(target.Id).Do();
                }
                else
                {
                    new Move(target.Position).Do();
                }
            }
            else
            {
                var x = new Random().Next(0, 16000);
                var y = new Random().Next(0, 9000);
                new Move(x, y).Do();
            }
        }
    }
}
