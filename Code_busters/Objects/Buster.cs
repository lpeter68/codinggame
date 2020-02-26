using Code_busters.States;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_busters.Objects
{
    public class Buster : BaseObject
    {
        public State CurrentState { get; set; }

        public Buster(int id, int type, Point position, int state, int value)
        {
            if (type == -1) throw new Exception("invalid type for this object");
            else
            {
                Id = id;
                EntityType = type;
                Position = position;
                State = state;
                value = Value;
            }

            if (state == 0)
            {
                CurrentState = new EmptyState(this);
            }
            else
            {
                CurrentState = new FullState(this);
            }
        }
        public Buster(int id, int type, int x, int y, int state, int value) : this(id, type, new Point(x, y), state, value)
        {

        }

        public void NextAction(GameContext gameContext)
        {
            CurrentState.DoNextAction(gameContext);
        }
    }
}
