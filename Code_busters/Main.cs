using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Code_busters.Objects;

class Player
{
    static void Main(string[] args)
    {
        GameContext gameContext = new GameContext()
        {
            BustersPerPlayer = int.Parse(Console.ReadLine()),
            GhostCount = int.Parse(Console.ReadLine()),
            MyTeamId = int.Parse(Console.ReadLine())
        };

        // game loop
        while (true)
        {
            gameContext.NewRound();
            int entities = int.Parse(Console.ReadLine()); // the number of busters and ghosts visible to you
            for (int i = 0; i < entities; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                int entityId = int.Parse(inputs[0]); // buster id or ghost id
                int x = int.Parse(inputs[1]);
                int y = int.Parse(inputs[2]); // position of this buster / ghost
                int entityType = int.Parse(inputs[3]); // the team id if it is a buster, -1 if it is a ghost.
                int state = int.Parse(inputs[4]); // For busters: 0=idle, 1=carrying a ghost.
                int value = int.Parse(inputs[5]); // For busters: Ghost id being carried. For ghosts: number of busters attempting to trap this ghost.
                if (entityType == -1)
                {
                    gameContext.Ghosts.Add(new Ghost(entityId, entityType, x, y, state, value));
                }
                else
                {
                    gameContext.UpdateBuster(entityId, entityType, x, y, state, value);
                }
            }

            var myBusters = gameContext.Busters.Where(b => b.EntityType == gameContext.MyTeamId);

            foreach (var buster in myBusters)
            {
                buster.DefineState(gameContext);
                buster.NextAction(gameContext);
            }
        }
    }
}