using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_busters.Objects
{
    public class GameContext
    {
        public int RoundNb { get; set; }
        public int BustersPerPlayer { get; set; }
        public int GhostCount { get; set; }
        public int MyTeamId { get; set; }
        public int Score { get; set; }
        public List<Buster> Busters { get; set; }
        public List<Ghost> Ghosts { get; set; }
        public List<Buster> OppositeBusters { get => Busters.Where(b => b.EntityType != MyTeamId).ToList(); }
        public List<Buster> MyBusters { get => Busters.Where(b => b.EntityType == MyTeamId).ToList(); }

        public GameContext()
        {
            Busters = new List<Buster>();
            Ghosts = new List<Ghost>();
            RoundNb = 0;
        }

        public void NewRound()
        {
            RoundNb++;
            Ghosts = new List<Ghost>();
        }

        public void UpdateBuster(int id, int type, Point position, int state, int value)
        {
            var buster = Busters.FindIndex(b => b.Id == id);
            if (buster != -1)
            {
                Busters[buster].Position = position;
                Busters[buster].State = state;
                Busters[buster].Value = value;
                Busters[buster].StunAvailableIn--;
                Busters[buster].GhostTarget = null;
            }
            else
            {
                Busters.Add(new Buster(id, type, position, state, value));
            }
        }

        public void UpdateBuster(int id, int type, int x, int y, int state, int value)
        {
            UpdateBuster(id, type, new Point(x, y), state, value);
        }


        public Point GetQG()
        {
            if (MyTeamId == 0) return new Point(0, 0);
            else return new Point(16000, 9000);
        }

        public Point GetOppositeQG()
        {
            if (MyTeamId == 1) return new Point(2000, 2000);
            else return new Point(14000, 7000);
        }
    }
}
