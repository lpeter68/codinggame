using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using Spring2020_Pacman;

/**
 * Grab the pellets as fast as you can!
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int width = int.Parse(inputs[0]); // size of the grid
        int height = int.Parse(inputs[1]); // top left corner is (x=0, y=0)
        //Map map = new Map(width, height);
        for (int y = 0; y < height; y++)
        {
            string row = Console.ReadLine();
            var x = 0;
            //foreach (var item in row)
            //{
            //    map.SetCase(x, y, item == ' ' ? 0 : -1);
            //}
        }

        // game loop
        while (true)
        {
            inputs = Console.ReadLine().Split(' ');
            int myScore = int.Parse(inputs[0]);
            int opponentScore = int.Parse(inputs[1]);
            int visiblePacCount = int.Parse(Console.ReadLine()); // all your pacs and enemy pacs in sight
            List<int> myPacman = new List<int>();
            for (int i = 0; i < visiblePacCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int pacId = int.Parse(inputs[0]); // pac number (unique within a team)
                bool mine = inputs[1] != "0"; // true if this pac is yours
                int x = int.Parse(inputs[2]); // position in the grid
                int y = int.Parse(inputs[3]); // position in the grid
                string typeId = inputs[4]; // unused in wood leagues
                int speedTurnsLeft = int.Parse(inputs[5]); // unused in wood leagues
                int abilityCooldown = int.Parse(inputs[6]); // unused in wood leagues
                if (mine)
                {
                    myPacman.Add(pacId);
                }
            }

            int visiblePelletCount = int.Parse(Console.ReadLine()); // all pellets in sight
            List<Pellet> pellets = new List<Pellet>();
            for (int i = 0; i < visiblePelletCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                int x = int.Parse(inputs[0]);
                int y = int.Parse(inputs[1]);
                int value = int.Parse(inputs[2]); // amount of points this pellet is worth
                pellets.Add(new Pellet(x, y, value));
            }

            // Write an action using Console.WriteLine()
            // To debug: Console.Error.WriteLine("Debug messages...");
            string commands = "";
            int pelletID = 0;
            int pelletStep = 10;
            if (pellets.Any(p => p.Value == 10))
            {
                pelletStep = 1; //on vise tout les gros
                pellets = pellets.OrderByDescending(p => p.Value).ToList();
            }
            foreach (var pac in myPacman)
            {
                commands += "MOVE " + pac + " " + pellets[pelletID].X + " " + pellets[pelletID].Y + " | "; // MOVE <pacId> <x> <y>
                pelletID += pelletStep;
            }
            Console.WriteLine(commands);

        }
    }
}