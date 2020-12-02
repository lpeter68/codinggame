using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class Player
{
    static void Main(string[] args)
    {
        string[] inputs;
        inputs = Console.ReadLine().Split(' ');
        int W = int.Parse(inputs[0]); // width of the building.
        int H = int.Parse(inputs[1]); // height of the building.
        int N = int.Parse(Console.ReadLine()); // maximum number of turns before game over.
        inputs = Console.ReadLine().Split(' ');
        int X0 = int.Parse(inputs[0]);
        int Y0 = int.Parse(inputs[1]);

        int R_Limit = W - 1;
        int L_Limit = 0;
        int U_Limit = 0;
        int D_Limit = H - 1;

        // game loop
        while (true)
        {
            string bombDir = Console.ReadLine(); // the direction of the bombs from batman's current location (U, UR, R, DR, D, DL, L or UL)

            if (bombDir.Contains('R'))
            {
                L_Limit = X0 + 1;
                X0 = L_Limit + (int)(R_Limit - L_Limit / 2);
            }

            if (bombDir.Contains('L'))
            {
                R_Limit = X0 - 1;
                X0 = L_Limit + (int)(R_Limit - L_Limit / 2);
            }

            if (bombDir.Contains('U'))
            {
                D_Limit = Y0 - 1;
                Y0 = U_Limit + (int)(D_Limit - U_Limit / 2);
            }

            if (bombDir.Contains('D'))
            {
                U_Limit = Y0 + 1;
                Y0 = U_Limit + (int)(D_Limit - U_Limit / 2);
            }

            // the location of the next window Batman should jump to.
            Console.WriteLine(X0 + " " + Y0);
        }
    }
}