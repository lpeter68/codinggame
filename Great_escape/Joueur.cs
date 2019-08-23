using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Joueur : IPosition
{
    private Position _pos;

    public Position Pos
    {
        get => _pos;
        set => _pos = value;
    }
    public int PlayerId { get; set; }
    public int MurRestant { get; set; }

    public Direction Objectif
    {
        get
        {
            switch (PlayerId)
            {
                case 0: return Direction.DROITE;
                case 1: return Direction.GAUCHE;
                case 2: return Direction.BAS;
                case 3: return Direction.HAUT;
                default: return Direction.DROITE;
            }
        }
    }


    public Joueur(Position pos, int playerId, int murRestant)
    {
        Pos = pos;
        PlayerId = playerId;
        MurRestant = murRestant;
    }

    public void MoveToNextCase(Case target)
    {
        if (target.Pos == Pos.GetPositionBas())
        {
            Console.WriteLine("DOWN");
        }
        if (target.Pos == Pos.GetPositionDroite())
        {
            Console.WriteLine("RIGHT");
        }
        if (target.Pos == Pos.GetPositionGauche())
        {
            Console.WriteLine("LEFT");
        }
        if (target.Pos == Pos.GetPositionHaut())
        {
            Console.WriteLine("UP");
        }
    }

    public override string ToString()
    {
        return Pos.ToString() + " " + PlayerId + " " + MurRestant;
    }

    #region comparator

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(obj, null) || GetType() != obj.GetType())
        {
            return false;
        }

        var a = (Joueur)obj;
        return a.Pos == Pos && a.PlayerId == PlayerId && a.MurRestant == MurRestant;
    }

    public static bool operator ==(Joueur obj1, Joueur obj2)
    {
        if (ReferenceEquals(obj1, null)) return ReferenceEquals(obj2, null);
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Joueur obj1, Joueur obj2)
    {
        return !(obj1 == obj2);
    }

    public override int GetHashCode()
    {
        return Pos.GetHashCode() ^ MurRestant.GetHashCode() * 10000 ^ PlayerId * 100000;
    }
    #endregion
}

