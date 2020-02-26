using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Code_busters.Objects
{
    public class GameContext
    {
        public int BustersPerPlayer { get; set; }
        public int GhostCount { get; set; }
        public int MyTeamId { get; set; }

        public List<Buster> Busters { get; set; }
        public List<Ghost> Ghosts { get; set; }

        public GameContext()
        {
            Busters = new List<Buster>();
            Ghosts = new List<Ghost>();
        }

        public void NewRound()
        {
            Busters = new List<Buster>();
            Ghosts = new List<Ghost>();
        }

        public Point GetQG()
        {
            if (MyTeamId == 0) return new Point(0, 0);
            else return new Point(16000, 9000);
        }
    }
}
