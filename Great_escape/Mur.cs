using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Mur : IPosition
{
    public Position Pos { get; set; }
    public bool IsVertical { get; set; }
    public Position PointCentral
    {
        get
        {
            if (IsVertical)
            {
                return Pos.GetPositionBas();
            }
            else
            {
                return Pos.GetPositionDroite();
            }
        }
    }

    public Mur(Position pos, bool isVertical)
    {
        Pos = pos;
        IsVertical = isVertical;
    }

    public bool BloqueCases(Case case1, Case case2, Plateau plateau)
    {
        var caseA = plateau.GetCase(Pos);
        Case caseB;
        Case caseC;
        Case caseD;
        if (IsVertical)
        {
            caseB = plateau.GetCase(Pos.GetPositionBas());
            caseC = plateau.GetCase(caseA.Pos.GetPositionGauche());
            caseD = plateau.GetCase(caseB.Pos.GetPositionGauche());
        }
        else
        {
            caseB = plateau.GetCase(Pos.GetPositionDroite());
            caseC = plateau.GetCase(caseA.Pos.GetPositionHaut());
            caseD = plateau.GetCase(caseB.Pos.GetPositionHaut());
        }
        if (case1 == caseA && case2 == caseC || case1 == caseB && case2 == caseD ||
            case2 == caseA && case1 == caseC || case2 == caseB && case1 == caseD)
        {
            return true;
        }
        return false;
    }

    public override string ToString()
    {
        return Pos.X + " " + Pos.Y + " " + (IsVertical ? "V" : "H");
    }

    #region comparator

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(obj, null) || GetType() != obj.GetType())
        {
            return false;
        }

        var a = (Mur)obj;
        return a.Pos == Pos && a.IsVertical == IsVertical;
    }

    public static bool operator ==(Mur obj1, Mur obj2)
    {
        if (ReferenceEquals(obj1, null)) return ReferenceEquals(obj2, null);
        return obj1.Equals(obj2);
    }

    public static bool operator !=(Mur obj1, Mur obj2)
    {
        return !(obj1 == obj2);
    }

    public override int GetHashCode()
    {
        return Pos.GetHashCode() ^ IsVertical.GetHashCode() * 1000;
    }
    #endregion
}