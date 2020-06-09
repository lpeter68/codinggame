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

        private int _state { get; set; }

        public override int State
        {
            get { return _state; }
            set
            {
                _state = value;
                /*if (_state != 1)
                {
                    //if (Id % 2 == 0 && StunAvailableIn == 0) CurrentState = new GuardState(this);
                    //else CurrentState = new EmptyState(this);
                }
                else
                {
                    CurrentState = new FullState(this);
                }*/
            }
        }

        private int _stunAvailableIn;
        public int StunAvailableIn { get => _stunAvailableIn; set { _stunAvailableIn = value <= 0 ? 0 : value; } }

        public Point Target { get; set; }
        public Ghost GhostTarget { get; set; }
        public int StunFor
        {
            get
            {
                if (State == 2) return Value;
                else return 0;
            }
        }

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
                StunAvailableIn = 0;
            }


        }
        public Buster(int id, int type, int x, int y, int state, int value) : this(id, type, new Point(x, y), state, value)
        {

        }

        public void NextAction(GameContext gameContext)
        {
            CurrentState.DoNextAction(gameContext);
        }

        public void DefineState(GameContext gameContext)
        {
            if (_state != 1)
            {
                if (gameContext.MyBusters.OrderByDescending(b => b.Id).FirstOrDefault() == this) CurrentState = new SupportState(this);
                else if ((gameContext.GhostCount - gameContext.Score * 2) <= gameContext.BustersPerPlayer && StunAvailableIn <= 1) CurrentState = new GuardState(this);
                else CurrentState = new EmptyState(this);
            }
            else
            {
                CurrentState = new FullState(this);
            }
        }
    }
}
