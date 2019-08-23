using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class InfoDikstra
{
    public Position From { get; set; }
    public Direction Objectif { get; set; }
    public List<Case> Result { get; set; }
    public Plateau Plateau { get; set; }

    public InfoDikstra(Position from, Direction objectif, List<Case> result, Plateau plateau)
    {
        From = from;
        Objectif = objectif;
        Result = result;
        Plateau = new Plateau(plateau);
    }
}

