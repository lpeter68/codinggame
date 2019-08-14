using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Case : IPosition
{
    public Position Pos { get; set; }
    public List<Case> Voisines { get; set; }

    public Case(Position pos)
    {
        Pos = pos;
        Voisines = new List<Case>();
    }

    public void AddVoisine(Case voisine)
    {
        if (voisine != null)
        {
            if (!Voisines.Contains(voisine))
            {
                Voisines.Add(voisine);
                voisine.AddVoisine(this);
            }
        }
    }

    public void RemoveVoisine(Case voisine)
    {
        if (voisine != null)
        {
            if (Voisines.Contains(voisine))
            {
                Voisines.Remove(voisine);
                voisine.RemoveVoisine(this);
            }
        }
    }
}